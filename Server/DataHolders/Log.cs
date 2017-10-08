using CloneCAD.Common.DataHolders;

using System;
using System.IO;

namespace CloneCAD.Server.DataHolders
{
    public class Log
    {
        public enum Status { Succeeded, Failed }

        private readonly StreamWriter Writer;

        public AliasDictionary Aliases { get; set; }
        public LocaleConfig Locale { get; set; }
        public ErrorHandler Handler { get; set; }

        public Log(string fileName, LocaleConfig locale, AliasDictionary aliases)
        {
            Aliases = aliases;
            Locale = locale;
            Handler = new ErrorHandler(locale);

            try
            {
                Writer = new StreamWriter(fileName)
                {
                    AutoFlush = true
                };
            }
            catch (IOException)
            {
                Handler.Error("LogFileInUse", 1);
            }
        }

        public void WriteLine(string text)
        {
            string formatted = "[" + DateTime.Now + "]: " + Locale[text];

            Console.WriteLine(formatted);
            Writer.WriteLine(formatted);
        }

        public void WriteLine(string text, Status status, params object[] objs)
        {
            string formatted = "[" + DateTime.Now + "]: " + Locale[text, objs] + " [" + (status == Status.Succeeded ? Locale["SucceededStatus"] : Locale["FailedStatus"]) + "]";

            Console.WriteLine(formatted);
            Writer.WriteLine(formatted);
        }

        public void WriteLine(string text, string ip, params object[] objs)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases[ip] + "]: " + Locale[text, objs];

            Console.WriteLine(formatted);
            Writer.WriteLine(formatted);
        }

        public void WriteLine(string text, string ip, Status status, params object[] objs)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases[ip] + "]: " + Locale[text, objs] + " [" + (status == Status.Succeeded ? Locale["SucceededStatus"] : Locale["FailedStatus"]) + "]";

            Console.WriteLine(formatted);
            Writer.WriteLine(formatted);
        }
    }
}
