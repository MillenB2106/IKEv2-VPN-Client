using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool createdNew;
            using (var mutex = new System.Threading.Mutex(true, "VPN", out createdNew))
            {
                if (createdNew)
                    Application.Run(new VPN());
                else
                    MessageBox.Show("An instance of this application is already running");
            }
        }
    }
}
