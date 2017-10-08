using System;
using System.IO;

namespace CloneCAD.Common
{
    public static class Functions
    {
        public static string ExceptionHandlerBackend(this Exception e)
        {
            string errorFile = "error0.dump";
            
            ushort reiteration = 0;

            while (File.Exists(errorFile))
                errorFile = "error" + ++reiteration + ".dump";

            return errorFile;
        }

        public static string ToSplitID(this uint rawValue)
        {
            try
            {
                char[] idC = rawValue.ToString().ToCharArray();

                return new string(new[]
                {
                    idC[0],
                    idC[1],
                    idC[2],
                    '-',
                    idC[3],
                    idC[4],
                    idC[5],
                    '-',
                    idC[6],
                    idC[7],
                    idC[8]
                });
            }
            catch
            {
                return rawValue.ToString();
            }
        }

        public static uint ToRawID(this string id)
        {
            try
            {
                char[] idC = id.ToCharArray();

                return uint.Parse(new string(new[]
                {
                    idC[0],
                    idC[1],
                    idC[2],

                    idC[4],
                    idC[5],
                    idC[6],

                    idC[8],
                    idC[9],
                    idC[10],
                }));
            }
            catch
            {
                return uint.Parse(id.Replace("-", ""));
            }
        }

        public static bool TryToRawID(this string id, out uint rawID)
        {
            char[] idC = id.ToCharArray();

            if (idC.Length == 11 && uint.TryParse(new string(new[]
            {
                idC[0],
                idC[1],
                idC[2],

                idC[4],
                idC[5],
                idC[6],

                idC[8],
                idC[9],
                idC[10],
            }), out rawID))
                return true;

            if (uint.TryParse(id.Replace("-", ""), out rawID))
                return true;

            rawID = 0;
            return false;
        }
    }
}
