using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class HomeView : UserControl
    {
        private Button run_Button;

        public static CheckBox union_CheckBox;
        public static CheckBox error_CheckBox;

        public HomeView()
        {
            this.InitializeComponent();

            DataContext = new TestTypeViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            run_Button = this.FindControl<Button>("run_Button");
            union_CheckBox = this.FindControl<CheckBox>("UnionBasedCheckBox");
            error_CheckBox = this.FindControl<CheckBox>("ErrorBasedCheckBox");
        }

        public void Click_Run(object sender, RoutedEventArgs e)
        {
            run_Button.IsEnabled = false;
        }

        public void Checked_Union_Based(object sender, RoutedEventArgs e)
        {
            TestTypeViewModel.scripts.Add("UNION_BASED");
        }

        public void Checked_Error_Based(object sender, RoutedEventArgs e)
        {
            TestTypeViewModel.scripts.Add("ERROR_BASED");
        }

        public void Unchecked_Union_Based(object sender, RoutedEventArgs e)
        {
            TestTypeViewModel.scripts.Remove("UNION_BASED");
        }

        public void Unchecked_Error_Based(object sender, RoutedEventArgs e)
        {
            TestTypeViewModel.scripts.Remove("ERROR_BASED");
        }
    }
}
