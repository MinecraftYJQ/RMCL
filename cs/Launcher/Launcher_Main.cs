using KMCCC.Authentication;
using KMCCC.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher.cs
{
    internal class Launcher_Main
    {
        public static LauncherCore Core = LauncherCore.Create();
        KMCCC.Launcher.Version[] versions = Core.GetVersions().ToArray();
        public Launcher_Main(string Size_Ms,string Name,string Java_Path,int version) {
            
            /*
            默认大小 | Default size
            标准大小（870 * 520） | Standard size (870 * 520)
            */
            WindowSize Game_Size = null;
            if (Size_Ms == "默认大小 | Default size")
            {
                Screen mainScreen = Screen.PrimaryScreen;
                int screenWidth = mainScreen.Bounds.Width;
                int screenHeight = mainScreen.Bounds.Height;
                Game_Size = new WindowSize { Height = (ushort?)(screenHeight - 180), Width = (ushort?)(screenWidth - 100) };
            }
            else if (Size_Ms == "标准大小（870 * 520） | Standard size (870 * 520)")
            {
                Game_Size = new WindowSize { Height = 520, Width = 870 };
            }
            try
            {
                Core.JavaPath = Java_Path;
                var ver = versions[version];
                var result = Core.Launch(new LaunchOptions
                {
                    Version = ver,
                    MaxMemory = 1024,
                    Authenticator = new OfflineAuthenticator(Name),
                    Mode = LaunchMode.MCLauncher,
                    Size = Game_Size

                });
            }catch (Exception ex)
            {
                MessageBox.Show("游戏启动出现错误！\n错误信息:"+ex.Message,"RMCL - 启动游戏错误",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
