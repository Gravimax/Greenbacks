using Greenbacks.Models;
using Greenbacks.ViewModels;
using System.Windows;

namespace Greenbacks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ApplicationConfiguration.GreenbacksConfig = ApplicationConfiguration.Load();
            this.DataContext = new ApplicationViewModel();
            SetWindow(ApplicationConfiguration.GreenbacksConfig);

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.ExitAppMessage>(this, (message) =>
            {
                this.Close();
            });
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveConfiguration(this.Left, this.Top, this.ActualHeight, this.ActualWidth);
        }


        private void SetWindow(GreenbacksConfiguration configuration)
        {
            if (configuration.RememberLastLocation)
            {
                if (configuration.WindowTop > 0)
                {
                    this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                    this.Top = configuration.WindowTop;
                    this.Left = configuration.WindowLeft;
                }
                else
                {
                    this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                }
            }
            else
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }

            if (configuration.RememberWindowSize)
            {
                this.Height = configuration.WindowHeight;
                this.Width = configuration.WindowWidth;
            }
        }


        public void SaveConfiguration(double left, double top, double height, double width)
        {
            ApplicationConfiguration.GreenbacksConfig.WindowLeft = left;
            ApplicationConfiguration.GreenbacksConfig.WindowTop = top;
            ApplicationConfiguration.GreenbacksConfig.WindowHeight = height;
            ApplicationConfiguration.GreenbacksConfig.WindowWidth = width;
            ApplicationConfiguration.Save(ApplicationConfiguration.GreenbacksConfig);
        }
    }
}
