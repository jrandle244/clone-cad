using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using CloneCAD.Common.DataHolders;

namespace CloneCAD.Client
{
    public static class Functions
    {
        public static void GetFailTest(bool tryGetResult)
        {
            if (tryGetResult)
                return;

            MessageBox.Show("Server does not contain the value or function that has been \"TryGet\"ed! If this continues please submit a bug report.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
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