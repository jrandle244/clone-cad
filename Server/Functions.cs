using CloneCAD.Common.DataHolders;

using System;

namespace CloneCAD.Server
{
    public static class Functions
    {
        public static void Error(LocaleConfig Locale, string LocaleError, int ExitCode)
        {
            Console.WriteLine(Locale[LocaleError] + "\nPress any key to exit.");
            Console.ReadKey();
            Environment.Exit(ExitCode);
        }

        public static void Error(LocaleConfig Locale, string LocaleError, Exception e, int ExitCode)
        {
            Console.WriteLine(Locale[LocaleError] + "\n" + Locale["ErrorSaved"] + "\nPress any key to exit.");
            Console.ReadKey();
            Environment.Exit(ExitCode);
        }
    }
}
