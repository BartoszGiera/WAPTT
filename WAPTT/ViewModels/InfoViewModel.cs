using System;
using System.Collections.Generic;
using System.Text;

namespace WAPTT.ViewModels
{
    class InfoViewModel : ViewModelBase
    {
        private string _info = "Web application penetration testing tool written in C# and XAML.\n\nSupported test scenarios:\nUNION-Based SQLi\nERROR-Based SQLi";

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged(nameof(Info));
            }
        }
    }
}
