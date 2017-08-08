using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCRPDBServer
{
    public class Log
    {
        private static StreamWriter writer;

        public Log(string FileName) =>
            writer = new StreamWriter(FileName);

        public static void WriteLine(string text)
        {
            string formatted = "[" + DateTime.Now + "]: " + text;

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);

            writer.Flush();
        }

        public static void WriteLine(string text, string ip)
        {
            string formatted = "[" + DateTime.Now + "] [" + ip + "]: " + text;

            WriteLine(formatted);
        }
    }
}
