using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

namespace CloneCAD.Server.DataHolders
{
    public class ErrorHandler
    {
        public LocaleConfig Locale { get; set; }

        public ErrorHandler(LocaleConfig locale) =>
            Locale = locale;

        public void GetFailTest(NetRequestResult tryGetResult, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            switch (tryGetResult)
            {
                case NetRequestResult.Completed:
                    return;
                case NetRequestResult.Invalid:
                    Error("TryGetInvalid", 5, caller, lineNumber, filePath);
                    break;
                case NetRequestResult.Incompleted:
                    Error("TryGetIncomplete", 5, caller, lineNumber, filePath);
                    break;
            }

            Environment.Exit(4);
        }

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
                MessageBox.Show(Locale["UnexpectedError"].StartsWith("LOCALE ERROR (")
                    ? @"The error has been saved to a log (" + e.ExceptionHandlerBackend() + @").\nPlease upload the error log to the Discord server or post it to GitHub."
                    : Locale["UnexpectedError", e.ExceptionHandlerBackend()]);

            Environment.Exit(exitCode);
        }
    }
}
