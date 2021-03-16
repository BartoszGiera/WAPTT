using System;
using System.Collections.Generic;
using System.Text;
using WAPTT.Scripts;

namespace WAPTT.ViewModels
{
    class HistoryViewModel : ViewModelBase
    {
        private string _reportsHistory = Files_Management.GetReportsList();

        public string ReportsHistory
        {
            get { return _reportsHistory; }
            set
            {
                _reportsHistory = value;
                OnPropertyChanged(nameof(ReportsHistory));
            }
        }
    }
}
