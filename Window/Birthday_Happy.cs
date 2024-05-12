using Launcher.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher.Window
{
    public partial class Birthday_Happy : Form
    {
        public Birthday_Happy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closes = false;
            Opacity = 0;
            MessageBox.Show("这么没诚意，不行！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Opacity = 100;
            js = 4;
            label6.Text = "还剩下:" + (js + 1).ToString() + "s";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Opacity = 0;
            MessageBox.Show("感谢你的祝福！她收到了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }

        int js = 4;
        bool closes = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text="还剩下:"+js.ToString()+"s";
            if(js == 0 )
            {
                if (closes)
                {
                    Close();
                }
            }
            js--;
        }

        private void Birthday_Happy_Load(object sender, EventArgs e)
        {
            label6.Text = "还剩下:" + (js+1).ToString() + "s";
        }
    }
}
