using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.Media;
using CloneCAD.Common;

namespace CloneCAD.Server
{
    public static class ServerFunctions
    {
        //I hate playing sounds esp in a console program, but in order to attract the user it's probably a good idea.

        public static void Error(string error, int exitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(error + "\nPress any key to exit.");

            Console.ReadKey();
            Environment.Exit(exitCode);
        }

        public static void Error(string error, Exception e, int exitCode)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(error + "\nThe error has been saved in the same directory as the server.\nPress any key to exit.");

            Console.ReadKey();
            e.ExceptionHandlerBackend();
        }

        public static void Error(LocaleConfig locale, string localeError, int exitCode, params object[] formatterObjs)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(string.Format(locale[localeError], formatterObjs) + "\n" + locale["PressAnyKeyToExit"]);

            Console.ReadKey();
            Environment.Exit(exitCode);
        }

        public static void Error(LocaleConfig locale, string localeError, Exception e, int exitCode, params object[] formatterObjs)
        {
            SystemSounds.Exclamation.Play();

            Console.WriteLine(string.Format(locale[localeError], formatterObjs) + "\n" + locale["ErrorSaved"] + locale["PressAnyKeyToExit"]);

            Console.ReadKey();
            e.ExceptionHandlerBackend();
        }
    }
}
