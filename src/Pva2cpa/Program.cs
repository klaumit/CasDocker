using System;
using System.Windows.Forms;

namespace Pva2cpa
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Args = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static string[] Args { get; private set; }
    }
}