using Launcher.cs;
using Launcher.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Command command1 = new Command("md RMCL",true);
            if (File.Exists("RMCL\\InitializeComponent.txt"))
            {
                if (File.ReadAllText("RMCL\\InitializeComponent.txt") == "Loding...")
                {
                    Regedit_RMCL_Write regedit_RMCL_Write = new Regedit_RMCL_Write();
                    File.WriteAllText("RMCL\\InitializeComponent.txt", "OK");
                }
            }
            if (args.Length != 0)
            {
                for(int i=0;i<args.Length; i++)
                {
                    RMCL_internal.console.WriteLine("["+i.ToString()+"]:"+args[i]);
                }
                if (args[0] == "-h")
                {
                    MessageBox.Show("RMCL参数启动程序使用：\n" +
                                    "-h\n" +
                                    "显示帮助程序\n" +
                                    "-l\n" +
                                    "使用默认设置启动Minecraft\n" +
                                    "-l <玩家名> <游戏版本>\n" +
                                    "启动Minecraft\n" +
                                    "-s <选项> <内容>\n" +
                                    "设置某些内容:\n" +
                                    "-s name <玩家名>\n" +
                                    "-s javapath <Java路径>\n" +
                                    "-s version <游戏版本>\n" +
                                    "[*]:如果内容含有空格，需要在内容两边加上英文版的双引号！", "RMCL参数启动程序 帮助", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }else if (args[0] == "-l")
                {
                    if (args.Length>1)
                    {
                        string[] subDirectories = null;

                        try
                        {
                            string folderPath = ".minecraft\\versions";
                            subDirectories = Directory.GetDirectories(folderPath);
                            int i = 0;
                            foreach (string subDir in subDirectories)
                            {
                                i++;
                                string folderName = new DirectoryInfo(subDir).Name;
                                RMCL_internal.console.WriteLine($"当前:{folderName}");
                                if (folderName == args[2])
                                {
                                    Task.Run(() =>
                                    {
                                        Launcher_Main launcher = new Launcher_Main(File.ReadAllText("RMCL\\Size.txt"), args[1], File.ReadAllText("RMCL\\Java_Path.txt"), i - 1);
                                    });
                                    Thread.Sleep(1000);
                                    RMCL_internal.console.WriteLine("OK");
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            RMCL_internal.console.WriteLine("RMCL无权限或Versions文件夹不存在！");
                        }
                    }
                    else
                    {
                        string[] subDirectories = null;

                        try
                        {
                            string folderPath = ".minecraft\\versions";
                            subDirectories = Directory.GetDirectories(folderPath);
                            int i = 0;
                            foreach (string subDir in subDirectories)
                            {
                                i++;
                                string folderName = new DirectoryInfo(subDir).Name;
                                RMCL_internal.console.WriteLine($"当前:{folderName}");
                                if (folderName == File.ReadAllText("RMCL\\Game_Version.txt"))
                                {
                                    Task.Run(() =>
                                    {
                                        Launcher_Main launcher = new Launcher_Main(File.ReadAllText("RMCL\\Size.txt"), File.ReadAllText("RMCL\\Player_Name.txt"), File.ReadAllText("RMCL\\Java_Path.txt"), i - 1);
                                    });
                                    Thread.Sleep(1000);
                                    RMCL_internal.console.WriteLine("OK");
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            RMCL_internal.console.WriteLine("RMCL无权限或Versions文件夹不存在！");
                        }
                    }
                    

                }
                else if (args[0] == "-s")
                {
                    if (args[1] == "name")
                    {
                        File.WriteAllText("RMCL\\Player_Name.txt", args[2]);
                    }else if (args[1] == "javapath")
                    {
                        File.WriteAllText("RMCL\\Java_Path.txt", args[2]);
                    }else if (args[1] == "version")
                    {
                        File.WriteAllText("RMCL\\Game_Version.txt", args[2]);
                    }
                }
            }
            else
            {
                try
                {
                    string GUI = File.ReadAllText("RMCL\\GUI.txt");
                    if (GUI == "false")
                    {
                        try
                        {
                            string programPath = "RMCL\\RMCL_Command\\RMCL_Command.exe";

                            // 创建一个新的进程对象
                            Process process = new Process();

                            // 设置要启动的程序路径
                            process.StartInfo.FileName = programPath;

                            // 启动外部程序
                            process.Start();
                        }
                        catch
                        {
                            /*Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);*/
                            Command command = new Command("md RMCL\\RMCL_Command",true);
                            Download_RMCL_Command download_RMCL_Command = new Download_RMCL_Command();
                            download_RMCL_Command.ShowDialog();
                        }
                        Thread.Sleep(1000);
                        System.Environment.Exit(0);
                    }
                    else
                    {
                         /*try
                         {
                             Application.EnableVisualStyles();
                             Application.Run(new MainForm());
                         }
                         catch
                         {
                             MessageBox.Show("很抱歉，RMCL出现了严重错误!\n也许这个错误开发者也救不了罢...", "RMCL - 错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }*/
                        Application.EnableVisualStyles();
                        Application.Run(new MainForm());
                    }
                }catch {
                    /*try
                    {
                        Application.EnableVisualStyles();
                        Application.Run(new MainForm());
                    }
                    catch
                    {
                        MessageBox.Show("很抱歉，RMCL出现了严重错误!\n也许这个错误开发者也救不了罢...", "RMCL - 错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
                    Application.EnableVisualStyles();
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
