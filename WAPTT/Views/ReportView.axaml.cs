using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class ReportView : UserControl
    {
        public ReportView()
        {
            this.InitializeComponent();

            DataContext = new ReportViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
