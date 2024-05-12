using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static Launcher.cs.qjbl;

namespace Launcher.Window
{
    public partial class Debug_Window : Form
    {
        public Debug_Window()
        {
            InitializeComponent();
        }
        private static void SaveFile(string content, string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "另存为";
            saveFileDialog.FileName = "RMCL - 日志文件.log";
            saveFileDialog.Filter = filter;
            saveFileDialog.OverwritePrompt = true;
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    File.WriteAllText(filePath, content);
                    RMCL_internal.console.WriteLine("文件已保存到: " + filePath);
                }
                catch (Exception ex)
                {
                    RMCL_internal.console.WriteLine("保存文件时出错: " + ex.Message);
                }
            }
        }
        private void Debug_Window_Load(object sender, EventArgs e)
        {
            
            Task.Run(() =>
            {
                int js = 1;
                while (true)
                {
                    if(js != RMCL_internal.debug_log_js) {
                        string textWithNewLines = RMCL_internal.debug_log[RMCL_internal.debug_log_js-1].ToString();
                        string[] lines = textWithNewLines.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                        foreach (string line in lines)
                        {
                            richTextBox1.AppendText(line+"\r");
                            richTextBox1.Select(richTextBox1.Text.Length, 0);
                            //Thread.Sleep(1);
                        }
                        js++;
                        richTextBox1.Select(richTextBox1.Text.Length, 0);
                    }
                    Location = new Point(RMCL_internal.debug_x, RMCL_internal.debug_y);
                    Height = RMCL_internal.debug_h;
                    MaximumSize= new Size(25565, RMCL_internal.debug_h);
                    MinimumSize = new Size(500, RMCL_internal.debug_h);
                }
            });
        }
        private void 置顶此窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (置顶此窗口ToolStripMenuItem.Checked)
            {
                置顶此窗口ToolStripMenuItem.Checked = false;
                TopMost = false;
            }
            else
            {
                置顶此窗口ToolStripMenuItem.Checked = true;
                TopMost = true;
            }
        }
        private void 另存日志为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileContent=null;
            foreach (string line in RMCL_internal.debug_log)
            {
                fileContent += line+"\n";
            }
            string fileFilter = "日志文件(*.log)|*.log";
            SaveFile(fileContent, fileFilter);
        }

        private void Debug_Window_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("帮助:\n\nDebug(调试)模式有什么作用？\nDebug(调试)模式可以帮助用户反馈错误信息给开发者！\n当然，如果你电脑性能不是很好的不太建议开调试模式，因为这个模式会占用一定系统资源（笑", "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
