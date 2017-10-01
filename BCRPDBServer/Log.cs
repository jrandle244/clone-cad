using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneCADServer
{
    public class Log
    {
        private static StreamWriter writer;
        public static List<Alias> Aliases { get; set; }

        public Log(string FileName, List<Alias> aliases)
        {
            Aliases = aliases;
            try
            {
                writer = new StreamWriter(FileName);
            }
            catch (IOException)
            {
                Console.WriteLine("A program is hogging the log file! Please exit the program and start the server again.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public static void WriteLine(string text)
        {
            string formatted = "[" + DateTime.Now + "]: " + text;

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);

            writer.Flush();
        }

        public static void WriteLine(string text, string ip)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases.GetAlias(ip) + "]: " + text;

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);

            writer.Flush();
        }
    }
}
