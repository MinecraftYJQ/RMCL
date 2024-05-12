using Launcher.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.Window
{
    public partial class Download_RMCL_Frp_New : Form
    {
        public Download_RMCL_Frp_New()
        {
            InitializeComponent();
        }

        private void Download_RMCL_Frp_New_Load(object sender, EventArgs e)
        {
            ProgressBar.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
            Task.Run(() =>
            {
                DownloadFile("https://minecraftyjq.github.io/assets/download/RMCL_WAN_Online/frps/frps.exe", "RMCL\\RMCL_WAN_Online\\frps\\frps.exe", progressBar1, label1);
            });
            Task.Run(() =>
            {
                DownloadFile("https://minecraftyjq.github.io/assets/download/RMCL_WAN_Online/frpc/frpc.exe", "RMCL\\RMCL_WAN_Online\\frpc\\frpc.exe", progressBar2, label2);
            });
            Task.Run(() =>
            {
                DownloadFile("https://minecraftyjq.github.io/assets/download/RMCL_WAN_Online/Main/RMCL_Frp_New.exe", "RMCL\\RMCL_WAN_Online\\Main\\RMCL_Frp_New.exe", progressBar3, label3);
            });
        }
        
        int wc = 0;
        private void DownloadFile(string URL, string filename, ProgressBar prog, Label label)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    label.Text = percent.ToString() + "%";
                    if (percent.ToString() == "100")
                    {
                        label.Text = "下载完成!";
                    }
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                    //RMCL_internal.console.WriteLine(label1.Text);
                }
                so.Close();
                st.Close();
                wc++;
                if (wc == 3)
                {
                    Command command = new Command("RMCL\\RMCL_WAN_Online\\Main\\RMCL_Frp_New.exe -rmcl -start -ljmk", true);
                    Thread.Sleep(1000);
                    this.Close();
                }
            }
            catch (System.Exception)
            {
                RMCL_internal.console.WriteLine("引发异常");
                throw;
            }
        }
    }
}
