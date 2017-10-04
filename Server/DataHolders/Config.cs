using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloneCAD.Server.DataHolders
{
    public enum FilterType { None, Whitelist, Blacklist }
    public enum Permission { Civ, Police, Dispatch }

    public class Config : GenericConfig
    {
        public string FilePath { get; }

        public string IP => base["IP"];
        public int Port { get; private set; }

        public FilterType Filter
        {
            get
            {
                if (!int.TryParse(this["FilterType"], out int filter) || filter < 0 || filter > 2)
                    Functions.Error(Locale, "InvalidFilterType", 1);

                return (FilterType)filter;
            }
        }
        public string[] FilteredCivIPs { get; private set; }
        public string[] FilteredPoliceIPs { get; private set; }
        public string[] FilteredDispatchIPs { get; private set; }
        public string Log => base["Log"];
        public AliasDictionary Aliases;
        public LocaleConfig Locale { get; private set; }

        public Config(string FilePath) : base (new List<string>()
        {
            "IP",
            "Port",
        })
        {
            this.FilePath = FilePath;

            Load();
        }

        public new void Load()
        {
            base.Load();
            Locale = new LocaleConfig();
            
            case "IP":
                IP = line[1];
                break;

            case "Port":
                int _Port;

                if (!int.TryParse(line[1], out _Port) || _Port < 1024 || _Port > 65536)
                {
                    Console.WriteLine("The port is invalid.\nMake sure it is a positive integer within 1025-65535.");
                    Environment.Exit(0);
                }

                Port = _Port;
                break;

            case "Filter":
                int _Filter;

                            

                Filter = (FilterType)_Filter;
                break;

            case "FilteredCivIPs":
                if (string.IsNullOrWhiteSpace(line[1]))
                    FilteredCivIPs = new string[0];
                else
                    FilteredCivIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                break;

            case "FilteredPoliceIPs":
                if (string.IsNullOrWhiteSpace(line[1]))
                    FilteredPoliceIPs = new string[0];
                else
                    FilteredPoliceIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                break;

            case "FilteredDispatchIPs":
                if (string.IsNullOrWhiteSpace(line[1]))
                    FilteredDispatchIPs = new string[0];
                else
                    FilteredDispatchIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                break;

            case "Aliases":
                if (string.IsNullOrWhiteSpace(line[1]))
                    Aliases = new List<Alias>();
                else
                    Aliases = line[1].Split(',').Select(x => x.Trim()).Select(x => Alias.Parse(x)).ToList();
                break;
            }
        }

        public bool HasPerm(string ip, Permission perm)
        {
            if (Filter == FilterType.None)
                return true;

            if (Filter == FilterType.Blacklist)
            {
                if (perm == Permission.Civ && !FilteredCivIPs.Contains(ip))
                    return true;

                if (perm == Permission.Police && !FilteredPoliceIPs.Contains(ip))
                    return true;

                if (perm == Permission.Dispatch && !FilteredDispatchIPs.Contains(ip))
                    return true;
            }
            
            if (Filter == FilterType.Whitelist)
            {
                if (perm == Permission.Civ && FilteredCivIPs.Contains(ip))
                    return true;

                if (perm == Permission.Police && FilteredPoliceIPs.Contains(ip))
                    return true;

                if (perm == Permission.Dispatch && FilteredDispatchIPs.Contains(ip))
                    return true;
            }

            return false;
        }
    }
}
