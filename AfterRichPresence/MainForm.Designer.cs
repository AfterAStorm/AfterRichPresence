namespace AfterRichPresence
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            trayIcon = new NotifyIcon(components);
            trayContextMenuStrip = new ContextMenuStrip(components);
            trayConnected = new ToolStripMenuItem();
            trayProfile = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            trayExit = new ToolStripMenuItem();
            profilesTabControl = new TabControl();
            profilesDefault = new TabPage();
            profilesAddPage = new TabPage();
            profilePanel = new Panel();
            profileEditorDynamicGroup = new GroupBox();
            timerLeft = new Label();
            timerUnit = new ComboBox();
            label15 = new Label();
            timerInput = new TextBox();
            profileEditorDynamicRefresh = new Button();
            luaPopoutLinkLabel = new LinkLabel();
            docsLinkLabel = new LinkLabel();
            label14 = new Label();
            profileEditorDynamicOutput = new RichTextBox();
            label13 = new Label();
            profileEditorDynamicTriggers = new CheckedListBox();
            label12 = new Label();
            profileEditorDynamicLua = new RichTextBox();
            applicationsLinkLabel = new LinkLabel();
            profileEditorStaticGroup = new GroupBox();
            profileEditorStaticState = new TextBox();
            label11 = new Label();
            profileEditorStaticDetails = new TextBox();
            label10 = new Label();
            profileEditorStaticTimestampTypeNone = new RadioButton();
            profileEditorStaticTimestampTypeLocalTime = new RadioButton();
            profileEditorStaticTimestampTypeCustom = new RadioButton();
            label9 = new Label();
            profileEditorStaticTimestampEnd = new TextBox();
            profileEditorStaticTimestampStart = new TextBox();
            label8 = new Label();
            profileEditorStaticSmallText = new TextBox();
            label7 = new Label();
            profileEditorStaticSmallImage = new TextBox();
            label6 = new Label();
            profileEditorStaticLargeText = new TextBox();
            label5 = new Label();
            profileEditorStaticLargeImage = new TextBox();
            label4 = new Label();
            profileEditorClientId = new TextBox();
            label3 = new Label();
            profileEditorRemove = new Button();
            label2 = new Label();
            profileEditorTypeDynamic = new RadioButton();
            profileEditorTypeStatic = new RadioButton();
            profileEditorName = new TextBox();
            label1 = new Label();
            errorProvider = new ErrorProvider(components);
            profileConnect = new Button();
            profileDisconnect = new Button();
            profileStatus = new Label();
            profileSwitch = new Button();
            trayContextMenuStrip.SuspendLayout();
            profilesTabControl.SuspendLayout();
            profilePanel.SuspendLayout();
            profileEditorDynamicGroup.SuspendLayout();
            profileEditorStaticGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayContextMenuStrip;
            trayIcon.Text = "AfterRichPresence";
            trayIcon.MouseClick += trayIcon_MouseClick;
            // 
            // trayContextMenuStrip
            // 
            trayContextMenuStrip.Items.AddRange(new ToolStripItem[] { trayConnected, trayProfile, toolStripSeparator1, trayExit });
            trayContextMenuStrip.Name = "trayContextMenuStrip";
            trayContextMenuStrip.Size = new Size(158, 76);
            // 
            // trayConnected
            // 
            trayConnected.Name = "trayConnected";
            trayConnected.Size = new Size(157, 22);
            trayConnected.Text = "Connect";
            trayConnected.Click += trayConnected_Click;
            // 
            // trayProfile
            // 
            trayProfile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            trayProfile.Name = "trayProfile";
            trayProfile.Size = new Size(157, 22);
            trayProfile.Text = "Profile (Default)";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(154, 6);
            // 
            // trayExit
            // 
            trayExit.Name = "trayExit";
            trayExit.Size = new Size(157, 22);
            trayExit.Text = "Exit (MMB)";
            trayExit.Click += trayExit_Click;
            // 
            // profilesTabControl
            // 
            profilesTabControl.Controls.Add(profilesDefault);
            profilesTabControl.Controls.Add(profilesAddPage);
            profilesTabControl.ItemSize = new Size(50, 20);
            profilesTabControl.Location = new Point(12, 12);
            profilesTabControl.Name = "profilesTabControl";
            profilesTabControl.SelectedIndex = 0;
            profilesTabControl.Size = new Size(629, 426);
            profilesTabControl.TabIndex = 1;
            profilesTabControl.SelectedIndexChanged += profilesTabControl_SelectedIndexChanged;
            // 
            // profilesDefault
            // 
            profilesDefault.ForeColor = SystemColors.ControlText;
            profilesDefault.Location = new Point(4, 24);
            profilesDefault.Name = "profilesDefault";
            profilesDefault.Padding = new Padding(3);
            profilesDefault.Size = new Size(621, 398);
            profilesDefault.TabIndex = 0;
            profilesDefault.Text = "Default";
            profilesDefault.UseVisualStyleBackColor = true;
            // 
            // profilesAddPage
            // 
            profilesAddPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            profilesAddPage.Location = new Point(4, 24);
            profilesAddPage.Name = "profilesAddPage";
            profilesAddPage.Padding = new Padding(3);
            profilesAddPage.Size = new Size(621, 398);
            profilesAddPage.TabIndex = 1;
            profilesAddPage.Text = "     +";
            profilesAddPage.UseVisualStyleBackColor = true;
            // 
            // profilePanel
            // 
            profilePanel.Controls.Add(profileEditorDynamicGroup);
            profilePanel.Controls.Add(applicationsLinkLabel);
            profilePanel.Controls.Add(profileEditorStaticGroup);
            profilePanel.Controls.Add(profileEditorClientId);
            profilePanel.Controls.Add(label3);
            profilePanel.Controls.Add(profileEditorRemove);
            profilePanel.Controls.Add(label2);
            profilePanel.Controls.Add(profileEditorTypeDynamic);
            profilePanel.Controls.Add(profileEditorTypeStatic);
            profilePanel.Controls.Add(profileEditorName);
            profilePanel.Controls.Add(label1);
            profilePanel.Location = new Point(16, 36);
            profilePanel.Name = "profilePanel";
            profilePanel.Size = new Size(621, 398);
            profilePanel.TabIndex = 2;
            // 
            // profileEditorDynamicGroup
            // 
            profileEditorDynamicGroup.Controls.Add(timerLeft);
            profileEditorDynamicGroup.Controls.Add(timerUnit);
            profileEditorDynamicGroup.Controls.Add(label15);
            profileEditorDynamicGroup.Controls.Add(timerInput);
            profileEditorDynamicGroup.Controls.Add(profileEditorDynamicRefresh);
            profileEditorDynamicGroup.Controls.Add(luaPopoutLinkLabel);
            profileEditorDynamicGroup.Controls.Add(docsLinkLabel);
            profileEditorDynamicGroup.Controls.Add(label14);
            profileEditorDynamicGroup.Controls.Add(profileEditorDynamicOutput);
            profileEditorDynamicGroup.Controls.Add(label13);
            profileEditorDynamicGroup.Controls.Add(profileEditorDynamicTriggers);
            profileEditorDynamicGroup.Controls.Add(label12);
            profileEditorDynamicGroup.Controls.Add(profileEditorDynamicLua);
            profileEditorDynamicGroup.Location = new Point(6, 104);
            profileEditorDynamicGroup.Name = "profileEditorDynamicGroup";
            profileEditorDynamicGroup.Size = new Size(609, 288);
            profileEditorDynamicGroup.TabIndex = 11;
            profileEditorDynamicGroup.TabStop = false;
            profileEditorDynamicGroup.Text = "Dynamic Presence Options";
            // 
            // timerLeft
            // 
            timerLeft.Location = new Point(132, 89);
            timerLeft.Name = "timerLeft";
            timerLeft.Size = new Size(72, 77);
            timerLeft.TabIndex = 20;
            timerLeft.Text = "0s";
            // 
            // timerUnit
            // 
            timerUnit.FormattingEnabled = true;
            timerUnit.Items.AddRange(new object[] { "milliseconds", "seconds", "minutes", "hours", "days" });
            timerUnit.Location = new Point(132, 63);
            timerUnit.Name = "timerUnit";
            timerUnit.Size = new Size(72, 23);
            timerUnit.TabIndex = 19;
            timerUnit.Text = "milliseconds";
            timerUnit.SelectedIndexChanged += DynamicPropertiesChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(132, 19);
            label15.Name = "label15";
            label15.Size = new Size(37, 15);
            label15.TabIndex = 18;
            label15.Text = "Timer";
            // 
            // timerInput
            // 
            timerInput.Location = new Point(132, 37);
            timerInput.Name = "timerInput";
            timerInput.Size = new Size(72, 23);
            timerInput.TabIndex = 17;
            timerInput.TextChanged += DynamicPropertiesChanged;
            // 
            // profileEditorDynamicRefresh
            // 
            profileEditorDynamicRefresh.Location = new Point(6, 137);
            profileEditorDynamicRefresh.Name = "profileEditorDynamicRefresh";
            profileEditorDynamicRefresh.Size = new Size(75, 23);
            profileEditorDynamicRefresh.TabIndex = 16;
            profileEditorDynamicRefresh.Text = "Refresh";
            profileEditorDynamicRefresh.UseVisualStyleBackColor = true;
            profileEditorDynamicRefresh.Click += profileEditorDynamicRefresh_Click;
            // 
            // luaPopoutLinkLabel
            // 
            luaPopoutLinkLabel.AutoSize = true;
            luaPopoutLinkLabel.Location = new Point(557, 19);
            luaPopoutLinkLabel.Name = "luaPopoutLinkLabel";
            luaPopoutLinkLabel.Size = new Size(46, 15);
            luaPopoutLinkLabel.TabIndex = 15;
            luaPopoutLinkLabel.TabStop = true;
            luaPopoutLinkLabel.Text = "Popout";
            luaPopoutLinkLabel.TextAlign = ContentAlignment.TopRight;
            luaPopoutLinkLabel.LinkClicked += luaPopoutLinkLabel_LinkClicked;
            // 
            // docsLinkLabel
            // 
            docsLinkLabel.AutoSize = true;
            docsLinkLabel.Location = new Point(171, 168);
            docsLinkLabel.Name = "docsLinkLabel";
            docsLinkLabel.Size = new Size(33, 15);
            docsLinkLabel.TabIndex = 14;
            docsLinkLabel.TabStop = true;
            docsLinkLabel.Text = "Docs";
            docsLinkLabel.TextAlign = ContentAlignment.TopRight;
            docsLinkLabel.LinkClicked += docsLinkLabel_LinkClicked;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 168);
            label14.Name = "label14";
            label14.Size = new Size(45, 15);
            label14.TabIndex = 13;
            label14.Text = "Output";
            // 
            // profileEditorDynamicOutput
            // 
            profileEditorDynamicOutput.Location = new Point(5, 186);
            profileEditorDynamicOutput.Name = "profileEditorDynamicOutput";
            profileEditorDynamicOutput.ReadOnly = true;
            profileEditorDynamicOutput.Size = new Size(199, 96);
            profileEditorDynamicOutput.TabIndex = 12;
            profileEditorDynamicOutput.Text = "";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 19);
            label13.Name = "label13";
            label13.Size = new Size(48, 15);
            label13.TabIndex = 11;
            label13.Text = "Triggers";
            // 
            // profileEditorDynamicTriggers
            // 
            profileEditorDynamicTriggers.CheckOnClick = true;
            profileEditorDynamicTriggers.FormattingEnabled = true;
            profileEditorDynamicTriggers.Items.AddRange(new object[] { "Start", "Media Updated", "Timer" });
            profileEditorDynamicTriggers.Location = new Point(6, 37);
            profileEditorDynamicTriggers.Name = "profileEditorDynamicTriggers";
            profileEditorDynamicTriggers.Size = new Size(120, 94);
            profileEditorDynamicTriggers.TabIndex = 10;
            profileEditorDynamicTriggers.SelectedIndexChanged += DynamicPropertiesChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(210, 19);
            label12.Name = "label12";
            label12.Size = new Size(60, 15);
            label12.TabIndex = 9;
            label12.Text = "Lua Editor";
            // 
            // profileEditorDynamicLua
            // 
            profileEditorDynamicLua.AcceptsTab = true;
            profileEditorDynamicLua.DetectUrls = false;
            profileEditorDynamicLua.Location = new Point(210, 37);
            profileEditorDynamicLua.Name = "profileEditorDynamicLua";
            profileEditorDynamicLua.Size = new Size(393, 245);
            profileEditorDynamicLua.TabIndex = 8;
            profileEditorDynamicLua.Text = "--[[ GLHF ]]--";
            profileEditorDynamicLua.WordWrap = false;
            profileEditorDynamicLua.TextChanged += DynamicPropertiesChanged;
            // 
            // applicationsLinkLabel
            // 
            applicationsLinkLabel.AutoSize = true;
            applicationsLinkLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            applicationsLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            applicationsLinkLabel.Location = new Point(201, 78);
            applicationsLinkLabel.Name = "applicationsLinkLabel";
            applicationsLinkLabel.Size = new Size(81, 15);
            applicationsLinkLabel.TabIndex = 10;
            applicationsLinkLabel.TabStop = true;
            applicationsLinkLabel.Text = "Applications...";
            applicationsLinkLabel.LinkClicked += applicationsLinkLabel_LinkClicked;
            // 
            // profileEditorStaticGroup
            // 
            profileEditorStaticGroup.Controls.Add(profileEditorStaticState);
            profileEditorStaticGroup.Controls.Add(label11);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticDetails);
            profileEditorStaticGroup.Controls.Add(label10);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticTimestampTypeNone);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticTimestampTypeLocalTime);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticTimestampTypeCustom);
            profileEditorStaticGroup.Controls.Add(label9);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticTimestampEnd);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticTimestampStart);
            profileEditorStaticGroup.Controls.Add(label8);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticSmallText);
            profileEditorStaticGroup.Controls.Add(label7);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticSmallImage);
            profileEditorStaticGroup.Controls.Add(label6);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticLargeText);
            profileEditorStaticGroup.Controls.Add(label5);
            profileEditorStaticGroup.Controls.Add(profileEditorStaticLargeImage);
            profileEditorStaticGroup.Controls.Add(label4);
            profileEditorStaticGroup.Location = new Point(6, 104);
            profileEditorStaticGroup.Name = "profileEditorStaticGroup";
            profileEditorStaticGroup.Size = new Size(609, 288);
            profileEditorStaticGroup.TabIndex = 9;
            profileEditorStaticGroup.TabStop = false;
            profileEditorStaticGroup.Text = "Static Presence Options";
            // 
            // profileEditorStaticState
            // 
            profileEditorStaticState.Location = new Point(270, 257);
            profileEditorStaticState.Name = "profileEditorStaticState";
            profileEditorStaticState.Size = new Size(255, 23);
            profileEditorStaticState.TabIndex = 29;
            profileEditorStaticState.TextChanged += StaticPropertiesChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(270, 239);
            label11.Name = "label11";
            label11.Size = new Size(33, 15);
            label11.TabIndex = 28;
            label11.Text = "State";
            // 
            // profileEditorStaticDetails
            // 
            profileEditorStaticDetails.Location = new Point(9, 257);
            profileEditorStaticDetails.Name = "profileEditorStaticDetails";
            profileEditorStaticDetails.Size = new Size(255, 23);
            profileEditorStaticDetails.TabIndex = 27;
            profileEditorStaticDetails.TextChanged += StaticPropertiesChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(9, 239);
            label10.Name = "label10";
            label10.Size = new Size(42, 15);
            label10.TabIndex = 26;
            label10.Text = "Details";
            // 
            // profileEditorStaticTimestampTypeNone
            // 
            profileEditorStaticTimestampTypeNone.AutoSize = true;
            profileEditorStaticTimestampTypeNone.Checked = true;
            profileEditorStaticTimestampTypeNone.Location = new Point(392, 216);
            profileEditorStaticTimestampTypeNone.Name = "profileEditorStaticTimestampTypeNone";
            profileEditorStaticTimestampTypeNone.Size = new Size(54, 19);
            profileEditorStaticTimestampTypeNone.TabIndex = 25;
            profileEditorStaticTimestampTypeNone.TabStop = true;
            profileEditorStaticTimestampTypeNone.Text = "None";
            profileEditorStaticTimestampTypeNone.UseVisualStyleBackColor = true;
            profileEditorStaticTimestampTypeNone.CheckedChanged += StaticPropertiesChangedRadio;
            // 
            // profileEditorStaticTimestampTypeLocalTime
            // 
            profileEditorStaticTimestampTypeLocalTime.AutoSize = true;
            profileEditorStaticTimestampTypeLocalTime.Enabled = false;
            profileEditorStaticTimestampTypeLocalTime.Location = new Point(304, 216);
            profileEditorStaticTimestampTypeLocalTime.Name = "profileEditorStaticTimestampTypeLocalTime";
            profileEditorStaticTimestampTypeLocalTime.Size = new Size(82, 19);
            profileEditorStaticTimestampTypeLocalTime.TabIndex = 24;
            profileEditorStaticTimestampTypeLocalTime.TabStop = true;
            profileEditorStaticTimestampTypeLocalTime.Text = "Local Time";
            profileEditorStaticTimestampTypeLocalTime.UseVisualStyleBackColor = true;
            profileEditorStaticTimestampTypeLocalTime.CheckedChanged += StaticPropertiesChangedRadio;
            // 
            // profileEditorStaticTimestampTypeCustom
            // 
            profileEditorStaticTimestampTypeCustom.AutoSize = true;
            profileEditorStaticTimestampTypeCustom.Location = new Point(231, 216);
            profileEditorStaticTimestampTypeCustom.Name = "profileEditorStaticTimestampTypeCustom";
            profileEditorStaticTimestampTypeCustom.Size = new Size(67, 19);
            profileEditorStaticTimestampTypeCustom.TabIndex = 23;
            profileEditorStaticTimestampTypeCustom.TabStop = true;
            profileEditorStaticTimestampTypeCustom.Text = "Custom";
            profileEditorStaticTimestampTypeCustom.UseVisualStyleBackColor = true;
            profileEditorStaticTimestampTypeCustom.CheckedChanged += StaticPropertiesChangedRadio;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(111, 216);
            label9.Name = "label9";
            label9.Size = new Size(12, 15);
            label9.TabIndex = 22;
            label9.Text = "-";
            // 
            // profileEditorStaticTimestampEnd
            // 
            profileEditorStaticTimestampEnd.Location = new Point(129, 213);
            profileEditorStaticTimestampEnd.Name = "profileEditorStaticTimestampEnd";
            profileEditorStaticTimestampEnd.Size = new Size(96, 23);
            profileEditorStaticTimestampEnd.TabIndex = 21;
            profileEditorStaticTimestampEnd.TextChanged += StaticPropertiesChanged;
            // 
            // profileEditorStaticTimestampStart
            // 
            profileEditorStaticTimestampStart.Location = new Point(9, 213);
            profileEditorStaticTimestampStart.Name = "profileEditorStaticTimestampStart";
            profileEditorStaticTimestampStart.Size = new Size(96, 23);
            profileEditorStaticTimestampStart.TabIndex = 20;
            profileEditorStaticTimestampStart.TextChanged += StaticPropertiesChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 195);
            label8.Name = "label8";
            label8.Size = new Size(71, 15);
            label8.TabIndex = 19;
            label8.Text = "Timestamps";
            // 
            // profileEditorStaticSmallText
            // 
            profileEditorStaticSmallText.Location = new Point(9, 169);
            profileEditorStaticSmallText.Name = "profileEditorStaticSmallText";
            profileEditorStaticSmallText.Size = new Size(594, 23);
            profileEditorStaticSmallText.TabIndex = 18;
            profileEditorStaticSmallText.TextChanged += StaticPropertiesChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 151);
            label7.Name = "label7";
            label7.Size = new Size(96, 15);
            label7.TabIndex = 17;
            label7.Text = "Small Image Text";
            // 
            // profileEditorStaticSmallImage
            // 
            profileEditorStaticSmallImage.Location = new Point(9, 125);
            profileEditorStaticSmallImage.Name = "profileEditorStaticSmallImage";
            profileEditorStaticSmallImage.Size = new Size(594, 23);
            profileEditorStaticSmallImage.TabIndex = 16;
            profileEditorStaticSmallImage.TextChanged += StaticPropertiesChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 107);
            label6.Name = "label6";
            label6.Size = new Size(120, 15);
            label6.TabIndex = 15;
            label6.Text = "Small Image Key/URL";
            // 
            // profileEditorStaticLargeText
            // 
            profileEditorStaticLargeText.Location = new Point(9, 81);
            profileEditorStaticLargeText.Name = "profileEditorStaticLargeText";
            profileEditorStaticLargeText.Size = new Size(594, 23);
            profileEditorStaticLargeText.TabIndex = 14;
            profileEditorStaticLargeText.TextChanged += StaticPropertiesChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 63);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 13;
            label5.Text = "Large Image Text";
            // 
            // profileEditorStaticLargeImage
            // 
            profileEditorStaticLargeImage.Location = new Point(9, 37);
            profileEditorStaticLargeImage.Name = "profileEditorStaticLargeImage";
            profileEditorStaticLargeImage.Size = new Size(594, 23);
            profileEditorStaticLargeImage.TabIndex = 12;
            profileEditorStaticLargeImage.TextChanged += StaticPropertiesChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 19);
            label4.Name = "label4";
            label4.Size = new Size(120, 15);
            label4.TabIndex = 11;
            label4.Text = "Large Image Key/URL";
            // 
            // profileEditorClientId
            // 
            profileEditorClientId.Location = new Point(6, 75);
            profileEditorClientId.Name = "profileEditorClientId";
            profileEditorClientId.Size = new Size(179, 23);
            profileEditorClientId.TabIndex = 7;
            profileEditorClientId.TextChanged += profileEditorClientId_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 57);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Client Id";
            // 
            // profileEditorRemove
            // 
            profileEditorRemove.Location = new Point(517, 6);
            profileEditorRemove.Name = "profileEditorRemove";
            profileEditorRemove.Size = new Size(98, 23);
            profileEditorRemove.TabIndex = 5;
            profileEditorRemove.Text = "Delete Profile";
            profileEditorRemove.UseVisualStyleBackColor = true;
            profileEditorRemove.Click += profileEditorRemove_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(216, 6);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 4;
            label2.Text = "Profile Type";
            // 
            // profileEditorTypeDynamic
            // 
            profileEditorTypeDynamic.AutoSize = true;
            profileEditorTypeDynamic.Location = new Point(276, 25);
            profileEditorTypeDynamic.Name = "profileEditorTypeDynamic";
            profileEditorTypeDynamic.Size = new Size(72, 19);
            profileEditorTypeDynamic.TabIndex = 3;
            profileEditorTypeDynamic.TabStop = true;
            profileEditorTypeDynamic.Text = "Dynamic";
            profileEditorTypeDynamic.UseVisualStyleBackColor = true;
            profileEditorTypeDynamic.CheckedChanged += ProfileEditorType_CheckChanged;
            // 
            // profileEditorTypeStatic
            // 
            profileEditorTypeStatic.AutoSize = true;
            profileEditorTypeStatic.Checked = true;
            profileEditorTypeStatic.Location = new Point(216, 25);
            profileEditorTypeStatic.Name = "profileEditorTypeStatic";
            profileEditorTypeStatic.Size = new Size(54, 19);
            profileEditorTypeStatic.TabIndex = 2;
            profileEditorTypeStatic.TabStop = true;
            profileEditorTypeStatic.Text = "Static";
            profileEditorTypeStatic.UseVisualStyleBackColor = true;
            profileEditorTypeStatic.CheckedChanged += ProfileEditorType_CheckChanged;
            // 
            // profileEditorName
            // 
            profileEditorName.Location = new Point(6, 24);
            profileEditorName.Name = "profileEditorName";
            profileEditorName.Size = new Size(179, 23);
            profileEditorName.TabIndex = 1;
            profileEditorName.TextChanged += profileEditorName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 6);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 0;
            label1.Text = "Profile Name";
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // profileConnect
            // 
            profileConnect.Location = new Point(16, 440);
            profileConnect.Name = "profileConnect";
            profileConnect.Size = new Size(91, 23);
            profileConnect.TabIndex = 3;
            profileConnect.Text = "Connect";
            profileConnect.UseVisualStyleBackColor = true;
            profileConnect.Click += profileConnect_Click;
            // 
            // profileDisconnect
            // 
            profileDisconnect.Enabled = false;
            profileDisconnect.Location = new Point(113, 440);
            profileDisconnect.Name = "profileDisconnect";
            profileDisconnect.Size = new Size(91, 23);
            profileDisconnect.TabIndex = 4;
            profileDisconnect.Text = "Disconnect";
            profileDisconnect.UseVisualStyleBackColor = true;
            profileDisconnect.Click += profileDisconnect_Click;
            // 
            // profileStatus
            // 
            profileStatus.Location = new Point(210, 441);
            profileStatus.Name = "profileStatus";
            profileStatus.Size = new Size(336, 23);
            profileStatus.TabIndex = 5;
            profileStatus.Text = "Idle";
            profileStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // profileSwitch
            // 
            profileSwitch.Enabled = false;
            profileSwitch.Location = new Point(546, 440);
            profileSwitch.Name = "profileSwitch";
            profileSwitch.Size = new Size(91, 23);
            profileSwitch.TabIndex = 6;
            profileSwitch.Text = "Switch";
            profileSwitch.UseVisualStyleBackColor = true;
            profileSwitch.Click += profileSwitch_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 467);
            Controls.Add(profileSwitch);
            Controls.Add(profileStatus);
            Controls.Add(profileDisconnect);
            Controls.Add(profileConnect);
            Controls.Add(profilePanel);
            Controls.Add(profilesTabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AfterRichPresence";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            trayContextMenuStrip.ResumeLayout(false);
            profilesTabControl.ResumeLayout(false);
            profilePanel.ResumeLayout(false);
            profilePanel.PerformLayout();
            profileEditorDynamicGroup.ResumeLayout(false);
            profileEditorDynamicGroup.PerformLayout();
            profileEditorStaticGroup.ResumeLayout(false);
            profileEditorStaticGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayContextMenuStrip;
        private ToolStripMenuItem trayProfile;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem trayExit;
        private TabControl profilesTabControl;
        private TabPage profilesDefault;
        private TabPage profilesAddPage;
        private Panel profilePanel;
        private TextBox profileEditorName;
        private Label label1;
        private ErrorProvider errorProvider;
        private Label label2;
        private RadioButton profileEditorTypeDynamic;
        private RadioButton profileEditorTypeStatic;
        private Button profileEditorRemove;
        private TextBox profileEditorClientId;
        private Label label3;
        private GroupBox profileEditorStaticGroup;
        private RichTextBox profileEditorDynamicLua;
        private Label profileStatus;
        private Button profileDisconnect;
        private Button profileConnect;
        private LinkLabel applicationsLinkLabel;
        private TextBox profileEditorStaticSmallText;
        private Label label7;
        private TextBox profileEditorStaticSmallImage;
        private Label label6;
        private TextBox profileEditorStaticLargeText;
        private Label label5;
        private TextBox profileEditorStaticLargeImage;
        private Label label4;
        private Label label9;
        private TextBox profileEditorStaticTimestampEnd;
        private TextBox profileEditorStaticTimestampStart;
        private Label label8;
        private RadioButton profileEditorStaticTimestampTypeNone;
        private RadioButton profileEditorStaticTimestampTypeLocalTime;
        private RadioButton profileEditorStaticTimestampTypeCustom;
        private TextBox profileEditorStaticState;
        private Label label11;
        private TextBox profileEditorStaticDetails;
        private Label label10;
        private GroupBox profileEditorDynamicGroup;
        private Label label12;
        private Label label13;
        private CheckedListBox profileEditorDynamicTriggers;
        private Label label14;
        private RichTextBox profileEditorDynamicOutput;
        private LinkLabel docsLinkLabel;
        private LinkLabel luaPopoutLinkLabel;
        private Button profileEditorDynamicRefresh;
        private Button profileSwitch;
        private ToolStripMenuItem trayConnected;
        private TextBox timerInput;
        private Label timerLeft;
        private ComboBox timerUnit;
        private Label label15;
    }
}