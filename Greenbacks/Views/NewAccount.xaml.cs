using Greenbacks.Common.Models;
using Greenbacks.Helpers;
using Greenbacks.Models;
using Greenbacks.RecordValidation;
using System.Collections.Generic;
using System.Windows;

namespace Greenbacks.View
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        public List<AccountTypeModel> accountTypes { get; set; }

        public NewAccountModel NewSelectedAccount { get; set; }

        public NewAccount(List<AccountTypeModel> accountTypes)
        {
            InitializeComponent();
            NewSelectedAccount = new NewAccountModel();
            NewSelectedAccount.AccountTypeId = 1;
            NewSelectedAccount.StartingBalance = 0.0M;
            this.accountTypes = accountTypes;
            this.cmbAccountType.ItemsSource = this.accountTypes;
            this.DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NewAccountValidation vr = new NewAccountValidation(NewSelectedAccount);
            vr.Validate();

            if (vr.HasErrors)
            {
                MessageBox.Show(vr.Message, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            NewSelectedAccount.Asset = ((AccountTypeModel)cmbAccountType.SelectedItem).Asset;
            if (!string.IsNullOrEmpty(pswdAccountNumber.Password))
            {
                NewSelectedAccount.AccountNumber = Security.GetMd5Hash(pswdAccountNumber.Password.Trim());
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void txtStartingBalance_GotFocus(object sender, RoutedEventArgs e)
        {
            txtStartingBalance.SelectionStart = 0;
            txtStartingBalance.SelectionLength = txtStartingBalance.Text.Length;
        }
    }
}
