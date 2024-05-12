using Launcher.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.cs
{
    internal class Command_fpr
    {
        public Command_fpr(string str, bool Win) 
        {
            try
            {
                File.ReadAllText("RMCL\\RMCL_WAN_Online\\Main\\RMCL_Frp_New.exe");
                Command command = new Command(str, Win);
            }catch  (Exception ex)
            {
                Download_RMCL_Frp_New download_RMCL_Frp_New = new Download_RMCL_Frp_New();
                download_RMCL_Frp_New.Show();
            }
        }
    }
}
