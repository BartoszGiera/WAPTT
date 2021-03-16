using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WAPTT.Scripts;

namespace WAPTT.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public static string url;
        
        private string _url;

        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(URL));
                url = value;
            }
        }

        private string _cookieName;

        public string CookieName
        {
            get { return _cookieName; }
            set
            {
                _cookieName = value;
                OnPropertyChanged(nameof(CookieName));
            }
        }

        private string _cookieValue;

        public string CookieValue
        {
            get { return _cookieValue; }
            set
            {
                _cookieValue = value;
                OnPropertyChanged(nameof(CookieValue));
            }
        }

        private string _cookiePath;

        public string CookiePath
        {
            get { return _cookiePath; }
            set
            {
                _cookiePath = value;
                OnPropertyChanged(nameof(CookiePath));
            }
        }

        public static string domain;

        private string _cookieDomain;

        public string CookieDomain
        {
            get { return _cookieDomain; }
            set
            {
                _cookieDomain = value;
                OnPropertyChanged(nameof(CookieDomain));
            }
        }

        private string _cookiesList;

        public string CookieList
        {
            get { return _cookiesList; }
            set
            {
                _cookiesList = value;
                OnPropertyChanged(nameof(CookieList));
            }
        }

        public static CookieContainer cookieContainer = new CookieContainer();

        public void AddCookie()
        {
            CookieList += $"\"{CookieName}\", \"{CookieValue}\", \"{CookiePath}\", \"{CookieDomain}\"\n";

            Cookie cookie = new Cookie($"{CookieName}", $"{CookieValue}", $"{CookiePath}", $"{CookieDomain}");
            cookieContainer.Add(cookie);

            domain = CookieDomain;

            CookieName = String.Empty;
            CookieValue = String.Empty;
            CookiePath = String.Empty;
            CookieDomain = String.Empty;
        }
    }
}
