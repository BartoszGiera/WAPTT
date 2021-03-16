using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class TestTypeView : UserControl
    {

        public TestTypeView()
        {
            this.InitializeComponent();

            DataContext = new HomeViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
