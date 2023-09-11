using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AfterRichPresence
{
    public partial class LuaPopoutForm : Form
    {
        public LuaPopoutForm()
        {
            InitializeComponent();
        }

        Action<string>? luaChanged;

        public LuaPopoutForm(string startingLua, Action<string>? luaChanged)
        {
            InitializeComponent();
            luaBox.Text = startingLua;
            this.luaChanged = luaChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            luaChanged = null;
            Close();
        }

        private void luaBox_TextChanged(object sender, EventArgs e)
        {
            luaChanged?.Invoke(luaBox.Text);
        }

        private void LuaPopoutForm_Resize(object sender, EventArgs e)
        {
            luaBox.Size = new Size(luaBox.Size.Width, Size.Height - 80);
        }
    }
}
