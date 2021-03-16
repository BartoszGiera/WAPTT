using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WAPTT.Scripts;
using WAPTT.ViewModels;

namespace WAPTT.Scripts
{
    class Files_Management
    {
        public static string CheckDirectory()
        {
            string dirPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString()).ToString();

            Directory.CreateDirectory(dirPath + "\\Reports");

            dirPath += "\\Reports";

            return dirPath;
        }

        public static void SaveReport()
        {
            string dateTime = DateTime.Now.ToString("s");

            dateTime = dateTime.Replace("T", "--").Replace(':', '-');
            
            using (StreamWriter sw = File.CreateText(CheckDirectory() + $"\\SQLi_Test_{HomeViewModel.domain}--" + dateTime + ".txt"))
            {
                sw.WriteLine(SQL_Is_Vulnerable.reportResults);
                sw.WriteLine(SQL_Injection_UNION_Based.reportResults);
                sw.WriteLine(SQL_Injection_ERROR_Based.reportResults);
            }
        }

        public static string GetReportsList()
        {
            string[] reports = Directory.GetFiles(CheckDirectory());
            string reportsList = String.Empty;

            foreach (string report in reports)
            { 
                reportsList += report.Substring(report.IndexOf("SQL")) + "\n";
            }

            if (reportsList == null || reportsList == String.Empty)
                return "NO REPORTS FOUND";

            return reportsList;
        }

        public static string GetLatestReport()
        {
            string latestReport = String.Empty;

            string latestReportName = String.Empty;
            var files = new DirectoryInfo(CheckDirectory()).GetFiles();

            DateTime lastWritten = DateTime.MinValue;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWritten)
                {
                    lastWritten = file.LastWriteTime;
                    latestReportName = file.Name;
                }
            }

            if (latestReportName == null || latestReportName == String.Empty)
                return "NO REPORTS FOUND";

            using (StreamReader sr = File.OpenText(CheckDirectory() + "\\" + latestReportName))
            {
                latestReport = sr.ReadToEnd();
            }

            return latestReport;
        }
    }
}
