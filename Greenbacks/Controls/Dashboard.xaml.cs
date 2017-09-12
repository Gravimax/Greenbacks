using Greenbacks.ViewModels;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            this.DataContext = new DashboardViewModel();
        }
    }
}
