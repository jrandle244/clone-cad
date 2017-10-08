using System;
using CloneCAD.Common.DataHolders;
using CloneCAD.Server.DataHolders;
using CloneCAD.Server.DataHolders.Static;

using System.IO;

namespace CloneCAD.Server
{
    static class Program
    {
        public const string CIV_EXPORT_PATH = "Civilians.odf";
        public const string CONFIG_PATH = "settings.ini";

        static void Main(string[] args)
        {
#if DEBUG
            Config config;

            if (File.Exists(CONFIG_PATH))
                config = new Config(CONFIG_PATH);
            else
            {
                new ErrorHandler(null).Error("No \"" + CONFIG_PATH + "\" file exists. Check the README.md on GitHub to get a default \"" + CONFIG_PATH + "\".", 1);
                return; //Have to put this here so 'server' initialization didn't throw an error saying 'config' might not be initialized.
            }

            Server server = new Server(config, File.Exists(CIV_EXPORT_PATH) ? new StorableValue<CivilianDictionary>(CIV_EXPORT_PATH).Value : new CivilianDictionary());
            server.Start();
#endif
#if !DEBUG
            Config config = null;

            try 
            {
                if (File.Exists(CONFIG_PATH))
                    config = new Config(CONFIG_PATH);
                else
                {
                    new ErrorHandler(null).Error("No \"" + CONFIG_PATH + "\" file exists. Check the README.md on GitHub to get a default \"" + CONFIG_PATH + "\".", 1);
                    return; //Have to put this here so 'server' initialization didn't throw an error saying 'config' might not be initialized.
                }

                Server server = new Server(config, File.Exists(CIV_EXPORT_PATH) ? new StorableValue<CivilianDictionary>(CIV_EXPORT_PATH).Value : new CivilianDictionary());
                server.Start();
            }
            catch (Exception e)
            {
                new ErrorHandler(config?.Locale).Error("An exception went unhandled.", 3, e);
            }
#endif
        }
    }
}
