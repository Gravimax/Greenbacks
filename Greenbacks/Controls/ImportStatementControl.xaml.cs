using Greenbacks.Common.Models;
using Greenbacks.ViewModels;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for ImportStatementControl.xaml
    /// </summary>
    public partial class ImportStatementControl : UserControl
    {
        public ImportStatementControl()
        {
            InitializeComponent();
            this.DataContext = new ImportStatementViewModel();
        }
    }
}
