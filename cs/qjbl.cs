using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Launcher.cs.RMCL_Debug;

namespace Launcher.cs
{
    internal class qjbl
    {
        public static class RMCL_internal
        {
            public static System.Windows.Forms.Panel Dow_List =new System.Windows.Forms.Panel();
            public static string[] java_path = new string[25565];
            public static int java_path_js = 0;
            public static string[] debug_log=new string[32766];
            //public static string debug_log;
            public static int debug_log_js;
            public static int debug_x;
            public static int debug_y;
            public static int debug_h;
            public static int debug_w;
            public static Debug_Console console = new Debug_Console();
            public static string user_dq;
        }
        
    }
}
