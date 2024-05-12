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
    public partial class Christmas_Happy : Form
    {
        public Christmas_Happy()
        {
            InitializeComponent();
        }

        private void Christmas_Happy_Load(object sender, EventArgs e)
        {
            int iActulaWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            int iActulaHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Location=new Point(iActulaWidth-Width, iActulaHeight-Height-48);
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            Show_Window Show_Window = new Show_Window(i);
            Show_Window.Show();
            i++;
        }
    }
}
