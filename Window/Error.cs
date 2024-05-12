using Launcher.cs;
using Launcher.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class Error : Form
    {
        string ex;
        //zl用法:
        //传参1=cmcl.json文件丢失
        //传参2=hx.exe丢失
        //传参3=文件夹错误
        //传参4=数组越界
        public Error(int zl,string exs)
        {
            if (zl == 0)
            {
                cwm = "错误码:e" + zl;
                text = "RMCL初始化错误！\r\n\r\nRMCL initialization error!";
            }
            if (zl == 1) {
                cwm = "错误码:e" + zl;
                text = "检测到配置文件受损，请您立即重新安装该程序以解决问题。如果无法解决此问题，请联系开发者。\r\n\r\nIf a corrupted profile is detected, please reinstall the program immediately to resolve the issue. If you can't resolve the issue, contact the developer.";
            }
            else if (zl == 2)
            {
                cwm = "错误码:e" + zl;
                text = "核心文件被删除，请立即重新安装！如多次无法解决，请联系开发者！\r\n\r\nCore files are deleted, please reinstall now! If you can't solve it multiple times, please contact the developer!";
            } else if (zl == 3)
            {
                cwm = "错误码:e" + zl;
                text = "文件夹名称出现以下字符：\\ / : * ? \" < > | 等符号！\r\n\r\nError in path: nThe following characters appear in the folder name:  \\ / : * ? \" < > | and other symbols!";
            }else if (zl == 4)
            {
                cwm = "错误码:e" + zl;
                text = "出现了致命错误！索引超出了数组越界。\r\n\r\nA fatal error has occurred! The index is out of bounds beyond the array.";
            }
            ex = exs;
            InitializeComponent();
        }
        string text=null,cwm=null;
        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Error_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible)
            {
                button2.Text = "详细信息";
                /*Size = new Size(620, 400);
                textBox1.Visible = false;
                Location = new Point(Location.X, Location.Y + 150);*/
                textBox1.Visible = false;
                h2.Enabled = true;

            }
            else
            {
                button2.Text = "收起";
                /*Size = new Size(620, 700);
                textBox1.Visible = true;
                textBox1.Text = ex;
                Location = new Point(Location.X, Location.Y - 150);*/
                h1.Enabled = true;
            }
        }
        int i = 0;
        private void h1_Tick(object sender, EventArgs e)
        {
            h2.Enabled = false;
            i++;
            Size = new Size(620, Height+20);
            Location = new Point(Location.X, Location.Y - 10);
            if (i == 15)
            {
                textBox1.Visible = true;
                textBox1.Text = ex;
                h1.Enabled = false;
            }
        }
        
        private void h2_Tick(object sender, EventArgs e)
        {
            h1.Enabled = false;
            i--;
            Location = new Point(Location.X, Location.Y +10);
            Size = new Size(620, Height - 20);
            if (i == 0)
            {
                h2.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Error_Load(object sender, EventArgs e)
        {
            //Message_fs message_Fs = new Message_fs("RMCL异常抛出", "错误信息:" + cwm);
            label2.Text = text;
            label3.Text = cwm;
            //Acrylic_Effect Acrylic_Effect = new Acrylic_Effect((int)this.Handle);
        }
    }
}
