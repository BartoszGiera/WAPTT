using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using WAPTT.Scripts;
using WAPTT.ViewModels;
using WAPTT.Views;

namespace WAPTT.ViewModels
{
    class TestTypeViewModel : ViewModelBase
    {
        public static List<string> scripts = new List<string>();

        public void Run()
        {
            if (scripts.Contains("UNION_BASED"))
            SQL_Injection_UNION_Based.Script(HomeViewModel.url);

            if (scripts.Contains("ERROR_BASED"))
            SQL_Injection_ERROR_Based.Script(HomeViewModel.url);

            Files_Management.SaveReport();
        }
    }
}
