using Discord;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterRichPresence
{
    internal sealed class PresenceManager : IDisposable
    {
        #region # - Properties

        private Discord.Discord? discord;
        private Discord.ActivityManager activityManager;
        private System.Windows.Forms.Timer loop;
        private long clientId;
        private bool shouldDispose = true;

        #endregion

        #region # - Constructor

        public PresenceManager(long clientId)
        {
            this.clientId = clientId;
            discord = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.NoRequireDiscord);
            discord.SetLogHook(LogLevel.Debug, (level, s) =>
            {
                MainForm.DebugLog(level.ToString() + ": " + s);
            });
            activityManager = discord.GetActivityManager();
            loop = new()
            {
                Interval = 1,
            };
            loop.Tick += Update;
            loop.Start();
        }

        #endregion

        #region # - Events



        #endregion

        #region # - Methods

        private void Update(object? sender = null, EventArgs? e = null)
        {
            if (discord == null)
                return;
            try
            {
                discord.RunCallbacks();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to discord.RunCallbacks because {ex.Message}");
                if (ex is Discord.ResultException && ex.Message.Contains("NotRunning"))
                {
                    shouldDispose = false;
                }
                MainForm.DebugLog($"RunCallbacks Error: {ex.Message}");
            }
        }

        public void UpdateActivity(ActivityInfo info)
        {
            Discord.Activity activity = new()
            {
                Type = ActivityType.Listening, // TODO: add to activityinfo?
                ApplicationId = clientId
            };
            if (info.Details != null)
                activity.Details = info.Details;
            if (info.State != null)
                activity.State = info.State;

            if (info.AssetLargeImage != null)
                activity.Assets.LargeImage = info.AssetLargeImage;
            if (info.AssetLargeText != null)
                activity.Assets.LargeText = info.AssetLargeText;
            if (info.AssetSmallImage != null)
                activity.Assets.SmallImage = info.AssetSmallImage;
            if (info.AssetSmallText != null)
                activity.Assets.SmallText = info.AssetSmallText;

            if (info.TimestampStart.HasValue)
                activity.Timestamps.Start = info.TimestampStart.Value;
            if (info.TimestampEnd.HasValue)
                activity.Timestamps.End = info.TimestampEnd.Value;

            if (activityManager != null)
                activityManager.UpdateActivity(activity, OnActivityUpdate);
        }

        public void ClearActivity()
        {
            if (activityManager != null)
                activityManager.ClearActivity(OnActivityClear);
        }

        private void OnActivityUpdate(Discord.Result result)
        {
            Debug.WriteLine($"OnActivityUpdate: {result}");
        }

        private void OnActivityClear(Discord.Result result)
        {
            Debug.WriteLine($"OnActivityClear: {result}");
        }

        public void Dispose()
        {
            if (shouldDispose)
            {
                discord?.Dispose();
            }
            discord = null;
        }

        #endregion

    }

    internal struct ActivityInfo
    {
        public string? Details { get; set; }
        public string? State { get; set; }

        public string? AssetLargeImage { get; set; }
        public string? AssetSmallImage { get; set; }
        public string? AssetLargeText { get; set; }
        public string? AssetSmallText { get; set; }

        public long? TimestampStart { get; set; }
        public long? TimestampEnd { get; set; }

        public static bool operator ==(ActivityInfo l, ActivityInfo r) => l.Details == r.Details && l.State == r.State && l.AssetLargeImage == r.AssetLargeImage && l.AssetSmallImage == r.AssetSmallImage && l.AssetLargeText == r.AssetLargeText && l.AssetSmallText == r.AssetSmallText && l.TimestampStart == r.TimestampStart && l.TimestampEnd == r.TimestampEnd;
        public static bool operator !=(ActivityInfo l, ActivityInfo r) => l.Details != r.Details || l.State != r.State || l.AssetLargeImage != r.AssetLargeImage || l.AssetSmallImage != r.AssetSmallImage || l.AssetLargeText != r.AssetLargeText || l.AssetSmallText != r.AssetSmallText || l.TimestampStart != r.TimestampStart || l.TimestampEnd != r.TimestampEnd;
    }
}
