using CloneCAD.Common.DataHolders;

using System;
using System.IO;
using System.Media;

namespace CloneCAD.Server
{
    public static class Functions
    {
        //I hate playing sounds esp in a console program, but in order to attract the user it's probably a good idea.

        public static void Error(string Error, int ExitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(Error + "\nPress any key to exit.");

            Console.ReadKey();
            Environment.Exit(ExitCode);
        }

        public static void Error(string Error, Exception e, int ExitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(Error + "\nThe error has been saved in the same directory as the server.\nPress any key to exit.");

            Console.ReadKey();
            Common.Functions.ExceptionHandlerBackend(e, ExitCode);
        }

        public static void Error(LocaleConfig Locale, string LocaleError, int ExitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(Locale[LocaleError] + "\n" + Locale["PressAnyKeyToExit"]);
            Console.ReadKey();
            Environment.Exit(ExitCode);
        }

        public static void Error(LocaleConfig Locale, string LocaleError, Exception e, int ExitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(Locale[LocaleError] + "\n" + Locale["ErrorSaved"] + Locale["PressAnyKeyToExit"]);

            Console.ReadKey();
            Common.Functions.ExceptionHandlerBackend(e, ExitCode);
        }
    }
}
