using System.Reactive;
using System.Text;
using System.Windows.Input;
using WAPTT.Commands;

namespace WAPTT.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel = new TestTypeViewModel();


        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
