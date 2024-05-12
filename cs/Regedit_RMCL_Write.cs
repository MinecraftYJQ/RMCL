using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Launcher.cs.qjbl;

namespace Launcher.cs
{
    internal class Regedit_RMCL_Write
    {
        public Regedit_RMCL_Write() {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                File.WriteAllText($"RMCL\\RMCL.bat", $"@echo off\ncd \"{Directory.GetCurrentDirectory()}\"\n\"{currentProcess.MainModule.FileName}\"");
                // 打开HKEY_CLASSES_ROOT并创建一个新的注册表项路径
                RegistryKey key = Registry.ClassesRoot;
                RegistryKey subKey0 = key.CreateSubKey("RMCL");
                RegistryKey subKey1 = key.CreateSubKey("RMCL\\shell\\open\\command");
                RegistryKey subKey2 = key.CreateSubKey("RMCL\\DefaultIcon");
                RegistryKey subKey3 = key.CreateSubKey("RMCL\\shell\\open");

                // 设置默认值，不需要指定名称，传入的字符串将作为默认值
                
                subKey0.SetValue("", "RMCL 协议", RegistryValueKind.String);
                subKey0.SetValue("URL Protocol", "", RegistryValueKind.String);
                subKey1.SetValue("", $"\"{Directory.GetCurrentDirectory()}\\RMCL\\RMCL.bat\" \"%1\"", RegistryValueKind.String);
                subKey2.SetValue("", $"\"{Directory.GetCurrentDirectory()}\\RMCL\\RMCL.bat\",1", RegistryValueKind.String);
                subKey3.SetValue("", "通过协议启动RMCL", RegistryValueKind.String);

                subKey1.Close();

                RMCL_internal.console.WriteLine("注册表的默认值已修改。");
            }
            catch (System.Exception ex)
            {
                RMCL_internal.console.WriteLine("发生错误: " + ex.Message);
            }
        }
    }
}
