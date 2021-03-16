using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class InfoView : UserControl
    {
        public InfoView()
        {
            this.InitializeComponent();

            DataContext = new InfoViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
