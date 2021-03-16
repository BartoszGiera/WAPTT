using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WAPTT.ViewModels;

namespace WAPTT.Commands
{
    class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new HomeViewModel();
            }
            else if(parameter.ToString() == "History")
            {
                viewModel.SelectedViewModel = new HistoryViewModel();
            }
            else if (parameter.ToString() == "Report")
            {
                viewModel.SelectedViewModel = new ReportViewModel();
            }
            else if (parameter.ToString() == "Info")
            {
                viewModel.SelectedViewModel = new InfoViewModel();
            }
            else if (parameter.ToString() == "TestType")
            {
                viewModel.SelectedViewModel = new TestTypeViewModel();
            }
        }
    }
}
