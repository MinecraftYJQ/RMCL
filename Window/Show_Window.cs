using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.Window
{
    public partial class Show_Window : Form
    {
        int isss;

        public Show_Window(int iss)
        {
            InitializeComponent();
            isss = iss;
        }

        private void Show_Window_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            int i = random.Next(10, 30);
            Width = i;
            Height = i;
            timer1.Interval = random.Next(10, 50);
            int iActulaWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            int iActulaHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(random.Next(0, iActulaWidth),0);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Location=new Point(Location.X, Location.Y+5);
            int iActulaHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            if(Location.Y>iActulaHeight)
            {
                RMCL_internal.console.WriteLine("["+isss + "]:" + Width);
                this.Close();
            }
            if (Width <= 15)
            {
                if(Location.Y > iActulaHeight -  iActulaHeight / 3)
                {
                    RMCL_internal.console.WriteLine("[" + isss + "]:" + Width);
                    this.Close();
                }
            }
        }
        int i = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            Location = new Point(Location.X+1, Location.Y);
            i++;
            if (i == 20)
            {
                timer2.Enabled=false;
                timer3.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Location = new Point(Location.X - 1, Location.Y);
            i--;
            if (i == 0)
            {
                timer2.Enabled = true;
                timer3.Enabled = false;
            }
        }
    }
}
