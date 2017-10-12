using System;
using System.Windows.Forms;
using CloneCAD.Client.DataHolders;
using CloneCAD.Client.Menus;

namespace CloneCAD.Client
{
    static class Program
    {
        public const string CONFIG_PATH = "settings.ini";
        public const string ID_PATH = "IDs.odf";

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
#endif
                config = new Config(CONFIG_PATH);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(config));
#if !DEBUG
            }
            catch (Exception e)
            {
                try 
                {
                    new ErrorHandler(config?.Locale).ExceptionHandler(e, 4);
                }
                catch (Exception ee)
                {
                    Clipboard.SetText(e.ToString());
                    MessageBox.Show($"An exception occured when the exception handler was trying to catch an exception! Ironic. The original exception has been copied to your clipboard. Please post an issue on GitHub or Discord in a code block. When you are ready to receive the exception handler exception (you have saved the previous to a file or uploaded it), please click OK.\n\n{e}\n\nONLY CLICK OK IF YOU HAVE READ THIS.", @"CloneCAD");

                    Clipboard.SetText(ee.ToString());
                    MessageBox.Show($"An exception occured when the exception handler was trying to catch an exception! Ironic. The exception handler exception has been copied to your clipboard. Please post an issue on GitHub or Discord in a code block.\n\n{ee}\n\nONLY CLICK OK IF YOU HAVE READ THIS.", @"CloneCAD");
                }
            }
#endif
        }
    }
}
