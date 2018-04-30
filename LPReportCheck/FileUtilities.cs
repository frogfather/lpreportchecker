using System;
using System.Collections.Generic;
using System.IO;

namespace LPReportCheck
{
    public static class FileUtilities
    {
        private static String GetDirectoryFromFileName(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf("\\",StringComparison.Ordinal) + 1);
        }

        private static DateTime GetFileDate(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.GetCreationTime(fileName);
            }
            else
            {
                return Convert.ToDateTime("11/11/2222");
            }

        }
    }
}
