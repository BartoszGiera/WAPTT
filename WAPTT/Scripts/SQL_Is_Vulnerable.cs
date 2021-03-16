using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using WAPTT.ViewModels;

namespace WAPTT.Scripts
{
    class SQL_Is_Vulnerable
    {
        public static string reportResults = String.Empty;
        
        public static string URLParameters(string url)
        {
            reportResults = "--- Looking for SQLi vulnerabilities ---\n\n\n";
            
            string vulnerable_parms = String.Empty;

            int index = url.IndexOf('?');
            string[] parms = url.Remove(0, index + 1).Split('&');

            foreach (string parm in parms)
            {
                string sqlURL = url.Replace(parm, parm + "aa'1");

                if (HTTP_Connection.Response(sqlURL).Contains("error in your SQL"))
                {
                    vulnerable_parms += parm + "&";
                    reportResults += $"Found SQL vulnerability in parameter: {parm}\n";
                }
                else
                {
                    continue;
                }
            }

            return vulnerable_parms.Substring(0, vulnerable_parms.Length - 1);
        }
    }
}
