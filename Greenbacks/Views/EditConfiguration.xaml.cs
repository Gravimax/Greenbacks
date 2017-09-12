using Greenbacks.Models;
using System.Windows;

namespace Greenbacks.Views
{
    /// <summary>
    /// Interaction logic for EditConfiguration.xaml
    /// </summary>
    public partial class EditConfiguration : Window
    {
        public GreenbacksConfiguration TempConfig { get; set; }
        private GreenbacksConfiguration greenbacksConfiguration;

        public EditConfiguration(GreenbacksConfiguration configuration)
        {
            InitializeComponent();
            this.DataContext = this;
            this.greenbacksConfiguration = configuration;
            TempConfig = CopyFrom(configuration);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CopyTo(TempConfig, greenbacksConfiguration);
            this.DialogResult = true;
            this.Close();
        }

        private GreenbacksConfiguration CopyFrom(GreenbacksConfiguration configuration)
        {
            GreenbacksConfiguration config = new GreenbacksConfiguration { RememberLastLocation = configuration.RememberLastLocation };
            return config;
        }

        private void CopyTo(GreenbacksConfiguration tempConfig, GreenbacksConfiguration configuration)
        {
            configuration.RememberLastLocation = tempConfig.RememberLastLocation;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
