using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.IO;

namespace CloneCAD.Server.DataHolders
{
    public class Log
    {
        public enum Status { Succeeded, Failed }

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
            string formatted = "[" + DateTime.Now + "]: " + text + (text.EndsWith(".") ? "" : ".");

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }

        public static void WriteLine(string text, Status status)
        {
            string formatted = "[" + DateTime.Now + "]: " + (text.EndsWith(".") ? text.Substring(0, text.Length - 1) : text) + " [" + status + "].";

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }

        public static void WriteLine(string text, string ip)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases[ip] + "]: " + text + (text.EndsWith(".") ? "" : ".");

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }

        public static void WriteLine(string text, string ip, Status status)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases[ip] + "]: " + (text.EndsWith(".") ? text.Substring(0, text.Length - 1) : text) + " [" + status + "].";

            Console.WriteLine(formatted);
            writer.WriteLine(formatted);
        }
    }
}
