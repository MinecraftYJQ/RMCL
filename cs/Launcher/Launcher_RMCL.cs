using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Media3D;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.cs.Launcher
{
    internal class Launcher_RMCL
    {
        public Launcher_RMCL(string version,string java,string name,string size,int ms) {

            //Launcher_Main launcher_Main = new Launcher_Main(size, name, java, ms);
            try
            {
                int z1, z2;
                Screen mainScreen = Screen.PrimaryScreen;

                int screenWidth = mainScreen.Bounds.Width;
                int screenHeight = mainScreen.Bounds.Height;
                if (size == "默认大小 | Default size")
                {
                    z1 = (screenHeight - 180);
                    z2 = (screenWidth - 100);
                }
                else
                {
                    z1 = 520;
                    z2 = 870;
                }
                //RMCL_internal.console.WriteLine(File.ReadAllText($".minecraft\\versions\\{version}\\{version}.json"));
                string json = System.IO.File.ReadAllText($".minecraft\\versions\\{version}\\{version}.json");
                JObject jsonObject = JObject.Parse(json);
                dynamic data = JsonConvert.DeserializeObject(json);
                string assetIndexId = data.assetIndex.id;
                RMCL_internal.console.WriteLine("Asset Index ID: " + assetIndexId);
                // 提取 "libraries" 数组
                JArray libraries = (JArray)jsonObject["libraries"];
                string pathss = System.IO.Directory.GetCurrentDirectory();
                RMCL_internal.console.WriteLine($"{pathss}");
                string b = $"\" net.minecraft.client.main.Main --username {name} --version {version} --gameDir .minecraft --assetsDir .minecraft\\assets --assetIndex {assetIndexId} --uuid 52428a0e1e303cb1976ce728b2614047 --accessToken effc7e828ece422fb9db380ebc27042e --userType legacy --versionType \"RMCL\" --width {z2} --height {z1}";
                string a = $"\"{java}\" -Xmx1024m -Dfile.encoding=GB18030 -Dsun.stdout.encoding=GB18030 -Dsun.stderr.encoding=GB18030 -Djava.rmi.server.useCodebaseOnly=true -Dcom.sun.jndi.rmi.object.trustURLCodebase=false -Dcom.sun.jndi.cosnaming.object.trustURLCodebase=false -Dlog4j2.formatMsgNoLookups=true -Dlog4j.configurationFile=\"{pathss}\\.minecraft\\versions\\{version}\\log4j2.xml\" -Dminecraft.client.jar=\"{pathss}\\.minecraft\\versions\\{version}\\{version}.jar\" -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32m -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -XX:-DontCompileHugeMethods -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Djava.library.path=\"{pathss}\\.minecraft\\versions\\{version}\\natives-windows-x86_64\" -Dminecraft.launcher.brand=RMCL -cp \"";
                // 遍历数组并提取 "artifact" 值
                try
                {
                    foreach (JObject library in libraries)
                    {
                        JObject downloads = (JObject)library["downloads"];
                        JObject artifact = (JObject)downloads["artifact"];
                        string path = artifact["path"].ToString();
                        a += $".minecraft\\libraries\\" + path + ";";
                    }
                }
                catch {
                }
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

                // 生成8位长度的随机密文
                string randomString = new string(Enumerable.Repeat(chars, 50)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                //RMCL_internal.console.WriteLine(a+b);
                File.WriteAllText($"RMCL\\temp\\{randomString}.bat", "@echo off&cls&"+a + $"{pathss}\\.minecraft\\versions\\{version}\\{version}.jar" + b);
                string batchFilePath = $"RMCL\\temp\\{randomString}.bat";

                /*// 创建 ProcessStartInfo 对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + batchFilePath;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();*/
                Task.Run(() =>
                {
                    ExecuteCommand(batchFilePath);
                });
            }
            catch { }
        
        }
        static async Task ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + command);
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process process = Process.Start(psi))
            {
                if (process != null)
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            RMCL_internal.console.WriteLine(e.Data,false);
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            RMCL_internal.console.WriteLine("Error: " + e.Data);
                        }
                    };

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
            }
        }
    }
}
