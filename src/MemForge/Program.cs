using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable ConvertToUsingDeclaration

namespace MemForge
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isFirstInstance;
            using (var mtx = new Mutex(true, "MemForge", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    var obj = new NoteIcon();
                    obj.noteIcon.Visible = true;
                    Application.Run();
                    obj.noteIcon.Dispose();
                }
                else
                {
                    MessageBox.Show("Application is running already!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}