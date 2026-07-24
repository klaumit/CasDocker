using System;
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
                    Application.Run(new NoteContext());
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