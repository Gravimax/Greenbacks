using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Models;
using Greenbacks.RecordValidation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Greenbacks.Views
{
    /// <summary>
    /// Interaction logic for TransactionEntry.xaml
    /// </summary>
    public partial class TransactionEntry : Window, INotifyPropertyChanged
    {
        private List<PayeeModel> _payeeList;
        public List<PayeeModel> PayeeList 
        {
            get { return _payeeList; }
            set
            {
                _payeeList = value;
                OnPropertyChanged("PayeeList");
            }
        }

        private List<GenericComboItem> _categoryList;
        public List<GenericComboItem> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                OnPropertyChanged("CategoryList");
            }
        }

        private TransactionModel transaction;

        private TransactionModel _selectedTransaction;
        public TransactionModel SelectedTransaction
        {
            get { return _selectedTransaction; }
            set 
            {
                _selectedTransaction = value;
                OnPropertyChanged("SelectedTransaction");
            }
        }

        public bool IsNewPayee = false;
        public bool IsNewCategory = false;

        public TransactionEntry(TransactionModel transaction, List<PayeeModel> payeeList, List<GenericComboItem> categoryList)
        {
            InitializeComponent();
            this.transaction = transaction;
            SelectedTransaction = TransToTemp(transaction);
            this.DataContext = this;

            this.PayeeList = payeeList;
            this.CategoryList = categoryList;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TransactionValidation vr = new TransactionValidation(_selectedTransaction);
            vr.Validate();

            if (vr.HasErrors)
            {
                MessageBox.Show(vr.Message, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TempToTrans(SelectedTransaction);
            SelectedTransaction = transaction;
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedTransaction = null;
            this.DialogResult = false;
            this.Close();
        }

        private void cmbPayee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedTransaction != null)
            {
                if (cmbPayee.SelectedItem != null)
                {
                    TransactionModel trans = new DALTransactions().GetPreviousTransaction(transaction.AccountId, ((PayeeModel)cmbPayee.SelectedItem).Id);
                    if (trans != null)
                    {
                        SelectedTransaction.CategoryId = trans.CategoryId;
                        SelectedTransaction.Debit = trans.Debit;
                        SelectedTransaction.Credit = trans.Credit;
                    }
                }
            }
        }

        private void cmbPayee_LostFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null) { return; }
            if (!string.IsNullOrEmpty(comboBox.Text))
            {
                if (PayeeList.FirstOrDefault(x => x.Name == comboBox.Text.Trim()) == null)
                {
                    PayeeModel newPayee = new PayeeModel { Name = comboBox.Text.Trim() };

                    DALPayees payees = new DALPayees();
                    payees.CreatePayee(newPayee);
                    PayeeList.Add(newPayee);
                    PayeeList = PayeeList.OrderBy(x => x.Name).ToList();
                    comboBox.SelectedItem = newPayee;
                    SelectedTransaction.PayeeId = newPayee.Id;
                    IsNewPayee = true;
                }
            }
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddEditCategory ac = new AddEditCategory();
            ac.Owner = this;
            ac.ShowDialog();

            if (ac.DialogResult == true)
            {
                DALCategories categories = new DALCategories();
                categories.CreateCategory(ac.SelectedCategory);
                CategoryList = categories.GetCategoriesList();
                cmbCategory.SelectedItem = ac.SelectedCategory;
                IsNewCategory = true;
            }
        }

        private TransactionModel TransToTemp(TransactionModel transaction)
        {
            return new TransactionModel
            {
                CategoryId = transaction.CategoryId,
                CheckNumber = transaction.CheckNumber,
                Clear = transaction.Clear,
                Debit = transaction.Debit,
                Expense = transaction.Expense,
                Id = transaction.Id,
                Memo = transaction.Memo,
                PayeeId = transaction.PayeeId,
                Reimbersable = transaction.Reimbersable,
                Taxable = transaction.Taxable,
                TaxDeductable = transaction.TaxDeductable,
                TransactionDate = transaction.TransactionDate,
                Credit = transaction.Credit
            };
        }

        private void TempToTrans(TransactionModel newTransaction)
        {
            transaction.CategoryId = newTransaction.CategoryId;
            transaction.CheckNumber = newTransaction.CheckNumber;
            transaction.Clear = newTransaction.Clear;
            transaction.Debit = newTransaction.Debit;
            transaction.Expense = newTransaction.Expense;
            transaction.Memo = newTransaction.Memo;
            transaction.PayeeId = newTransaction.PayeeId;
            transaction.Reimbersable = newTransaction.Reimbersable;
            transaction.Taxable = newTransaction.Taxable;
            transaction.TaxDeductable = newTransaction.TaxDeductable;
            transaction.TransactionDate = newTransaction.TransactionDate;
            transaction.Credit = newTransaction.Credit;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
