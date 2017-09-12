using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Windows;

namespace Greenbacks.View
{
    /// <summary>
    /// Interaction logic for TransferFunds.xaml
    /// </summary>
    public partial class TransferFunds : Window
    {
        private List<AccountModel> accounts;
        public List<AccountModel> Accounts { get { return accounts; } set { accounts = value; } }
        private TransferFundsArgs result;
        public TransferFundsArgs Result { get { return result; } set { result = value; } }

        public TransferFunds(List<AccountModel> accounts)
        {
            InitializeComponent();
            Result = new TransferFundsArgs();
            this.Accounts = accounts;
            this.DataContext = this;
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFromAccount.SelectedItem != null && cmdToAccount.SelectedItem != null)
            {
                result.FromAccountId = ((AccountModel)cmbFromAccount.SelectedItem).Id;
                result.ToAccountId = ((AccountModel)cmdToAccount.SelectedItem).Id;
                result.TransferAmount = decimal.Parse(txtAmount.Text.ToString());
                result.Memo = txtMemo.Text.ToString();

                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void txtAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAmount.SelectionStart = 0;
            txtAmount.SelectionLength = txtAmount.Text.Length;
        }
    }
}
