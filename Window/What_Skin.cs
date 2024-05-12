using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher.Window
{
    public partial class What_Skin : Form
    {
        public What_Skin()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                File.WriteAllText("RMCL\\Skin_Lx.txt", "W");
            }
            else
            {
                File.WriteAllText("RMCL\\Skin_Lx.txt", "S");
            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
