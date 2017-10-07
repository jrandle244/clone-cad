using System;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

namespace CloneCAD.Client
{
    public static class Functions
    {
        public static void GetFailTest(NetRequestResult tryGetResult, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            switch (tryGetResult)
            {
                case NetRequestResult.Completed:
                    return;
                case NetRequestResult.Invalid:
                    MessageBox.Show("Server does not contain the function that has been \"TryGet\"ed! If this continues please submit a bug report.\n\nAt: " + caller + " (line " + lineNumber + ") in " + caller, "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case NetRequestResult.Incompleted:
                    MessageBox.Show("Server execute the function that has been \"TryGet\"ed! If this continues please submit a bug report.\n\nAt: " + caller + " (line " + lineNumber + ") in " + caller , "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            Environment.Exit(4);
        }

        public static void Error(string localeKey, LocaleConfig locale, int exitCode)
        {
            MessageBox.Show(locale[localeKey], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(exitCode);
        }

        public static void ExceptionHandler(Exception e, int exitCode)
        {
            MessageBox.Show("An unexpected error has occured. Please open a GitHub issue with the label \"crash\" and put the contents of the error log in a code block in the post, or fill out a bug report form.\n\nError log: " + Common.Functions.ExceptionHandlerBackend(e, exitCode), "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Environment.Exit(exitCode);
        }

        public static void ExceptionHandler(LocaleConfig locale, Exception e, int exitCode)
        {
            MessageBox.Show(locale["UnexpectedErrorMessage"] == "LOCALE ERROR" ? "An unexpected error has occured. Please open a GitHub issue with the label \"crash\" and put the contents of the error log in a code block in the post, or fill out a bug report form.\n\nError log: " + Common.Functions.ExceptionHandlerBackend(e, exitCode) : locale["UnexpectedErrorMessage"] + Common.Functions.ExceptionHandlerBackend(e, exitCode), "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Environment.Exit(exitCode);
        }
    }
}