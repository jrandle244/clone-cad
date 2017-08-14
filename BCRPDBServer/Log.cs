﻿using System;
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
        public static List<Alias> Aliases { get; set; }

        public Log(string FileName, List<Alias> aliases)
        {
            Aliases = aliases;
            writer = new StreamWriter(FileName);
        }

        public static void WriteLine(string text)
        {
            string formatted = "[" + DateTime.Now + "]: " + text;
            
            writer.WriteLine(formatted);

            writer.Flush();
        }

        public static void WriteLine(string text, string ip)
        {
            string formatted = "[" + DateTime.Now + "] [" + Aliases.GetAlias(ip) + "]: " + text;
            
            writer.WriteLine(formatted);

            writer.Flush();
        }
    }
}
