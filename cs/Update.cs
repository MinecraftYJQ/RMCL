using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.cs
{
    internal class Update
    {
        public Update(bool Win=false)
        {
            RMCL_internal.console.WriteLine("正在检查更新...更新服务器:http://minecraftyjq.github.io/assets/download/RMCL");
            
            string version = GetResponse("http://minecraftyjq.github.io/assets/download/RMCL", Win);
            RMCL_internal.console.WriteLine(version);

            if(version!= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                if(version!=null)
                {
                    Update_Window update = new Update_Window($"当前RMCL有更新可用！\n当前版本{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}\n最新版本{version}");
                    update.ShowDialog();
                }
            }
            else
            {
                if (Win)
                {
                    System.Windows.Forms.MessageBox.Show("当前RMCL无更新！已是最新版。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        public string GetResponse(string Url,bool Win=false)
        {
            string ResponseData = string.Empty;
            try
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
                httpWebRequest.Timeout = 5000000;
                httpWebRequest.Method = "GET";

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                ResponseData = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

            }
            catch (Exception ex)
            {
                RMCL_internal.console.WriteLine("更新异常!");
                if(Win) {
                    System.Windows.Forms.MessageBox.Show("更新异常！似乎未联网！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                ResponseData = null;
            }
            return ResponseData;
        }
    }
}
