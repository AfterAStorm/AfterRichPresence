using Discord;
//using MoonSharp.Interpreter;
using NLua;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;

namespace AfterRichPresence
{
    public partial class MainForm : Form
    {
        /*internal readonly CoreModules DefaultScriptModules =
            CoreModules.Basic |
            CoreModules.Table |
            CoreModules.Bit32 |
            CoreModules.Coroutine |
            CoreModules.ErrorHandling |
            CoreModules.OS_Time |
            CoreModules.Json |
            CoreModules.Math |
            CoreModules.TableIterators |
            CoreModules.String;*/

        
        List<Profile> profiles = new List<Profile>();
        int currentProfileIndex { get; set; } = 0;

        PresenceManager? presenceManager;
        Lua? luaState;
        System.Windows.Forms.Timer timer;
        DateTimeOffset lastTick;
        System.Windows.Forms.Timer timerTimerDisplayUpdater;
        ActivityInfo lastInfo;
        //Script? script;

        static MainForm instance;

        public MainForm()
        {
            instance = this;
            InitializeComponent();
            trayIcon.Icon = Icon;
            trayIcon.Visible = false;

            currentProfileIndex = 0;
            UpdateTabs();
            UpdateTabEditor();

            MediaManager.MediaInfoChanged += MediaManager_MediaInfoChanged;
            MediaManager.MediaStateChanged += MediaManager_MediaStateChanged;
            //Script.WarmUp();
            timer = new();
            timer.Stop();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timerTimerDisplayUpdater = new();
            timerTimerDisplayUpdater.Interval = 100;
            timerTimerDisplayUpdater.Tick += (_, _) =>
            {
                timerLeft.Invoke(() => timerLeft.Text = !timer.Enabled ? "Enable timer trigger" :
                $"{DateTimeOffset.Now.Subtract(lastTick).TotalSeconds} seconds");
            };
            timerTimerDisplayUpdater.Start();
            presenceConfigChangedTimer.Tick += (_, _) =>
            {
                if (presenceConfigChangedTimerCount == 100)
                    UpdatePresence(DynamicTriggers.Start);
                if (presenceConfigChangedTimerCount <= 100)
                    presenceConfigChangedTimerCount += 1;
            };
            presenceConfigChangedTimer.Interval = 1;
            presenceConfigChangedTimer.Start();
        }

        public static void DebugLog(string gtxt)
        {
            instance.debugLog.Invoke(() => instance.debugLog.Text += gtxt + "\n");
        }

        #region # - Configuration

        private void AddDefault()
        {
            Profile defaultProfile = new Profile()
            {
                Name = "Default",
                ClientId = 0
            };
            profiles.Insert(0, defaultProfile);
        }

        private void LoadProfiles()
        {
            string? profilesStr = Properties.Settings.Default.Profiles;
            if (profilesStr == null || profilesStr.Length <= 2)
            {
                AddDefault();
                Profile mediaProfile = new Profile()
                {
                    Name = "Dynamic Template",
                    Type = ProfileType.Dynamic,
                    ClientId = 0,
                };
                profiles.Add(mediaProfile);
                UpdateTabs();
                UpdateTabEditor();
                return;
            }
            Debug.WriteLine("Loading... " + profilesStr);
            try
            {
                List<Profile>? parsed = JsonSerializer.Deserialize<List<Profile>>(profilesStr);
                if (parsed != null)
                    profiles = parsed;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Failed to load profiles because {e.Message}");
                MessageBox.Show($"Failed to load pre-existing profiles, sorry! Message: {e.Message}");
            }
            AddDefault();
            currentProfileIndex = 0;
            UpdateTabs();
            UpdateTabEditor();
        }

        private void SaveProfiles()
        {
            List<Profile> profilesToSave = profiles.Where(p => p.Name != "Default").ToList();
            var serialized = JsonSerializer.Serialize<List<Profile>>(profilesToSave);
            Debug.WriteLine("test:" + JsonSerializer.Serialize(profilesToSave[0]));
            Debug.WriteLine("Saving... " + serialized);

            Properties.Settings.Default.Profiles = serialized;
            Properties.Settings.Default.Save();
            Debug.WriteLine("Saved profiles!");
        }

