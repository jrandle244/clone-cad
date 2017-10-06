using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace CloneCAD.Common
{
    public static class Functions
    {
        public static void ExceptionHandlerBackend(this Exception e, int exitCode)
        {
            string errorFile = "error0.dump";

            {
                ushort reiteration = 0;

                while (File.Exists(errorFile))
                    errorFile = "error" + ++reiteration + ".dump";
            }

            Environment.Exit(exitCode);
        }
    }
}
