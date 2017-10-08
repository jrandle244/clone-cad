using System;
using System.Windows.Forms;
using CloneCAD.Client.DataHolders;
using CloneCAD.Client.Menus;

namespace CloneCAD.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config config = null;

#if !DEBUG
            try
            {
                config = new Config("settings.ini");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(config));
            }
            catch (Exception e)
            {
                new ErrorHandler(config?.Locale).ExceptionHandler(e, 4);
            }
#endif
#if DEBUG

            config = new Config("settings.ini");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(config));
#endif
        }
    }
}