        #endregion

        #region # - Other

        private void applicationsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start https://discord.com/developers/applications") { CreateNoWindow = true });
            applicationsLinkLabel.LinkVisited = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProfiles();
        }

        #endregion

        #region # - Tray

        private void UpdateTrayProfiles()
        {
            Debug.WriteLine($"Updating tray... {currentProfileIndex}");
            trayProfile.DropDownItems.Clear();
            for (int i = 0; i < profiles.Count; i++)
            {
                Profile profile = profiles[i];
                Debug.WriteLine($"{i} {profile.Name}");
                ToolStripMenuItem toolStripItem = new(profile.Name);
                toolStripItem.Enabled =
                    !connected ||
                    (i == currentProfileIndex || profile.ClientId == profiles[currentProfileIndex].ClientId);
                toolStripItem.Checked = i == currentProfileIndex;
                int id = i;
                toolStripItem.Click += (_, _) =>
                {
                    Debug.WriteLine($"-----------------------------------------{id} {profile.Name} clicked!");
                    profilesTabControl.SelectedIndex = id;
                    if (connected)
                        profileSwitch_Click(null, null);
                };
                trayProfile.DropDownItems.Add(toolStripItem);
            }
            trayConnected.Text = connected ? "Disconnect" : "Connect";
            trayConnected.Checked = connected;
        }

