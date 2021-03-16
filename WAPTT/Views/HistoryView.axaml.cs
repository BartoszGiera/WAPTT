using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class HistoryView : UserControl
    {
        public HistoryView()
        {
            this.InitializeComponent();

            DataContext = new HistoryViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
