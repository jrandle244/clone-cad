using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace CloneCAD.Server.DataHolders
{
    public enum FilterType { None, Whitelist, Blacklist }
    public enum Permission { Civ, Police, Dispatch }

    public class Config : GenericConfig
    {
        private ErrorHandler Handler;

        public int Port { get; private set; }

        public FilterType Filter { get; private set; }
        public string[] FilteredCivIPs { get; private set; }
        public string[] FilteredPoliceIPs { get; private set; }
        public string[] FilteredDispatchIPs { get; private set; }

        public AliasDictionary Aliases { get; private set; }
        public LocaleConfig Locale { get; private set; }
        public Log Log { get; private set; }

        public Config(string filePath) : base (new List<string>()
        {
            "Locale",
            "IP",
            "Port",
            "Filter",
            "FilteredCivIPs",
            "FilteredPoliceIPs",
            "FilteredDispatchIPs",
            "Log",
            "Aliases"
        })
        {
            Path = filePath;
            Load();
        }

        public new void Load()
        {
            base.Load();

            Locale = new LocaleConfig(base["Locale"]);
            Handler = new ErrorHandler(Locale);

            if (!int.TryParse(base["Port"], out int port) || port < 1024 || port > 65536)
                Handler.Error("InvalidPort", 1, base["Port"]);

            Port = port;


            if (!int.TryParse(base["Filter"], out int filter) || filter < 0 || filter > 2)
                Handler.Error("InvalidFilterType", 1, base["Filter"]);

            Filter = (FilterType)filter;
            

            FilteredCivIPs = string.IsNullOrWhiteSpace(base["FilteredCivIPs"]) ? new string[0] : base["FilteredCivIPs"].Split(',').Select(x => x.Trim()).ToArray();


            FilteredPoliceIPs = string.IsNullOrWhiteSpace(base["FilteredPoliceIPs"]) ? new string[0] : base["FilteredPoliceIPs"].Split(',').Select(x => x.Trim()).ToArray();


            FilteredDispatchIPs = string.IsNullOrWhiteSpace(base["FilteredDispatchIPs"]) ? new string[0] : base["FilteredDispatchIPs"].Split(',').Select(x => x.Trim()).ToArray();

            
            Aliases = string.IsNullOrWhiteSpace(base["Aliases"]) ? new AliasDictionary(Locale) : new AliasDictionary(Locale, base["Aliases"]);

            Log = new Log(base["Log"], Locale, Aliases);
        }

        public bool HasPerm(string ip, params Permission[] permissions)
        {
            foreach (Permission perm in permissions)
                switch (Filter)
                {
                    case FilterType.None:
                        return true;
                    case FilterType.Blacklist:
                        switch (perm)
                        {
                            case Permission.Civ when !FilteredCivIPs.Contains(ip):
                                return true;
                            case Permission.Police when !FilteredPoliceIPs.Contains(ip):
                                return true;
                            case Permission.Dispatch when !FilteredDispatchIPs.Contains(ip):
                                return true;
                        }
                        break;
                    case FilterType.Whitelist:
                        switch (perm)
                        {
                            case Permission.Civ when FilteredCivIPs.Contains(ip):
                                return true;
                            case Permission.Police when FilteredPoliceIPs.Contains(ip):
                                return true;
                            case Permission.Dispatch when FilteredDispatchIPs.Contains(ip):
                                return true;
                        }
                        break;
                }

            return false;
        }
    }
}
