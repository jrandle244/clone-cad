using System;
using System.IO;
using System.Windows.Forms;
using CloneCAD.Common.DataHolders;

namespace CloneCAD.Client
{
    public static class Functions
    {
        public static void Error(string LocaleKey, LocaleConfig Locale, int ExitCode)
        {
            MessageBox.Show(Locale[LocaleKey], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(ExitCode);
        }

        public static void ExceptionHandler(LocaleConfig Locale, Exception e, int ExitCode)
        {
            MessageBox.Show(Locale["UnexpectedErrorMessage"] == "LOCALE ERROR" ? "An unexpected error has occured. Please open a GitHub issue with the label \"crash\" and put the contents of the error log in a code block in the post.\n\nError log: " + errorFile : Locale["UnexpectedErrorMessage"] + errorFile, "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Common.Functions.ExceptionHandlerBackend(e, ExitCode);
        }
    }
}
