using System;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.DataHolders;

namespace CloneCAD.Server.DataHolders
{
    public class ErrorHandler
    {
        public LocaleConfig Locale { get; set; }

        public ErrorHandler(LocaleConfig locale) =>
            Locale = locale;

        public void Error(string localeKey, int exitCode)
        {
            Console.WriteLine(Locale?[localeKey] ?? localeKey);
            Console.ReadKey();
            Environment.Exit(exitCode);
        }

        public void Error(string localeKey, int exitCode, params object[] objs)
        {
            Console.WriteLine(Locale?[localeKey, objs] ?? string.Format(localeKey, objs));
            Environment.Exit(exitCode);
        }

        public void ExceptionHandler(Exception e, int exitCode)
        {
            if (Locale == null)
                Console.WriteLine(@"The error has been saved to a log (" + e.ExceptionHandlerBackend() + @").\nPlease upload the error log to the Discord server or post it to GitHub.");
            else
                MessageBox.Show(Locale["UnexpectedErrorMsg"].StartsWith("LOCALE ERROR (") ? @"The error has been saved to a log (" + e.ExceptionHandlerBackend() + @").\nPlease upload the error log to the Discord server or post it to GitHub." : Locale["UnexpectedError", e.ExceptionHandlerBackend()]);

            Environment.Exit(exitCode);
        }
    }
}
