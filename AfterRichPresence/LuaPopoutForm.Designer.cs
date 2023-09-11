namespace AfterRichPresence
{
    partial class LuaPopoutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            luaBox = new RichTextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // luaBox
            // 
            luaBox.AcceptsTab = true;
            luaBox.DetectUrls = false;
            luaBox.Dock = DockStyle.Top;
            luaBox.Location = new Point(0, 0);
            luaBox.Name = "luaBox";
            luaBox.Size = new Size(719, 347);
            luaBox.TabIndex = 0;
            luaBox.Text = "";
            luaBox.WordWrap = false;
            luaBox.TextChanged += luaBox_TextChanged;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(632, 353);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Done";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // LuaPopoutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(719, 388);
            Controls.Add(button1);
            Controls.Add(luaBox);
            Name = "LuaPopoutForm";
            Text = "AfterRichPresence Lua Editor";
            Resize += LuaPopoutForm_Resize;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox luaBox;
        private Button button1;
    }
}