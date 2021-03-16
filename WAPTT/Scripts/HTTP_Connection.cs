using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using WAPTT.ViewModels;
using System.IO;

namespace WAPTT.Scripts
{
    class HTTP_Connection
    {
        public static string Response(string sqlURL)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(sqlURL);
            httpRequest.Method = "GET";
            httpRequest.CookieContainer = HomeViewModel.cookieContainer;

            string httpResponse = String.Empty;

            using (StreamReader reader = new StreamReader(httpRequest.GetResponse().GetResponseStream()))
            {
                httpResponse = reader.ReadToEnd();
            }

            return httpResponse;
        }
    }
}
