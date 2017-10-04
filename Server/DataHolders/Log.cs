using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.IO;

namespace CloneCAD.Server.DataHolders
{
    public class Log
    {
        private static StreamWriter writer;

        public static AliasDictionary Aliases { get; set; }

        public Log(string FileName, LocaleConfig Locale, AliasDictionary _Aliases)
        {
            Aliases = _Aliases;

            try
            {
                writer = new StreamWriter(FileName)
                {
                    AutoFlush = true
                };
            }
            catch (IOException)
            {
                Functions.Error(Locale, "LogFileInUse", 1);
            }
        }

        public static void WriteLine(string text)
        {
            string formatted = "[" + DateTime.Now + "]: " + text;

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }

        public static void WriteLine(string text, string ip)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases[ip] + "]: " + text;

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }
    }
}
