using System;
using System.Collections.Generic;
using System.Text;
using WAPTT.Scripts;

namespace WAPTT.ViewModels
{
    class ReportViewModel : ViewModelBase
    {
        private string _latestReport = Files_Management.GetLatestReport();

        public string LatestReport
        {
            get { return _latestReport; }
            set
            {
                _latestReport = value;
                OnPropertyChanged(nameof(LatestReport));
            }
        }
    }
}
