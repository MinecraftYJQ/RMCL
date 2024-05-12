using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Launcher.cs.qjbl;

namespace Launcher.cs
{
    internal class RMCL_Debug
    {
        public class Debug_Console
        {
            public void WriteLine(dynamic value,bool debug_true=true)
            {
                
                if(debug_true)
                {
                    Console.WriteLine("[debug:]" + value);
                    RMCL_internal.debug_log_js++;
                    RMCL_internal.debug_log[RMCL_internal.debug_log_js] = "[debug]:" + value.ToString();
                }
                else
                {
                    Console.WriteLine(value);
                    RMCL_internal.debug_log_js++;
                    RMCL_internal.debug_log[RMCL_internal.debug_log_js] =value.ToString();
                }
                
                //RMCL_internal.debug_log += "[debug]:"+value + "\r\n";
            }
        }
    }
}
