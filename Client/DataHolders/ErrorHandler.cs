using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

namespace CloneCAD.Client.DataHolders
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
                    Error("TryGetInvalid", caller, lineNumber, filePath);
                    break;
                case NetRequestResult.Incompleted:
                    Error("TryGetIncomplete", caller, lineNumber, filePath);
                    break;
            }

            Environment.Exit(4);
        }

        public void Error(string localeKey) =>
            MessageBox.Show(Locale?[localeKey] ?? localeKey, @"CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void Error(string localeKey, params object[] objs) =>
            MessageBox.Show(Locale?[localeKey, objs] ?? string.Format(localeKey, objs), @"CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void Error(string localeKey, int exitCode)
        {
            Error(localeKey);
            Environment.Exit(exitCode);
        }

        public void Error(string localeKey, int exitCode, params object[] objs)
        {
            Error(localeKey, objs);
            Environment.Exit(exitCode);
        }

        public void ExceptionHandler(Exception e, int exitCode)
        {
            if (Locale == null)
                MessageBox.Show(@"The error has been saved to a log (" + e.ExceptionHandlerBackend() + @").\nPlease post an issue on GitHub and put the contents of the log in a code block.", @"CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(Locale["UnexpectedError"].StartsWith("LOCALE ERROR (") ? "The error has been saved to a log (" + e.ExceptionHandlerBackend() + ").\nPlease post an issue on GitHub and put the contents of the log in a code block." : Locale["UnexpectedError", e.ExceptionHandlerBackend()], @"CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Environment.Exit(exitCode);
        }
    }
}
