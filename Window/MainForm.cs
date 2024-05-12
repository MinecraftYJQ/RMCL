using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using Launcher.cs;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Policy;
using Launcher.Window;
using Launcher.Window.Loading;
using System.Drawing.Drawing2D;
using static Launcher.cs.qjbl;
using Launcher.cs.Launcher;
using Timer = System.Windows.Forms.Timer;
using System.Windows.Media;
using Launcher.cs.Make_Skin;
using System.Drawing.Imaging;
using System.IO.Compression;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Launcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static string Language_All = "Ch";
        
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
        
        static string Player_Name_str = null;
        int urs = 1;
        string[] url = new string[32767];
        public void button_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            string path = button.Text;
            RMCL_internal.console.WriteLine(path);
            Task.Run(() => {
                command("RMCL\\hx install " + path);
            });
        }
        private void refresh()
        {
            try
            {

                //string jg = GetResponse("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                string jg = File.ReadAllText("RMCL\\Mojang.txt").ToString();
                //RMCL_internal.console.WriteLine(jg);
                //RMCL_internal.console.WriteLine(jg);
                RMCL_internal.console.WriteLine(""+jg.Length);
                int y1 = 10;
                if (jg != null)
                {

                    Dow_List.Controls.Clear();
                    for (int i = 50; i < jg.Length - 3; i++)
                    {
                        if (jg[i] == 'r' && jg[i + 1] == 'e' && jg[i + 2] == 'l' && jg[i + 7] != 'T')
                        {
                            int temp = 0, temp1 = 0;
                            for (int j = i - 40; j <= i - 1; j++)
                            {
                                temp++;
                                if (jg[j] == '\"')
                                {
                                    temp1++;
                                    if (temp1 == 4)
                                    {
                                        Button button = new Button();
                                        Dow_List.Controls.Add(button);
                                        button.Location = new System.Drawing.Point(10, y1);
                                        y1 += 56;
                                        button.MouseClick += new MouseEventHandler(button_MouseClick);
                                        button.Width = Width;
                                        //button.Width = panel1.Width - 20;
                                        button.Height = 46;
                                        button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top)));
                                        //button.Text += "                                                                                                                                    \n此版本暂无介绍";
                                        //button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                                        button.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                                        for (int x = j + 1; x <= j + temp; x++)
                                        {
                                            if (jg[x] == '\"')
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                //RMCL_internal.console.Write(jg[x]);
                                                url[urs] += jg[x];
                                                button.Name += jg[x];
                                                button.Text += jg[x];

                                            }
                                        }
                                        for (int x = j - 19; x <= j; x++)
                                        {
                                            if (jg[x] == '\"')
                                            {
                                                for (int y = j + 35; y <= j; y++)
                                                {
                                                    //RMCL_internal.console.Write(jg[y]);
                                                }
                                                //RMCL_internal.console.WriteLine();
                                            }
                                        }
                                        urs++;


                                        //RMCL_internal.console.WriteLine();
                                        //Thread.Sleep(10);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Dow_List.Controls.Clear();
                }


            }
            catch (Exception ex)
            {
                Dow_List.Controls.Clear();
            }
        }
        private void rot(Control control,float amgle)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height); 
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            Graphics graphics = control.CreateGraphics(); 
            graphics.TranslateTransform(control.Width / 2, control.Height / 2); 
            graphics.RotateTransform(amgle);
            graphics.TranslateTransform(-control.Width / 2, -control.Height / 2); graphics.DrawImage(bitmap, new Point(0, 0));
            graphics.Dispose();
        }


        private void Form1_Load (object sender, EventArgs e)
        {
            Main_Panel.Location = new Point(0, -1);
            Setting_Panel.Location = new Point(-114514, -114514);
            More_Panel.Location = new Point(-114514, -114514);
            Versions_Op_Panel.Location = new Point(-114514, -114514);
            Date_Panel.Location = new Point(-114514, -114514);
            Concerning_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Panel.Location = new Point(-114514, -114514);
            Dow_Panel.Location = new Point(-114514, -114514);
            Panel.CheckForIllegalCrossThreadCalls = false;

            /*Task.Run(() =>
            {
                int i=0;
                while (true)
                {
                    try
                    {
                        rot(Icon_Pac, i);
                    }catch (Exception ex) { }
                    i++;
                    if (i == 360)
                    {
                        i = 0;
                    }
                    //Thread.Sleep(1);
                }
            });*/

            RMCL_internal.debug_log_js = 0;
            Easter_Egg_Trigger easter_Egg_Trigger = new Easter_Egg_Trigger();
            Loading_Window loading_Window = new Loading_Window();
            loading_Window.ShowDialog();
            Task.Run(() =>
            {
                while (true)
                {
                    RMCL_internal.debug_x = Location.X + Width;
                    RMCL_internal.debug_y = Location.Y + 0;
                    RMCL_internal.debug_h = Height;
                    RMCL_internal.debug_w = 480;
                }
            });
            Asia_Ens.Text = RMCL_internal.user_dq;
            CheckForIllegalCrossThreadCalls = false;
            Concerning_Text_Title.Text = "关于"+Text;
            ComboBox.CheckForIllegalCrossThreadCalls = false;
            Panel.CheckForIllegalCrossThreadCalls=false;
            //refresh();
            Concerning_Text_Text.Text = "程序名:Round Minecraft Launcher\n版本号:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n原作者:Minecraft一角钱";
            Task.Run(() =>
            {
                try
                {
                    string folderPath = ".minecraft\\versions";
                    string[] subDirectories = Directory.GetDirectories(folderPath);
                    foreach (string subDir in subDirectories)
                    {
                        string folderName = new DirectoryInfo(subDir).Name;
                        Setting_Versions.Items.Add(folderName);
                    }
                }
                catch
                {
                    RMCL_internal.console.WriteLine("RMCL无权限或Versions文件夹不存在！");
                }
                try
                {
                    string Language_str = File.ReadAllText("RMCL\\Language.txt");
                    RMCL_internal.console.WriteLine("" + Language_str);
                    Setting_Language.Text = Language_str;
                    Setting_Language_Hs();
                }
                catch
                {
                    Setting_Language.Text = "Chinese-中文（简体）";
                }
                try
                {
                    string Game_Version_str = File.ReadAllText("RMCL\\Game_Version.txt");
                    RMCL_internal.console.WriteLine("" + Game_Version_str);
                    Setting_Versions.Text = Game_Version_str;
                }
                catch { }
                for(int i=0;i< RMCL_internal.java_path_js; i++)
                {
                    Setting_Java.Items.Add(RMCL_internal.java_path[i]);
                }
                if (Setting_Java.Items.Count > 0)
                {
                    Setting_Java.Items.RemoveAt(0);
                    Setting_Java.Items.RemoveAt(Setting_Java.Items.Count - 1);
                }
                try
                {
                    string Java_Path_str = File.ReadAllText("RMCL\\Java_Path.txt");
                    Setting_Java.Text = Java_Path_str;
                }
                catch { }
                try
                {
                    Player_Name_str = File.ReadAllText("RMCL\\Player_Name.txt");
                    Setting_Player_Name.Text = Player_Name_str;
                }
                catch { }
                try
                {
                    Setting_Window_Size.Text = File.ReadAllText("RMCL\\Size.txt").ToString();
                }
                catch
                {
                    File.WriteAllText("RMCL\\Size.txt", "默认大小 | Default size");
                    Setting_Window_Size.Text = File.ReadAllText("RMCL\\Size.txt").ToString();
                }
                try
                {
                    if (File.ReadAllText("RMCL\\Update.txt") == "true")
                    {
                        Task.Run(() => {
                            Update update = new Update();
                        });
                    }
                    else
                    {
                        Update_Setting.Checked = false;
                    }
                }
                catch
                {
                    Task.Run(() => {
                        Update update = new Update();
                    });
                }
                try
                {
                    byte[] bytes = global::Launcher.Properties.Resources.cmcl;
                    using (FileStream fs = new FileStream("RMCL\\hx.exe", FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    bytes = global::Launcher.Properties.Resources.json_CMCL;
                    using (FileStream fs = new FileStream("RMCL\\cmcl.json", FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception ex) { }
                //refresh();
                
            });
            Text = "Round Minecraft Launcher " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Bitmap croppedImages = new Bitmap(global::Launcher.Properties.Resources.Steve);
            croppedImages.Save("RMCL\\Steve.png");
            try
            {
                if (File.ReadAllText("RMCL\\Skin.txt") == "true")
                {
                    // 创建一个Bitmap对象
                    Bitmap originalImage = new Bitmap("RMCL\\Skin.png");

                    // 创建一个新的Bitmap对象，用于存储指定区域的内容
                    Bitmap croppedImage = new Bitmap(8, 8);

                    // 提取指定区域的像素数据
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            System.Drawing.Color pixelColor = originalImage.GetPixel(x + 8, y + 8);
                            croppedImage.SetPixel(x, y, pixelColor);
                        }
                    }

                    // 保存新的图像
                    croppedImage.Save("RMCL\\croppedImage.png");
                    Thread.Sleep(100);
                    // 读取保存的图片
                    Bitmap savedImage = new Bitmap("RMCL\\croppedImage.png");

                    // 创建一个新的Bitmap对象，用于存储放大后的内容
                    Bitmap enlargedImage = new Bitmap(savedImage.Width * 10, savedImage.Height * 10);

                    // 将每个像素放大10倍
                    for (int x = 0; x < savedImage.Width; x++)
                    {
                        for (int y = 0; y < savedImage.Height; y++)
                        {
                            System.Drawing.Color pixelColor = savedImage.GetPixel(x, y);
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    enlargedImage.SetPixel(x * 10 + i, y * 10 + j, pixelColor);
                                }
                            }
                        }
                    }
                    savedImage.Dispose();
                    originalImage.Dispose();
                    croppedImage.Dispose();
                    enlargedImage.Save("RMCL\\Pixel.png");
                    // 将enlargedImage显示在图片框上
                    Icon_Pac.BackgroundImage = enlargedImage;
                }
                else
                {
                    // 创建一个Bitmap对象
                    Bitmap originalImage = new Bitmap("RMCL\\Steve.png");

                    // 创建一个新的Bitmap对象，用于存储指定区域的内容
                    Bitmap croppedImage = new Bitmap(8, 8);

                    // 提取指定区域的像素数据
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            System.Drawing.Color pixelColor = originalImage.GetPixel(x + 8, y + 8);
                            croppedImage.SetPixel(x, y, pixelColor);
                        }
                    }

                    // 保存新的图像
                    croppedImage.Save("RMCL\\croppedImage.png");
                    Thread.Sleep(100);
                    // 读取保存的图片
                    Bitmap savedImage = new Bitmap("RMCL\\croppedImage.png");

                    // 创建一个新的Bitmap对象，用于存储放大后的内容
                    Bitmap enlargedImage = new Bitmap(savedImage.Width * 10, savedImage.Height * 10);

                    // 将每个像素放大10倍
                    for (int x = 0; x < savedImage.Width; x++)
                    {
                        for (int y = 0; y < savedImage.Height; y++)
                        {
                            System.Drawing.Color pixelColor = savedImage.GetPixel(x, y);
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    enlargedImage.SetPixel(x * 10 + i, y * 10 + j, pixelColor);
                                }
                            }
                        }
                    }
                    savedImage.Dispose();
                    originalImage.Dispose();
                    croppedImage.Dispose();
                    enlargedImage.Save("RMCL\\Pixel.png");
                    // 将enlargedImage显示在图片框上
                    Icon_Pac.BackgroundImage = enlargedImage;
                }
            }
            catch
            {
                // 创建一个Bitmap对象
                Bitmap originalImage = new Bitmap("RMCL\\Steve.png");

                // 创建一个新的Bitmap对象，用于存储指定区域的内容
                Bitmap croppedImage = new Bitmap(8, 8);

                // 提取指定区域的像素数据
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        System.Drawing.Color pixelColor = originalImage.GetPixel(x + 8, y + 8);
                        croppedImage.SetPixel(x, y, pixelColor);
                    }
                }

                // 保存新的图像
                croppedImage.Save("RMCL\\croppedImage.png");
                Thread.Sleep(100);
                // 读取保存的图片
                Bitmap savedImage = new Bitmap("RMCL\\croppedImage.png");

                // 创建一个新的Bitmap对象，用于存储放大后的内容
                Bitmap enlargedImage = new Bitmap(savedImage.Width * 10, savedImage.Height * 10);

                // 将每个像素放大10倍
                for (int x = 0; x < savedImage.Width; x++)
                {
                    for (int y = 0; y < savedImage.Height; y++)
                    {
                        System.Drawing.Color pixelColor = savedImage.GetPixel(x, y);
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                enlargedImage.SetPixel(x * 10 + i, y * 10 + j, pixelColor);
                            }
                        }
                    }
                }
                savedImage.Dispose();
                originalImage.Dispose();
                croppedImage.Dispose();
                enlargedImage.Save("RMCL\\Pixel.png");
                // 将enlargedImage显示在图片框上
                Icon_Pac.BackgroundImage = enlargedImage;
            }
            if (File.Exists("RMCL\\Debug.txt"))
            {
                if (File.ReadAllText("RMCL\\Debug.txt") == "True")
                {
                    Debug_Button.Checked= true;
                }
            }
            //rot(label2, 20);
        }

        private void More_Goto_Click(object sender, EventArgs e)
        {
            More_Panel.Location = new Point(0, -1);
            Main_Panel.Location = new Point(-114514, -114514);
        }

        private void Back_Setting_Main_Click(object sender, EventArgs e)
        {
            More_Panel.Location = new Point(0, -1);
            Setting_Panel.Location = new Point(-114514, -114514);
        }

        private void Setting_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setting_Language_Hs();
        }
        private void Setting_Language_Hs()
        {
            if (Setting_Language.SelectedItem.ToString() == "Chinese-中文（简体）")
            {
                Starting_Game.Text = "启动游戏";
                More_Goto.Text = "更多选项";
                Minecraft_Versions.Text = "版本操作";
                Setting_Java_Ts.Text = "Java路径：";
                Setting_Language_Ts.Text = "语言设置：";
                Setting_Version_Ts.Text = "版本设置：";
                Back_Setting_Main.Text = "<返回";
                Setting_Title.Text = "本体设置";
                Setting_Player_Name_Ts.Text = "设置玩家名";
                More_Back.Text = "<返回";
                More_Title.Text = "更多选项";
                Setting_Goto.Text = "本体设置";
                Version_Op_Back.Text = "<返回";
                Version_Op_Title.Text = "版本操作";
                Delete_Version_Button.Text = "删除";
                Date_Back.Text = "<返回";
                Date_Title.Text = "更新历史";
                Update_History_Goto.Text = "更新历史";
                Versions_Op_Ts.Text = "在下方选择版本进行操作：";
                Concerning_Text_Title.Text = "关于 " + Text;
                Concerning_Back.Text = "<返回";
                Concerning_Title.Text = "关于本体";
                Concerning_Goto.Text = "关于本体";
                LAN_Online_Create_Goto.Text = "创建房间";
                LAN_Online_Create_Ts.Text = "Minecraft的联机端口：";
                LAN_Online_Title.Text = "局域网联机";
                LAN_Online_Create_Create.Text = "创建";
                LAN_Online_Back.Text = "<返回";
                LAN_Online_Access_OK_Stop.Text = "退出房间";
                label1.Text = "选择局域网内的房间：";
                TS1.Text = "扫描期间可能会导致系统微卡顿，属于正常现象！";
                LAN_Online_Access_IP_Ts.Text = "房间IP：";
                button1.Text = "局域网联机";
                LAN_Online_Access_Goto.Text = "接入房间";
                Setting_Window_Size_TS.Text = "游戏窗口：";
                Update_Goto.Text = "检查更新";
                Update_Setting.Text = "启动器自动检查更新";
                Dow_Goto.Text = "下载游戏";
                Dow_Title.Text = "下载游戏";
                Dow_Back.Text = "<返回";
                checkBox1.Text = "GUI启动器界面";
                button3.Text = "广域网联机";
                Concerning_Text_Text.Text = "程序名:Round Minecraft Launcher\n版本号:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n原作者:Minecraft一角钱";
                RMCL_internal.console.WriteLine("语言更改为简体中文");
                Language_All = "Ch";
            }
            else
            {
                Starting_Game.Text = "Launch the Minecraft";
                More_Goto.Text = "More";
                Minecraft_Versions.Text = "Version";
                Setting_Java_Ts.Text = "Java path:";
                Setting_Language_Ts.Text = "Language:";
                Setting_Version_Ts.Text = "Version:";
                Back_Setting_Main.Text = "<Back";
                Setting_Title.Text = "Setting";
                Setting_Player_Name_Ts.Text = "Setting Player Name";
                More_Back.Text = "<Back";
                More_Title.Text = "More";
                Setting_Goto.Text = "Setting";
                Version_Op_Back.Text = "<Back";
                Version_Op_Title.Text = "Versions";
                Delete_Version_Button.Text = "Delete";
                Date_Title.Text = "Update";
                Date_Back.Text = "<Back";
                Update_History_Goto.Text = "Update";
                Versions_Op_Ts.Text = "Select a version below:";
                Concerning_Text_Title.Text = "Concerning " + Text;
                Concerning_Back.Text = "<Back";
                Concerning_Title.Text = "Concerning";
                Concerning_Goto.Text = "Concerning";
                LAN_Online_Create_Goto.Text = "Create a room";
                LAN_Online_Create_Ts.Text = "Minecraft's online ports:";
                LAN_Online_Title.Text = "LAN Connection";
                LAN_Online_Create_Create.Text = "Create";
                LAN_Online_Back.Text = "<Back";
                LAN_Online_Access_OK_Stop.Text = "Exit";
                label1.Text = "Select a room within the LAN：";
                TS1.Text = "During the scanning, the system may cause micro-stuttering, which is a normal phenomenon!";
                LAN_Online_Access_IP_Ts.Text = "Room IP:";
                button1.Text = "Online";
                LAN_Online_Access_Goto.Text = "Access";
                Setting_Window_Size_TS.Text = "Window:";
                Update_Goto.Text = "Detect update";
                Update_Setting.Text = "The launcher automatically checks for updates";
                Dow_Goto.Text = "Download the game";
                Dow_Title.Text = "Download the game";
                Dow_Back.Text = "<Back";
                checkBox1.Text = "GUI launcher interface";
                button3.Text = "WAN connection";
                Concerning_Text_Text.Text = "Program Name:Round Minecraft Launcher\nVersion number:" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\nauthor:Minecraft一角钱";
                RMCL_internal.console.WriteLine("语言更改为英语");
                Language_All = "En";
            }

            string content = Setting_Language.SelectedItem.ToString();
            File.WriteAllText("RMCL\\Language.txt", content);

        }
        private void More_Back_Click(object sender, EventArgs e)
        {
            Main_Panel.Location = new Point(0, -1);
            More_Panel.Location = new Point(-114514, -114514);
        }

        private void Setting_Goto_Click(object sender, EventArgs e)
        {
            Setting_Panel.Location = new Point(0, -1);
            More_Panel.Location = new Point(-114514, -114514);
        }

        private void Setting_Versions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string content = Setting_Versions.Text.ToString();
            File.WriteAllText("RMCL\\Game_Version.txt", content);
            RMCL_internal.console.WriteLine("" + content);
        }
        
        private void Starting_Game_Click(object sender, EventArgs e)
        {
            Launcher_RMCL launcher_RMCL = new Launcher_RMCL(Setting_Versions.Text, Setting_Java.Text, Setting_Player_Name.Text, Setting_Window_Size.Text.ToString(), Setting_Versions.SelectedIndex);
        }
        private void Setting_Player_Name_TextChanged(object sender, EventArgs e)
        {
            string content = Setting_Player_Name.Text.ToString();
            File.WriteAllText("RMCL\\Player_Name.txt", content);
            
        }

        private void Taskkill_Java_Task_Button_Click(object sender, EventArgs e)
        {
            int javaProcessCount_lost = Process.GetProcessesByName("java").Length+ Process.GetProcessesByName("javaw").Length;
            RMCL_internal.console.WriteLine("当前运行的Java进程数量: " + javaProcessCount_lost);

            Process[] Java = Process.GetProcessesByName("Java");
            Process[] Javaw = Process.GetProcessesByName("Javaw");
            for(int i = 0; i <= 10; i++)
            {
                foreach (Process p in Java)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
                foreach (Process p in Javaw)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

            Thread.Sleep(100);
            int javaProcessCount_new= Process.GetProcessesByName("java").Length + Process.GetProcessesByName("javaw").Length;
            RMCL_internal.console.WriteLine("当前运行的Java进程数量: " + javaProcessCount_new);
            if(javaProcessCount_lost > 0) {
                if (Language_All == "En")
                {
                    MessageBox.Show("Java Processes Ended!\nNumber of Java Processes Ended:" + (javaProcessCount_lost - javaProcessCount_new).ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Java进程已结束!\n已结束Java进程数量:" + (javaProcessCount_lost - javaProcessCount_new).ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if(Language_All == "En")
                {
                    MessageBox.Show("There is currently no Java process!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("当前无Java进程!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Setting_Java_SelectedIndexChanged(object sender, EventArgs e)
        {
            string content = Setting_Java.Text.ToString();
            File.WriteAllText("RMCL\\Java_Path.txt", content);
        }
        int i = 0;
        string[] verpath = new string[1000];
        string[] ver = new string[1000];
        private void Minecraft_Versions_Click(object sender, EventArgs e)
        {
            More_Panel.Location = new Point(-114514,-114514);
            Versions_Op_Panel.Location = new Point(0, -1);
            string path = ".minecraft\\versions";
            string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            Versions_Versions_List.Items.Clear();
            foreach (string directory in directories)
            {
                i++;
                ver[i] = directory.Substring(20);
                verpath[i] = directory;
                Versions_Versions_List.Items.Add(directory.Substring(20));
            }
        }

        private void Version_Op_Back_Click(object sender, EventArgs e)
        {
            More_Panel.Location = new Point(0, -1);
            Versions_Op_Panel.Location = new Point(-114514, -114514);
        }

        private void Delete_Version_Button_Click(object sender, EventArgs e)
        {
            delete();
        }
        private int delete()
        {
            for (int j = 0; j <= i; j++)
            {
                if (Versions_Versions_List.Text == ver[j])
                {
                    try
                    {
                        string temp = verpath[j];
                        Directory.Delete(verpath[j], true);
                        Versions_Versions_List.Items.Remove(Versions_Versions_List.SelectedItem);
                        try
                        {
                            Setting_Versions.Items.Clear();
                            string folderPath = ".minecraft\\versions";
                            string[] subDirectories = Directory.GetDirectories(folderPath);
                            foreach (string subDir in subDirectories)
                            {
                                string folderName = new DirectoryInfo(subDir).Name;
                                Setting_Versions.Items.Add(folderName);
                            }
                        }
                        catch
                        {
                            RMCL_internal.console.WriteLine("RMCL无权限或Versions文件夹不存在！");
                        }
                        if (Language_All == "En")
                        {
                            System.Windows.Forms.MessageBox.Show(temp + "\nDeleted successfully!", "Deleted successfully!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show(temp + "\n删除成功！", "删除成功！", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        return 0;
                    }
                    catch{}

                }
            }
            return 0;
        }

        private void Update_History_Goto_Click(object sender, EventArgs e)
        {
            Date_Panel.Location = new Point(0, -1);
            More_Panel.Location = new Point(-114514, -114514);
        }

        private void Date_Back_Click(object sender, EventArgs e)
        {
            Date_Panel.Location = new Point(-114514, -114514);
            More_Panel.Location = new Point(0, -1);
        }

        private void Concerning_Back_Click(object sender, EventArgs e)
        {
            Concerning_Panel.Location = new Point(-114514, -114514);
            More_Panel.Location = new Point(0, -1);
        }

        int Online_Ms = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (Online_Ms == 0)
            {
                More_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Panel.Location = new Point(0, -1);
                LAN_Online_Main_Panel.Location = new Point(3, 43);
                LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Create_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_OK_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_List.Items.Clear();
            }else if (Online_Ms==1)
            {
                More_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Panel.Location = new Point(0, -1);
                LAN_Online_Main_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_OK_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Create_Panel.Location = new Point(3, 43);
            }else if (Online_Ms == 2)
            {
                More_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Panel.Location = new Point(0, -1);
                LAN_Online_Main_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
                LAN_Online_Access_OK_Panel.Location = new Point(3, 43);
                LAN_Online_Create_Panel.Location = new Point(-114514, -114514);

            }

        }

        private void LAN_Online_Create_Goto_Click(object sender, EventArgs e)
        {
            //LAN_Online_Main_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Create_Panel.Location = new Point(3, 43);
            LAN_Online_Main_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
            
        }
        //string[] ips = new string[65025];

        private static async Task ipcl(string ips)
        {
            TcpClient client = new TcpClient();
            bool isConnected = await ConnectAsyncWithTimeout(client, ips, 8088, 50);
            if (isConnected)
            {
                NetworkStream stream = client.GetStream();

                byte[] requestMessage = Encoding.UTF8.GetBytes("RMCL_Online_Get_Name");
                await stream.WriteAsync(requestMessage, 0, requestMessage.Length);

                byte[] responseMessage = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseMessage, 0, responseMessage.Length);
                string get_names = Encoding.UTF8.GetString(responseMessage, 0, bytesRead);

                client.Close();

                MainForm form = Application.OpenForms["Main"] as MainForm;
                if (form != null)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        //form.LAN_Online_Access_List.Items.Clear();
                        //form.LAN_Online_Access_List.Items.Add(get_names);
                        if (Language_All == "En")
                        {
                            form.LAN_Online_Access_List.Items.Add("Player "+get_names+"'s game room");
                        }
                        else
                        {
                            form.LAN_Online_Access_List.Items.Add("玩家 "+get_names+" 的房间");
                        }
                    });
                }
            }
            else
            {}
        }
        static async Task<bool> ConnectAsyncWithTimeout(TcpClient client, string serverIp, int serverPort, int timeout)
        {
            var connectTask = client.ConnectAsync(serverIp, serverPort);
            var timeoutTask = Task.Delay(timeout);

            if (await Task.WhenAny(connectTask, timeoutTask) == connectTask)
            {
                // 连接成功
                return true;
            }
            else
            {
                // 连接超时
                return false;
            }
        }
        string[] ip = new string[65025];
        bool stop = false;
        private async Task Scan()
        {
            LAN_Online_Access_List.Items.Clear();
            LAN_Online_Access_Panel.Location = new Point(3, 43);
            LAN_Online_Main_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Create_Panel.Location = new Point(-114514, -114514);
            int serverPort = 8088;
            
            int ips = 0;
            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= 255; j++)
                {
                    if (stop==false)
                    {
                        try
                        {
                            string serverIp = "192.168." + i.ToString() + "." + j.ToString();
                            RMCL_internal.console.WriteLine(serverIp);
                            TcpClient client = new TcpClient();
                            bool isConnected = await ConnectAsyncWithTimeout(client, serverIp, serverPort, 10); // 设置连接超时时间为50毫秒
                            if (isConnected)
                            {
                                NetworkStream stream = client.GetStream();

                                byte[] requestMessage = Encoding.UTF8.GetBytes("RMCL_Online_Access");
                                await stream.WriteAsync(requestMessage, 0, requestMessage.Length);

                                byte[] responseMessage = new byte[1024];
                                int bytesRead = await stream.ReadAsync(responseMessage, 0, responseMessage.Length);
                                string responseData = Encoding.UTF8.GetString(responseMessage, 0, bytesRead);
                                client.Close();

                                if (responseData == "RMCL_Online_Get_OK")
                                {
                                    ip[ips] = serverIp;
                                    ips++;
                                    ipcl(ip[ips - 1]);
                                }
                            }
                            else
                            { }
                            //await Task.Delay(10);
                        }
                        catch (Exception ex)
                        {
                            RMCL_internal.console.WriteLine("请求IP时出现异常: " + ex.Message);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void LAN_Online_Back_Click(object sender, EventArgs e)
        {
            LAN_Online_Panel.Location = new Point(-114514, -114514);
            More_Panel.Location = new Point(0, -1);
            stop = true;
        }

        private void LAN_Online_Access_Goto_Click(object sender, EventArgs e)
        {
            Scan();
            stop = false;
        }

        private async void LAN_Online_Access_Access_Click(object sender, EventArgs e)
        {
            RMCL_internal.console.WriteLine(ip[LAN_Online_Access_List.SelectedIndex]);
            string temp = await get_Minecraft_IP(ip[LAN_Online_Access_List.SelectedIndex]);
            RMCL_internal.console.WriteLine(temp);
            if (Language_All == "En")
            {
                LAN_Online_Access_IP_Ts.Text = "Room's IP:" + temp;
            }
            else
            {
                LAN_Online_Access_IP_Ts.Text = "房间IP：" + temp;
            }
            stop = true;
            Online_Ms = 2;
            LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Access_OK_Panel.Location = new Point(3, 43);
        }
        private static async Task<string> get_Minecraft_IP(string ips)
        {
            TcpClient client = new TcpClient();
            bool isConnected = await ConnectAsyncWithTimeout(client, ips, 8088, 50);
            if (isConnected)
            {
                NetworkStream stream = client.GetStream();

                byte[] requestMessage = Encoding.UTF8.GetBytes("RMCL_Online_Get_Minecraft");
                await stream.WriteAsync(requestMessage, 0, requestMessage.Length);

                byte[] responseMessage = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseMessage, 0, responseMessage.Length);
                string ipss = Encoding.UTF8.GetString(responseMessage, 0, bytesRead);

                client.Close();
                return ipss;
            }
            else
            {
                return null;
            }
        }

        private void LAN_Online_Create_Create_Click(object sender, EventArgs e)
        {
            Online_Ms = 1;
        }

        private void LAN_Online_Access_OK_Stop_Click(object sender, EventArgs e)
        {
            Online_Ms = 0;
            stop = false;
            More_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Panel.Location = new Point(0, -1);
            LAN_Online_Main_Panel.Location = new Point(3, 43);
            LAN_Online_Access_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Create_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Access_OK_Panel.Location = new Point(-114514, -114514);
            LAN_Online_Access_List.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop=true;
            Thread.Sleep(100);
            Scan();
        }

        private void LAN_Online_Create_Create_Click_1(object sender, EventArgs e)
        {
            if (LAN_Online_Create_Create.Text == "创建"|| LAN_Online_Create_Create.Text == "Create")
            {
                Server();
                Online_Ms = 1;
                Server_Stop = false;
                LAN_Online_Create_Host_Sr.Enabled = false;
                if (Language_All =="En")
                {
                    LAN_Online_Create_Create.Text = "Close";
                }
                else
                {
                    LAN_Online_Create_Create.Text = "关闭";
                }
                host = LAN_Online_Create_Host_Sr.Text;
            }
            else
            {
                if (Language_All == "En")
                {
                    LAN_Online_Create_Create.Text = "Create";
                }
                else
                {
                    LAN_Online_Create_Create.Text = "创建";
                }
                LAN_Online_Create_Host_Sr.Enabled = true;
                Server_Stop = true;
                Online_Ms = 0;
            }
        }
        static string ipss;
        static bool IsVirtualAdapter(string description)
        {
            // 判断描述信息中是否包含虚拟网卡的特定关键词或短语
            if (description.Contains("VMware") || description.Contains("VirtualBox") || description.Contains("Hyper-V"))
            {
                return true; // 是虚拟网卡
            }
            return false; // 不是虚拟网卡
        }
        static string host;
        static bool Server_Stop = false;
        static void DisplayIPv4Addresses(NetworkInterface networkInterface)
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        if (ip.Address.ToString() != "127.0.0.1")
                        {
                            RMCL_internal.console.WriteLine("IPv4 地址: " + ip.Address.ToString());
                            ipss = ip.Address.ToString();
                        }
                    }
                }
            }
        }

        static async Task Server()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (!IsVirtualAdapter(networkInterface.Description))
                {
                    DisplayIPv4Addresses(networkInterface);
                }
            }


            int serverPort = 8088;
            TcpListener listener = new TcpListener(IPAddress.Any, serverPort);
            listener.Start();
            RMCL_internal.console.WriteLine("服务器已启动，等待客户端连接...");

            while (true)
            {
                if (Server_Stop==false)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    RMCL_internal.console.WriteLine("客户端已连接");

                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string requestData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    RMCL_internal.console.WriteLine("接收到客户端消息: " + requestData);

                    if (requestData == "RMCL_Online_Access")
                    {
                        string responseDatas = "RMCL_Online_Get_OK";
                        byte[] responseBuffers = Encoding.UTF8.GetBytes(responseDatas);
                        await stream.WriteAsync(responseBuffers, 0, responseBuffers.Length);
                        RMCL_internal.console.WriteLine("已向客户端发送消息: " + responseDatas);
                    }
                    else if (requestData == "RMCL_Online_Get_Name")
                    {
                        string responseData = Player_Name_str;
                        byte[] responseBuffer = Encoding.UTF8.GetBytes(responseData);
                        await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                        RMCL_internal.console.WriteLine("已向客户端发送消息: " + responseData);
                    }
                    else if (requestData == "RMCL_Online_Get_Minecraft")
                    {
                        string responseData = ipss + ":"+host;
                        byte[] responseBuffer = Encoding.UTF8.GetBytes(responseData);
                        await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                        RMCL_internal.console.WriteLine("已向客户端发送消息: " + responseData);
                    }
                    client.Close();
                }
            }
        }

        private void button1_Click1(object sender, EventArgs e)
        {
            More_Panel.Location = new Point(-114514, -114514);
            Concerning_Panel.Location = new Point(0, -1);
        }

        private void Setting_Window_Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            RMCL_internal.console.WriteLine(Setting_Window_Size.Text.ToString());
            File.WriteAllText("RMCL\\Size.txt", Setting_Window_Size.Text.ToString());
        }

        private void Update_Setting_CheckedChanged(object sender, EventArgs e)
        {
            if(Update_Setting.Checked)
            {
                File.WriteAllText("RMCL\\Update.txt", "true");
                Task.Run(() => {
                    Update update = new Update();
                });
            }
            else
            {
                File.WriteAllText("RMCL\\Update.txt", "false");
            }
        }

        private void Update_Goto_Click(object sender, EventArgs e)
        {
            Update update = new Update(true);
        }

        private void Dow_Back_Click(object sender, EventArgs e)
        {
            Dow_Panel.Location = new Point(-114514, -114514);
            More_Panel.Location = new Point(0, -1);
        }

        private void Dow_Goto_Click(object sender, EventArgs e)
        {
            refresh();
            Dow_Panel.Location = new Point(0, -1);
            More_Panel.Location = new Point(-114514, -114514);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                File.WriteAllText("RMCL\\GUI.txt", "true");
            }
            else
            {
                File.WriteAllText("RMCL\\GUI.txt", "false");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            command("md RMCL\\RMCL_WAN_Online&md RMCL\\RMCL_WAN_Online\\frpc&md RMCL\\RMCL_WAN_Online\\frps&md RMCL\\RMCL_WAN_Online\\Main");
            Command_fpr commands = new Command_fpr("RMCL\\RMCL_WAN_Online\\Main\\RMCL_Frp_New.exe -rmcl -start -ljmk", true);
            
        }
        private static void CompressFolder(ZipArchive archive, string folderPath, string entryName)
        {
            // 确保folderPath以目录分隔符结尾
            if (!folderPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                folderPath += Path.DirectorySeparatorChar;
            }

            // 获取folderPath下的所有文件和文件夹
            foreach (string filePath in Directory.EnumerateFileSystemEntries(folderPath))
            {
                // 计算文件或文件夹在ZIP中的相对路径
                string relativePath = filePath.Substring(folderPath.Length).Replace("\\", "/");
                string zipEntryName = entryName + relativePath;

                if (Directory.Exists(filePath))
                {
                    // 如果是文件夹，递归调用CompressFolder
                    CompressFolder(archive, filePath, zipEntryName + "/");
                }
                else
                {
                    // 如果是文件，创建ZIP条目并复制文件
                    var entry = archive.CreateEntryFromFile(filePath, zipEntryName);
                }
            }
        }
        private void Icon_Pac_Click(object sender, EventArgs e)
        {
            // 创建一个OpenFileDialog对象
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.Title = "选择一个皮肤文件"; // 对话框标题
            openFileDialog.Filter = "皮肤文件 (*.png)|*.png";

            // 显示对话框并获取用户的选择
            DialogResult result = openFileDialog.ShowDialog();

            // 如果用户选择了文件并点击了“打开”按钮
            if (result == DialogResult.OK)
            {
                string folderPath = "RMCL\\Skin"; // 替换为你的文件夹路径

                try
                {
                    // 检查文件夹是否存在
                    if (Directory.Exists(folderPath))
                    {
                        // 删除文件夹及其所有子文件夹和文件
                        Directory.Delete(folderPath, true);
                        RMCL_internal.console.WriteLine("文件夹已被删除: " + folderPath);
                    }
                    else
                    {
                        RMCL_internal.console.WriteLine("文件夹不存在: " + folderPath);
                    }
                }
                catch (Exception ex)
                {
                    RMCL_internal.console.WriteLine("发生错误: " + ex.Message);
                }
                // 获取用户选择的文件路径
                string filePath = openFileDialog.FileName;
                var sink = new Skin_Initialize();
                // 在这里可以继续处理文件，例如加载文件内容等
                RMCL_internal.console.WriteLine("你选择了: " + filePath);
                What_Skin what_Skin = new What_Skin();
                what_Skin.ShowDialog();
                File.WriteAllText("RMCL\\Skin.txt", "true");
                try
                {
                    File.WriteAllText("RMCL\\Skin\\pack.mcmeta", "{\"pack\":{\"pack_format\":15,\"description\":\"RMCL 自定义离线皮肤资源包\"}}");
                    Bitmap pack_icon = new Bitmap("RMCL\\croppedImage.png");
                    pack_icon.Save("RMCL\\Skin\\pack.png");
                    pack_icon.Dispose();
                    File.WriteAllBytes("RMCL\\Skin.png", File.ReadAllBytes(filePath));
                    /*if (File.ReadAllText("RMCL\\Skin_Lx.txt") == "S")
                    {*/
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\alex.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\alex.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\ari.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\efe.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\kai.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\makena.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\noor.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\steve.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\sunny.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim\\zuri.png", File.ReadAllBytes(filePath));
                    /*}
                    else
                    {*/
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\steve.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\steve.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\alex.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\ari.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\efe.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\kai.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\makena.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\noor.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\steve.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\sunny.png", File.ReadAllBytes(filePath));
                        File.WriteAllBytes("RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide\\zuri.png", File.ReadAllBytes(filePath));
                    /*}*/
                }
                catch{}

                // 要压缩的文件夹路径
                string folderPathToCompress = "RMCL\\Skin";

                // ZIP文件的输出路径
                string zipPath = ".minecraft\\resourcepacks\\RMCL - 自定义单人游戏皮肤.zip";

                try
                {
                    using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, true))
                        {
                            // 递归压缩文件夹中的所有文件和子文件夹
                            CompressFolder(archive, folderPathToCompress, "");
                        }
                    }

                    RMCL_internal.console.WriteLine("文件夹压缩完成，ZIP文件保存在: " + zipPath);
                }
                catch (Exception ex)
                {
                    RMCL_internal.console.WriteLine("发生错误: " + ex.Message);
                }
                //Thread.Sleep(100);
                // 创建一个Bitmap对象
                Bitmap originalImage = new Bitmap("RMCL\\Skin.png");

                // 创建一个新的Bitmap对象，用于存储指定区域的内容
                Bitmap croppedImage = new Bitmap(8, 8);

                // 提取指定区域的像素数据
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        System.Drawing.Color pixelColor = originalImage.GetPixel(x + 8, y + 8);
                        croppedImage.SetPixel(x, y, pixelColor);
                    }
                }

                // 保存新的图像
                croppedImage.Save("RMCL\\croppedImage.png");
                // 读取保存的图片
                Bitmap savedImage = new Bitmap("RMCL\\croppedImage.png");

                // 创建一个新的Bitmap对象，用于存储放大后的内容
                Bitmap enlargedImage = new Bitmap(savedImage.Width * 10, savedImage.Height * 10);

                // 将每个像素放大10倍
                for (int x = 0; x < savedImage.Width; x++)
                {
                    for (int y = 0; y < savedImage.Height; y++)
                    {
                        System.Drawing.Color pixelColor = savedImage.GetPixel(x, y);
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                enlargedImage.SetPixel(x * 10 + i, y * 10 + j, pixelColor);
                            }
                        }
                    }
                }
                originalImage.Dispose();
                savedImage.Dispose();
                croppedImage.Dispose();
                enlargedImage.Save("RMCL\\Pixel.png");
                // 将enlargedImage显示在图片框上
                Icon_Pac.BackgroundImage = enlargedImage;
            }
        }


        private void Icon_Pac_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.Visible = true;
        }

        private void Icon_Pac_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            File.Delete("RMCL\\InitializeComponent.txt");
        }

        private void Debug_Button_CheckedChanged(object sender, EventArgs e)
        {
            File.WriteAllText("RMCL\\Debug.txt",Debug_Button.Checked.ToString());
        }
    }
}
