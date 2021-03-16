using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WAPTT.ViewModels;

namespace WAPTT.Views
{
    public class MainWindow : Window
    {
        private Button homeButton;
        private Button historyButton;
        private Button reportButton;
        private Button infoButton;
        private Button step1Button;
        private Button step2Button;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();

            DataContext = new MainWindowViewModel();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            homeButton = this.FindControl<Button>("homeButton");
            historyButton = this.FindControl<Button>("historyButton");
            reportButton = this.FindControl<Button>("reportButton");
            infoButton = this.FindControl<Button>("infoButton");
            step1Button = this.FindControl<Button>("step1Button");
            step2Button = this.FindControl<Button>("step2Button");
        }

        public void Click_Home(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = false;
            historyButton.IsEnabled = true;
            reportButton.IsEnabled = true;
            infoButton.IsEnabled = true;
            step1Button.IsVisible = true;
            step2Button.IsVisible = true;
            step1Button.IsEnabled = false;
            step2Button.IsEnabled = true;
        }

        public void Click_History(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = true;
            historyButton.IsEnabled = false;
            reportButton.IsEnabled = true;
            infoButton.IsEnabled = true;
            step1Button.IsVisible = false;
            step2Button.IsVisible = false;
        }

        public void Click_Report(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = true;
            historyButton.IsEnabled = true;
            reportButton.IsEnabled = false;
            infoButton.IsEnabled = true;
            step1Button.IsVisible = false;
            step2Button.IsVisible = false;
        }

        public void Click_Info(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = true;
            historyButton.IsEnabled = true;
            reportButton.IsEnabled = true;
            infoButton.IsEnabled = false;
            step1Button.IsVisible = false;
            step2Button.IsVisible = false;
        }

        public void Click_Step1(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = false;
            historyButton.IsEnabled = true;
            reportButton.IsEnabled = true;
            infoButton.IsEnabled = true;
            step1Button.IsEnabled = false;
            step2Button.IsEnabled = true;
        }

        public void Click_Step2(object sender, RoutedEventArgs e)
        {
            homeButton.IsEnabled = false;
            historyButton.IsEnabled = true;
            reportButton.IsEnabled = true;
            infoButton.IsEnabled = true;
            step1Button.IsEnabled = true;
            step2Button.IsEnabled = false;
        }
    }
}
