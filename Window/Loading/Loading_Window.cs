using Launcher.cs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.Window.Loading
{
    public partial class Loading_Window : Form
    {
        public static string[] java_path = new string[25565];
        public Loading_Window()
        {
            InitializeComponent();
        }
        private string command(string str)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(str + "&exit");
                string output = p.StandardOutput.ReadToEnd();
                p.StandardInput.AutoFlush = true;
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void AppDomainManagerInitializationOptions()
        {
            // 确定当前进程是否已经以管理员权限运行
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))//如果假，那么运行未触发
            {
                RMCL_internal.console.WriteLine("触发UAC!");
                // 准备启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Verb = "runas"; // 触发UAC

                // 启动新进程
                Process.Start(startInfo);

                File.WriteAllText("RMCL\\InitializeComponent.txt", "Loding...");
                Thread.Sleep(1);
                // 当前进程将关闭
                Application.Exit();
            }
        }
        bool ass = true;
        private void Loading_Window_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if(ass) {
                        RMCL_internal.debug_x = Location.X + Width;
                        RMCL_internal.debug_y = Location.Y + 0;
                        RMCL_internal.debug_h = Height;
                        RMCL_internal.debug_w = Width;
                    }
                }
            });
            if (File.Exists("RMCL\\Debug.txt"))
            {
                if (File.ReadAllText("RMCL\\Debug.txt") == "True")
                {
                    Debug_Window debug_Window = new Debug_Window();
                    debug_Window.Show();
                }
            }
            if (!File.Exists("RMCL\\InitializeComponent.txt"))
            {
                RMCL_internal.console.WriteLine("未初始化!");
                try
                {
                    AppDomainManagerInitializationOptions();
                }
                catch (Exception ex){}
            }
            Random random = new Random();
            int ee=random.Next(0,6);
            RMCL_internal.console.WriteLine(ee);
            if (ee == 1)
            {
                pictureBox1.BackgroundImage = global::Launcher.Properties.Resources._1;
            }
            else if (ee == 2)
            {
                pictureBox1.BackgroundImage = global::Launcher.Properties.Resources._2;
            }
            else if (ee == 3)
            {
                pictureBox1.BackgroundImage = global::Launcher.Properties.Resources._3;
            }
            else if (ee == 42)
            {
                pictureBox1.BackgroundImage = global::Launcher.Properties.Resources._4;
            }
            else if (ee == 5)
            {
                pictureBox1.BackgroundImage = global::Launcher.Properties.Resources._5;
            }
            Loading_Window.CheckForIllegalCrossThreadCalls = false;
            Task.Run(() =>
            {
                label1.Text = "[Main]:创建RMCL文件夹...";
                if (!Directory.Exists("RMCL"))
                {
                    Directory.CreateDirectory("RMCL");
                    RMCL_internal.console.WriteLine("文件夹已创建");
                }
                label1.Text = "[Main]:创建RMCL\\temp文件夹...";
                if (!Directory.Exists("RMCL\\temp"))
                {
                    Directory.CreateDirectory("RMCL\\temp");
                    RMCL_internal.console.WriteLine("文件夹已创建");
                }
                Command command1 = new Command("md RMCL", true);
                Command command2 = new Command("md RMCL\\RMCL_Command", true);
                label1.Text = "[Main]:读取Java路径...";
                RMCL_internal.console.WriteLine(command("where javaw").Substring(command("where javaw").IndexOf("&exit") + 6));
                string[] lines = command("where javaw").Substring(command("where javaw").IndexOf("&exit") + 6).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    RMCL_internal.java_path[RMCL_internal.java_path_js] = line;
                    RMCL_internal.java_path_js++;
                }
                label1.Text = "[Main]:获取Mojang官方下载版本Json文件...";
                File.WriteAllText("RMCL\\Mojang.txt", GetResponse("https://launchermeta.mojang.com/mc/game/version_manifest.json"));
                string ip_api_return = command("curl http://ip-api.com/json/");

                // 解析JSON字符串
                JObject jsonObject = JObject.Parse(ip_api_return.Substring(ip_api_return.IndexOf("&exit") + 5));

                // 提取city、regionName和region字段
                string city = jsonObject["city"].ToString();
                string regionName = jsonObject["regionName"].ToString();
                string region = jsonObject["region"].ToString();

                RMCL_internal.console.WriteLine("城市: " + city);
                RMCL_internal.console.WriteLine("区域: " + regionName);
                RMCL_internal.console.WriteLine("地区: " + region);

                RMCL_internal.user_dq = $"当前所在地区:{regionName} · {city}";


                //Asia_Ens.Text= RMCL_internal.sw.ToString();
                /* // 根据city输出前城市的中文
                // 这里需要一个映射关系，将英文城市名映射到中文城市名
                // 由于这个映射关系可能很复杂，这里仅提供一个简单示例
                var cityToChinese = new Dictionary<string, string>
                {
                    { "Guangzhou", "广州" },
                    // 可以添加更多的映射关系
                };

                string previousCityChinese = cityToChinese.ContainsKey(jsonObject["city"].ToString()) ? cityToChinese[jsonObject["city"].ToString()] : "未知城市";
                RMCL_internal.console.WriteLine("Previous City in Chinese: " + previousCityChinese);
                */
                ass = false;
                Close();
            });
        }

        public string GetResponse(string Url)
        {
            string ResponseData = string.Empty;
            try
            {
                //Message_fs message = new Message_fs("RMCL启动器", "正在加载下载版本...");
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
                httpWebRequest.Timeout = 5000;
                httpWebRequest.Method = "GET";

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                ResponseData = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
            }
            catch (Exception)
            {
                ResponseData = null;
            }
            return ResponseData;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
