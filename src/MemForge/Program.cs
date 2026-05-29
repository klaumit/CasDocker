using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MemForge;

namespace xxyy
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, "MemForge", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    NotificationIcon notificationIcon = new NotificationIcon();
                    notificationIcon.notifyIcon.Visible = true;
                    Application.Run();
                    notificationIcon.notifyIcon.Dispose();
                }
                else
                {
                    // The application is already running
                    // TODO: Display message box or change focus to existing application instance
                }
            } // releases the Mutex
        }
    }
}