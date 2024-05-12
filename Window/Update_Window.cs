using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher.cs
{
    public partial class Update_Window : Form
    {
        public Update_Window(string xx)
        {
            InitializeComponent();
            label1.Text = label1.Text + xx;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = global::Launcher.Properties.Resources.Update_RMCL;
            using (FileStream fs = new FileStream("RMCL\\Update_RMCL.exe", FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            Command command = new Command("RMCL\\Update_RMCL.exe",true);
            Thread.Sleep(50);
            Environment.Exit(0);
        }

        private void Update_Window_Load(object sender, EventArgs e)
        {

        }
    }
}
