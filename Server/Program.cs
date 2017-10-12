using System;
using CloneCAD.Common.DataHolders;
using CloneCAD.Server.DataHolders;
using CloneCAD.Server.DataHolders.Static;

using System.IO;
using System.Windows.Forms;

namespace CloneCAD.Server
{
    static class Program
    {
        public const string CIV_EXPORT_PATH = "Civilians.odf";
        public const string CONFIG_PATH = "settings.ini";

        [STAThread]
        static void Main()
        {
            Config config = null;
#if !DEBUG
            try
            {
#endif
                if (File.Exists(CONFIG_PATH))
                    config = new Config(CONFIG_PATH);
                else
                {
                    new ErrorHandler(null).Error("No \"" + CONFIG_PATH + "\" file exists. Check the README.md on GitHub to get a default \"" + CONFIG_PATH + "\".", 1);
                    return; //Have to put this here so 'server' initialization didn't throw an error saying 'config' might not be initialized.
                }

                Server server = new Server(config, File.Exists(CIV_EXPORT_PATH) ? new StorableValue<CivilianDictionary>(CIV_EXPORT_PATH).Value : new CivilianDictionary());
                server.Start();
#if !DEBUG
            }
            catch (Exception e)
            {
                try
                {
                    new ErrorHandler(config?.Locale).Error("UnexpectedErrorMsg", 3, e);
                }
                catch (Exception ee)
                {
                    Clipboard.SetText(e.ToString());
                    Console.WriteLine($"An exception occured when the exception handler was trying to catch an exception! Ironic. The original exception has been copied to your clipboard. Please post an issue on GitHub or Discord in a code block. When you are ready to receive the exception handler exception (you have saved the previous to a file or uploaded it), please click OK.\n\n{e}\n\nONLY PRESS A KEY IF YOU HAVE READ THIS.");

                    Console.ReadKey();

                    Clipboard.SetText(ee.ToString());
                    Console.WriteLine($"\n\nAn exception occured when the exception handler was trying to catch an exception! Ironic. The exception handler exception has been copied to your clipboard. Please post an issue on GitHub or Discord in a code block. When you are ready to receive the exception handler exception (you have saved the previous to a file or uploaded it), please click OK.\n\n{ee}\n\nONLY PRESS A KEY IF YOU HAVE READ THIS.");

                    Console.ReadKey();

                    Environment.Exit(3);
                }
            }
#endif
        }
    }
}