        private void trayConnected_Click(object sender, EventArgs e)
        {
            if (connected)
                profileDisconnect_Click(null, null);
            else
                profileConnect_Click(null, null);
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                trayExit_Click(sender, e);
            else if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
                trayIcon.Visible = false;
            }
        }

        private void trayExit_Click(object sender, EventArgs e)
        {
            SaveProfiles();
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProfiles();
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                trayIcon.Visible = true;
                Hide();
            }
        }

        #endregion

        #region # - Profiles' Tab Control

        private bool supressIndexChange = false;
        private int lastIndex = 0;

        private void UpdateTabs(bool keepIndex = false)
        {
            UpdateTrayProfiles();
            int index = profilesTabControl.SelectedIndex;
            List<TabPage> removeTabs = new();
            foreach (TabPage tab in profilesTabControl.TabPages)
                if (tab != profilesDefault && tab != profilesAddPage) removeTabs.Add(tab);
            foreach (TabPage tab in removeTabs)
                profilesTabControl.TabPages.Remove(tab);

            profiles.Reverse();
            foreach (Profile profile in profiles)
            {
                if (profile.Name == "Default")
                    continue;
                TabPage page = new(profile.Name);
                profilesTabControl.TabPages.Insert(1, page);
            }
            profiles.Reverse();
            //supressIndexChange = keepIndex;
            if (keepIndex)
                profilesTabControl.SelectedIndex = index;
        }

        private void profilesTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (supressIndexChange)
            {
                supressIndexChange = false;
                return;
            }
            if (profilesTabControl.SelectedIndex == profilesTabControl.TabCount - 1)
            {
                string name = "Unnamed";
                int num = 1;
                while (profiles.Find(x => x.Name == name).Name == name)
                {
                    if (name == "Unnamed")
                    {
                        name = "Unnamed 1";
                    }
                    else
                    {
                        num++;
                        name = "Unnamed " + num;
                    }
                }
                Profile profile = new Profile();
                profile.Name = name;
                profiles.Add(profile);
                UpdateTabs(true);
                profilesTabControl.SelectedIndex = profiles.IndexOf(profile);
            }
            else
            {
                currentProfileIndex = profilesTabControl.SelectedIndex;
            }
            lastIndex = profilesTabControl.SelectedIndex;
            UpdateTabEditor();
            UpdateTrayProfiles();
        }

        #endregion

        #region # - Profile Editor

        private bool suppressEditorEvents = false;
        private System.Windows.Forms.Timer presenceConfigChangedTimer = new();
        private int presenceConfigChangedTimerCount = 101;

        private void UpdateTabEditor() // based on currently selected tab/profile
        {
            suppressEditorEvents = true;
            if (profiles.Count <= currentProfileIndex)
                return; // ???
            Profile profile = profiles[currentProfileIndex];
            foreach (Control control in new Control[]
            {
                profileEditorName,
                profileEditorTypeStatic,
                profileEditorTypeDynamic,
                profileEditorRemove
            })
                control.Enabled = profile.Name != "Default";
            profileEditorName.Text = profile.Name;

            profileEditorTypeStatic.Checked = profile.Type == ProfileType.Static;
            profileEditorTypeDynamic.Checked = profile.Type == ProfileType.Dynamic;

            // static
            profileEditorStaticGroup.Visible = profile.Type == ProfileType.Static;
            profileEditorStaticLargeImage.Text = profile.StaticProperties.AssetLargeImage;
            profileEditorStaticLargeText.Text = profile.StaticProperties.AssetLargeText;
            profileEditorStaticSmallImage.Text = profile.StaticProperties.AssetSmallImage;
            profileEditorStaticSmallText.Text = profile.StaticProperties.AssetSmallText;
            profileEditorStaticDetails.Text = profile.StaticProperties.Details;
            profileEditorStaticState.Text = profile.StaticProperties.State;
            profileEditorStaticTimestampStart.Text = profile.StaticProperties.TimestampStart.ToString();
            profileEditorStaticTimestampEnd.Text = profile.StaticProperties.TimestampEnd.ToString();

            profileEditorStaticTimestampStart.Enabled = profile.StaticTimestampType == ProfileTimestampType.Custom;
            profileEditorStaticTimestampEnd.Enabled = profileEditorStaticTimestampStart.Enabled;

            profileEditorStaticTimestampTypeNone.Checked = profile.StaticTimestampType == ProfileTimestampType.None;
            profileEditorStaticTimestampTypeCustom.Checked = profile.StaticTimestampType == ProfileTimestampType.Custom;
            profileEditorStaticTimestampTypeLocalTime.Checked = profile.StaticTimestampType == ProfileTimestampType.LocalTime;

            // dynamic
            profileEditorDynamicGroup.Visible = profile.Type == ProfileType.Dynamic;
            profileEditorDynamicLua.Text = profile.DynamicProperties.Script;
            timerInput.Text = profile.DynamicProperties.TimerInterval.ToString();
            timerUnit.Text = profile.DynamicProperties.TimerUnit.ToString().ToLower();

            // other
            profileSwitch.Enabled =
                (connected && activeProfileIndex != currentProfileIndex && activeProfileIndex > 0 && profiles[activeProfileIndex].ClientId == profile.ClientId);

            var trigger = profile.DynamicProperties.Triggers;
            List<string> triggers = new();
            foreach (DynamicTriggers t in Enum.GetValues(typeof(DynamicTriggers)))
                if (trigger.HasFlag(t) && t != DynamicTriggers.None)
                    triggers.Add(Enum.GetName(t) ?? "");

            for (int i = 0; i < profileEditorDynamicTriggers.Items.Count; i++)
            {
                string item = (string)profileEditorDynamicTriggers.Items[i];
                profileEditorDynamicTriggers.SetItemChecked(i, false);
                foreach (string tr in triggers)
                    if (item.StartsWith(tr.Substring(0, 4)))
                    {
                        Debug.WriteLine(item.ToString() + " | " + tr.ToString());
                        profileEditorDynamicTriggers.SetItemChecked(i, true);
                        break;
                    }
            }

            // etc
            profileEditorClientId.Text = profile.ClientId.ToString();
            profileEditorClientId.Enabled = !connected;
            suppressEditorEvents = false;

            foreach (Control control in new Control[]
            {
                profileEditorName,
                profileEditorTypeStatic,
                profileEditorTypeDynamic,

                profileEditorStaticGroup,

                profileEditorDynamicGroup
            })
                control.Enabled = true;

            foreach (Control control in new Control[]
            {
                profileEditorName,
                profileEditorTypeStatic,
                profileEditorTypeDynamic,
                profileEditorRemove,

                profileEditorStaticGroup,

                profileEditorDynamicGroup
            })
                control.Enabled = (connected && currentProfileIndex != activeProfileIndex) ? false : control.Enabled;

        }

        private void profileSwitch_Click(object? sender, EventArgs? e)
        {
            if (!connected)
                return;
            //Profile current = profiles[currentProfileIndex];
            //Profile active = profiles[activeProfileIndex];
            profileConnect_Click(null, null);
            UpdateTrayProfiles();
            UpdateTabEditor(); // above already does that, but do a lil' refresh :)
        }

        private void profileEditorName_TextChanged(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            Profile profile = profiles[currentProfileIndex];
            foreach (Profile otherProfile in profiles)
                if (otherProfile.Name == profileEditorName.Text && otherProfile.Name != profile.Name)
                {
                    errorProvider.SetError(profileEditorName, "Profile name is already taken!");
                    return;
                }
            errorProvider.SetError(profileEditorName, null);
            profile.Name = profileEditorName.Text;
            profiles[currentProfileIndex] = profile;
            UpdateTabs(true);
            profileEditorName.Focus();
            profileEditorName.Select(profileEditorName.Text.Length, 0);

            SaveProfiles();
        }

        private void ProfileEditorType_CheckChanged(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            Profile profile = profiles[currentProfileIndex];
            profile.Type = profileEditorTypeStatic.Checked ? ProfileType.Static : ProfileType.Dynamic;
            profiles[currentProfileIndex] = profile;
            UpdateTabEditor();

            SaveProfiles();
        }

        private void profileEditorRemove_Click(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            if (MessageBox.Show("Are you sure?", "Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;
            profiles.Remove(profiles[currentProfileIndex]);
            int index = lastIndex;
            UpdateTabs();
            UpdateTabEditor();
            profilesTabControl.SelectedIndex = index - 1;

            SaveProfiles();
        }

        private void profileEditorClientId_TextChanged(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            bool parsed = long.TryParse(profileEditorClientId.Text, out long clientId);
            if (!parsed)
            {
                errorProvider.SetError(profileEditorClientId, "Invalid client id!");
                return;
            }
            errorProvider.SetError(profileEditorClientId, null);
            Profile profile = profiles[currentProfileIndex];
            profile.ClientId = clientId;
            profiles[currentProfileIndex] = profile;

            SaveProfiles();
        }

        private void StaticPropertiesChanged(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            Profile profile = profiles[currentProfileIndex];
            ActivityInfo info = profile.StaticProperties;
            info.AssetLargeImage = profileEditorStaticLargeImage.Text;
            info.AssetLargeText = profileEditorStaticLargeText.Text;
            info.AssetSmallImage = profileEditorStaticSmallImage.Text;
            info.AssetSmallText = profileEditorStaticSmallText.Text;
            info.Details = profileEditorStaticDetails.Text;
            info.State = profileEditorStaticState.Text;
            if (profile.StaticTimestampType == ProfileTimestampType.Custom)
            {
                bool parsedStart = long.TryParse(profileEditorStaticTimestampStart.Text, out long start); ;
                bool parsedEnd = long.TryParse(profileEditorStaticTimestampEnd.Text, out long end);
                info.TimestampStart = null;
                info.TimestampEnd = null;
                if (parsedStart)
                    info.TimestampStart = start;
                if (parsedEnd)
                    info.TimestampEnd = end;
            }
            profile.StaticProperties = info;
            profiles[currentProfileIndex] = profile;
            presenceConfigChangedTimerCount = 0;

            SaveProfiles();
        }

        private void StaticPropertiesChangedRadio(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            Profile profile = profiles[currentProfileIndex];
            profile.StaticTimestampType = profileEditorStaticTimestampTypeCustom.Checked ? ProfileTimestampType.Custom :
                profileEditorStaticTimestampTypeLocalTime.Checked ? ProfileTimestampType.LocalTime : ProfileTimestampType.None;
            profiles[currentProfileIndex] = profile;
            UpdateTabEditor();
            StaticPropertiesChanged(sender, e);
        }

        private static int GetIntervalBasedOn(int interval, TimerUnit unit)
        {
            return unit switch
            {
                TimerUnit.Milliseconds => interval, // already in ms :)
                TimerUnit.Seconds => interval * 1000, // ms to s
                TimerUnit.Minutes => interval * 1000 * 60, // ms to s to min
                TimerUnit.Hours => interval * 1000 * 60 * 60, // ms to s to min to hr
                TimerUnit.Days => interval * 1000 * 60 * 60 * 24, // ms to s to min to hr to days
                _ => interval,
            };
        }

        private void CheckTimerTrigger(DynamicProperties properties)
        {
            if (properties.Triggers.HasFlag(DynamicTriggers.Timer))
            {
                timer.Interval = Math.Max(1, GetIntervalBasedOn(properties.TimerInterval, properties.TimerUnit));
                lastTick = DateTimeOffset.Now;
                if (!timer.Enabled)
                    timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        private void DynamicPropertiesChanged(object sender, EventArgs e)
        {
            if (suppressEditorEvents)
                return;
            Profile profile = profiles[currentProfileIndex];
            var properties = profile.DynamicProperties;

            properties.Script = profileEditorDynamicLua.Text;
            properties.Triggers = DynamicTriggers.None;

            bool parsed = int.TryParse(timerInput.Text, out int interval);
            if (parsed)
                properties.TimerInterval = interval;
            properties.TimerUnit = Enum.Parse<TimerUnit>(timerUnit.Text.Substring(0, 1).ToUpper() + timerUnit.Text.Substring(1));

            foreach (string item in profileEditorDynamicTriggers.CheckedItems)
            {
                DynamicTriggers trigger = Enum.Parse<DynamicTriggers>(item.Replace(" ", ""));
                properties.Triggers |= trigger;
            }
            CheckTimerTrigger(properties);

            profile.DynamicProperties = properties;
            profiles[currentProfileIndex] = profile;

            SaveProfiles();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            lastTick = DateTimeOffset.Now;
            UpdatePresence(DynamicTriggers.Timer);
        }

        #endregion

        #region # - Profile / Presence Integration

        private bool connected = false;
        private int activeProfileIndex = 0;

        void LuaPrint(params object[] s)
        {
            foreach (object o in s)
            {
                profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += $"{o.ToString()}\n");
            }
        }

        private void UpdatePresence(DynamicTriggers trigger = DynamicTriggers.None)
        {
            if (!connected || presenceManager == null)
                return;
            Profile profile = profiles[activeProfileIndex];

            if (profile.Type == ProfileType.Static && trigger.HasFlag(DynamicTriggers.Start))
            {
                presenceManager.UpdateActivity(new ActivityInfo()
                {
                    Details = profile.StaticProperties.Details,
                    State = profile.StaticProperties.State,
                    AssetLargeImage = profile.StaticProperties.AssetLargeImage,//"https://media.tenor.com/iRnNOGMr56EAAAAC/mauzymice-mauzy.gif",
                    AssetLargeText = profile.StaticProperties.AssetLargeText,//"uwu",
                    AssetSmallImage = profile.StaticProperties.AssetSmallImage, //"https://media.tenor.com/hw_qrCgCvfIAAAAd/command-block-minecraft.gif",
                    AssetSmallText = profile.StaticProperties.AssetSmallText, //"spinny spin!"
                    TimestampStart = profile.StaticProperties.TimestampStart,
                    TimestampEnd = profile.StaticProperties.TimestampEnd
                });
            }
            else if (profile.Type == ProfileType.Dynamic)
            {
                if ((profile.DynamicProperties.Triggers & trigger) != trigger)
                    return; // trigger disabled
                if (luaState != null)
                    luaState.Close();
                profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Clear());
                luaState = new();
                luaState.RegisterFunction("print", this, typeof(MainForm).GetMethod(nameof(MainForm.LuaPrint), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
                luaState.NewTable("presence");

                List<string> triggers = new();
                foreach (DynamicTriggers t in Enum.GetValues(typeof(DynamicTriggers)))
                    if (trigger.HasFlag(t) && t != DynamicTriggers.None)
                        triggers.Add(Enum.GetName(t) ?? "");
                string triggersStr = "";
                for (int i = 0; i < triggers.Count; i++)
                    triggersStr += i == triggers.Count - 1 ? triggers[i] : triggers[i] + ", ";
                profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text = $"Trigger ({triggersStr})\n");

                if (trigger.HasFlag(DynamicTriggers.MediaUpdated))
                {
                    Debug.WriteLine("create media table!");
                    MediaState state = MediaManager.GetLiveMediaState();
                    MediaInfo info = MediaManager.GetInfo(); // cant get live because it's async, and if we make this an async function, it break ):
                    luaState.NewTable("media");
                    LuaTable mediaTable = luaState.GetTable("media");
                    mediaTable["state"] = state.ToString();
                    mediaTable["title"] = info.Title ?? "unknown";
                    mediaTable["artist"] = info.Artist ?? "unknown";
                    mediaTable["position"] = info.Position.TotalSeconds;
                    mediaTable["length"] = info.Length.TotalSeconds;
                    luaState["media"] = mediaTable;
                }

                try
                {
                    luaState.DoString(profile.DynamicProperties.Script);
                }
                catch (Exception e)
                {
                    profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += "[!] An error occured! [!]\n");
                    profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += e.Message);

                    presenceManager.UpdateActivity(new ActivityInfo()
                    {
                        Details = "An error occurred!",
                        State = e != null ? e.Message : "No error message to provide!",
                        TimestampStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        AssetLargeImage = "https://download.logo.wine/logo/Lua_(programming_language)/Lua_(programming_language)-Logo.wine.png",
                        AssetLargeText = "Lua"
                    });

                    return;
                }
                LuaTable presenceTable = luaState.GetTable("presence");
                if (presenceTable != null)
                {
                    ActivityInfo info = new()
                    {
                        Details = (string?)presenceTable["details"],
                        State = (string?)presenceTable["state"],
                        AssetLargeImage = (string?)presenceTable["largeImage"],
                        AssetLargeText = (string?)presenceTable["largeText"],
                        AssetSmallImage = (string?)presenceTable["smallImage"],
                        AssetSmallText = (string?)presenceTable["smallText"],
                        TimestampStart = (long?)Convert.ToInt64((presenceTable["timestampStart"])),
                        TimestampEnd = (long?)Convert.ToInt64((presenceTable["timestampEnd"])),
                    };
                    if (lastInfo != info)
                    {
                        lastInfo = info;
                        presenceManager.UpdateActivity(info);
                    }
                }
                else
                    presenceManager.ClearActivity();
                luaState.Dispose();
                luaState = null;
                /*script = new(DefaultScriptModules);
                script.Options.DebugPrint = s =>
                {
                    profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += s.Replace("\t", " ") + "\n");
                };
                List<string> triggers = new();
                foreach (DynamicTriggers t in Enum.GetValues(typeof(DynamicTriggers)))
                    if (trigger.HasFlag(t) && t != DynamicTriggers.None)
                        triggers.Add(Enum.GetName(t) ?? "");
                string triggersStr = "";
                for (int i = 0; i < triggers.Count; i++)
                    triggersStr += i == triggers.Count - 1 ? triggers[i] : triggers[i] + ", ";
                profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text = $"Trigger ({triggersStr})\n");
                Table presenceTable = new Table(script);
                presenceTable.Set("details", DynValue.NewString(""));
                presenceTable.Set("state", DynValue.NewString(""));
                script.Globals["presence"] = presenceTable;

                if (trigger.HasFlag(DynamicTriggers.MediaUpdated))
                {
                    MediaState state = MediaManager.GetState();
                    MediaInfo info = MediaManager.GetInfo();
                    Table mediaTable = new(script);
                    mediaTable["title"] = info.Title ?? "unknown";
                    mediaTable["artist"] = info.Artist ?? "unknown";
                    mediaTable["position"] = info.Position.TotalSeconds;
                    mediaTable["length"] = info.Length.TotalSeconds;
                    script.Globals["media"] = mediaTable;
                }

                try
                {
                    script.DoString(profile.DynamicProperties.Script);
                }
                catch (Exception e)
                {
                    profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += "[!] An error occured! [!]\n");
                    profileEditorDynamicOutput.Invoke(() => profileEditorDynamicOutput.Text += e.Message);

                    presenceManager.UpdateActivity(new ActivityInfo()
                    {
                        Details = "An error occurred!",
                        State = e != null ? e.Message : "No error message to provide!",
                        TimestampStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        AssetLargeImage = "https://download.logo.wine/logo/Lua_(programming_language)/Lua_(programming_language)-Logo.wine.png",
                        AssetLargeText = "Lua"
                    });
                    return;
                }
                presenceTable = (Table)script.Globals["presence"];
                if (presenceTable != null)
                {
                    ActivityInfo info = new()
                    {
                        Details = (string?)presenceTable["details"],
                        State = (string?)presenceTable["state"],
                        AssetLargeImage = (string?)presenceTable["largeImage"],
                        AssetLargeText = (string?)presenceTable["largeText"],
                        AssetSmallImage = (string?)presenceTable["smallImage"],
                        AssetSmallText = (string?)presenceTable["smallText"],
                        TimestampStart = (long?)((Double?)presenceTable["timestampStart"]),
                        TimestampEnd = (long?)((Double?)presenceTable["timestampEnd"]),
                    };
                    presenceManager.UpdateActivity(info);
                }
                else
                    presenceManager.ClearActivity();*/
            }
        }

        private void MediaManager_MediaStateChanged(MediaState state)
        {
            Debug.WriteLine($"MediaState changed! {state}");
            UpdatePresence(DynamicTriggers.MediaUpdated);
        }

        private void MediaManager_MediaInfoChanged(MediaInfo info)
        {
            Debug.WriteLine($"MediaInfo changed! {info.Title} --> {info.Artist} ({info.Position}>>{info.Position.TotalSeconds} {info.Length}>>{info.Length.TotalSeconds})");
            UpdatePresence(DynamicTriggers.MediaUpdated);
        }

        private void profileConnect_Click(object? sender, EventArgs? e)
        {
            Profile profile = profiles[currentProfileIndex];
            activeProfileIndex = currentProfileIndex;
            profileConnect.Enabled = false;
            profileStatus.Text = $"Connecting using {profile.Name} ({profile.ClientId})...";
            connected = true;
            UpdateTabEditor();
            UpdateTrayProfiles();
            if (presenceManager == null)
                presenceManager = new PresenceManager(profile.ClientId);
            profileStatus.Text = $"Using {profile.Name} ({profile.ClientId})!";
            profileDisconnect.Enabled = true;
            var flags = DynamicTriggers.Start;
            if (MediaManager.GetLiveMediaState() == MediaState.Playing)
                flags |= DynamicTriggers.MediaUpdated;
            CheckTimerTrigger(profile.DynamicProperties);
            UpdatePresence(flags);
        }

        private void profileDisconnect_Click(object? sender, EventArgs? e)
        {
            profileDisconnect.Enabled = false;
            profileStatus.Text = "Disconnecting...";
            presenceManager?.Dispose();
            presenceManager = null;
            profileStatus.Text = "Idle";
            profileConnect.Enabled = true;
            connected = false;
            UpdateTabEditor();
            UpdateTrayProfiles();
        }

        #endregion

        private void luaPopoutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            profileEditorDynamicLua.Enabled = false;
            LuaPopoutForm form = new(profileEditorDynamicLua.Text, lua =>
            {
                profileEditorDynamicLua.Text = lua;
            });
            profilesTabControl.Enabled = false;
            form.Icon = Icon;
            form.Show();

            form.FormClosing += (_, _) =>
            {
                profileEditorDynamicLua.Enabled = true;
                profilesTabControl.Enabled = true;
            };
        }

        private void docsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start https://github.com/AfterAStorm/AfterRichPresence/wiki") { CreateNoWindow = true });
            applicationsLinkLabel.LinkVisited = true;
        }

        private void profileEditorDynamicRefresh_Click(object sender, EventArgs e)
        {
            UpdatePresence(DynamicTriggers.Start | DynamicTriggers.MediaUpdated);
        }
    }
}