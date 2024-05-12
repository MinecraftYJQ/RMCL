using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Launcher.cs.qjbl;

namespace Launcher.cs
{
    internal class Message_fs
    {
        public Message_fs(string title, string text)
        {
            RMCL_internal.console.WriteLine("Message_fs启动");
            try
            {
                new ToastContentBuilder()
                        .AddText(title)
                        .AddText(text)
                        .Show();
            }
            catch (Exception ex)
            {
                RMCL_internal.console.WriteLine("信息错误！");
            }
        }
    }
}
