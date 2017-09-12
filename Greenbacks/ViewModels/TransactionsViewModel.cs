using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Helpers;
using Greenbacks.Models;
using Greenbacks.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Greenbacks.ViewModels
{
    public class TransactionsViewModel : ViewModelBase
    {
        #region Properties


        DALTransactions dal_Trans = new DALTransactions();

        private int accountId;


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


        private ObservableCollection<TransactionModel> transList;

        private ObservableCollection<TransactionModel> _transactions;
        public ObservableCollection<TransactionModel> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value.OrderBy(e => e.TransactionDate).ToObservableCollection();
                OnPropertyChanged("Transactions");
                if (_transactions.Count >= 1)
                {
                    OnScrollToRecord(_transactions[_transactions.Count - 1]);
                }

                RefreshTotals();
            }
        }


        // Deposit
        public decimal AccountCredits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate <= DateTime.Now).Sum(x => (decimal?)x.Credit) ?? 0;
                }

                return 0;
            }
        }


        public decimal EndOfMonthCredits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate <= DateUtilities.GetLastDayOfMonth(DateTime.Now)).Sum(x => (decimal?)x.Credit) ?? 0;
                }

                return 0;
            }
        }


        public decimal FutureCredits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate > DateTime.Now).Sum(x => (decimal?)x.Credit) ?? 0;
                }

                return 0;
            }
        }


        // Withdrawal
        public decimal AccountDebits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate <= DateTime.Now).Sum(x => (decimal?)x.Debit) ?? 0;
                }

                return 0;
            }
        }


        public decimal EndOfMonthDebits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate <= DateUtilities.GetLastDayOfMonth(DateTime.Now)).Sum(x => (decimal?)x.Debit) ?? 0;
                }

                return 0;
            }
        }


        public decimal FutureDebits
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions.Where(y => y.TransactionDate > DateTime.Now).Sum(x => (decimal?)x.Debit) ?? 0;
                }

                return 0;
            }
        }


        public decimal AccountBalance
        {
            get { return AccountCredits - AccountDebits; }
        }


        public decimal EndOfMonthBalance
        {
            get { return EndOfMonthCredits - EndOfMonthDebits; }
        }


        public decimal FutureBalance
        {
            get 
            {
                if (_transactions != null)
                {
                    decimal credit = _transactions.Sum(x => (decimal?)x.Credit) ?? 0;
                    decimal debit = _transactions.Sum(x => (decimal?)x.Debit) ?? 0;
                    return credit - debit;
                }

                return 0;
            }
        }


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


        #endregion


        public DelegateCommand NewTransactionCommand { get; private set; }
        public DelegateCommand EditTransactionCommand { get; private set; }
        public DelegateCommand DeleteTransactionCommand { get; private set; }
        public DelegateCommand DoubleClickItemCommand { get; private set; }

        public DelegateCommand CLRChangeCommand { get; private set; }


        public TransactionsViewModel()
        {
            CategoryList = new DALCategories().GetCategoriesList();
            PayeeList = new DALPayees().GetPayees().OrderBy(x => x.Name).ToList();

            NewTransactionCommand = new DelegateCommand(CreateNewTransaction, CanCreateTransaction);
            EditTransactionCommand = new DelegateCommand(EditTransaction, CanEditTransaction);
            DeleteTransactionCommand = new DelegateCommand(DeleteSelectedTransaction, CanDeleteTransaction);
            DoubleClickItemCommand = new DelegateCommand(TransDoubleClick);
            CLRChangeCommand = new DelegateCommand(CLRChange);
        }


        public void GetTransactions(int accountId)
        {
            try
            {
                this.accountId = accountId;
                transList = dal_Trans.GetAccountTransactions(accountId);
                Transactions = transList.ToObservableCollection();
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        public void CLRChange(object args)
        {
            try
            {
                if (args != null)
                {
                    TransactionModel model = args as TransactionModel;
                    if (model != null)
                    {
                        dal_Trans.UpdateTransClr(model.Id, model.Clear);
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private bool CanCreateTransaction(object args)
        {
            return true;
        }


        private void CreateNewTransaction(object args)
        {
            try
            {
                TransactionEntry te = new TransactionEntry(new TransactionModel { AccountId = accountId, TransactionDate = DateTime.Now }, PayeeList, CategoryList);

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    te.Owner = owner;
                }

                te.ShowDialog();

                if (te.DialogResult == true)
                {
                    dal_Trans.CreateTransaction(te.SelectedTransaction);
                    Transactions.Add(te.SelectedTransaction);
                    Transactions = Transactions.OrderBy(e => e.TransactionDate).ToObservableCollection();
                    OnMessageUpdated("Transaction Added", false);
                    RefreshTotals();
                    CalcBalances();
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private void TransDoubleClick(object args)
        {
            if (CanEditTransaction(null))
            {
                EditTransaction(args);
            }
        }


        private bool CanEditTransaction(object args)
        {
            return SelectedTransaction != null;
        }


        private void EditTransaction(object args)
        {
            try
            {
                if (SelectedTransaction != null)
                {
                    TransactionEntry te = new TransactionEntry(SelectedTransaction, PayeeList, CategoryList);

                    System.Windows.Window owner = args as System.Windows.Window;
                    if (owner != null)
                    {
                        te.Owner = owner;
                    }

                    te.ShowDialog();
                    if (te.DialogResult.Equals(true))
                    {
                        dal_Trans.UpdateTransaction(SelectedTransaction);
                        OnMessageUpdated("Transaction Saved", false);
                        RefreshTotals();
                        CalcBalances();

                        if (te.IsNewPayee) { PayeeList = new DALPayees().GetPayees(); }
                        if (te.IsNewCategory) { CategoryList = new DALCategories().GetCategoriesList(); }
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private bool CanDeleteTransaction(object args)
        {
            return SelectedTransaction != null;
        }


        private void DeleteSelectedTransaction(object args)
        {
            try
            {
                if (Utilities.ShowDialogBox("Delete Transaction?") == true)
                {
                    dal_Trans.DeleteTransaction(SelectedTransaction);
                    Transactions.Remove(SelectedTransaction);
                    SelectedTransaction = null;
                    OnMessageUpdated("Transaction Deleted", false);
                    RefreshTotals();
                    CalcBalances();
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        decimal balance = 0;

        private void CalcBalances()
        {
            balance = 0;

            foreach (var item in Transactions.OrderBy(x => x.TransactionDate).ToArray())
            {
                if (item.Credit > 0)
                {
                    balance += item.Credit;
                }
                else
                {
                    balance -= item.Debit;
                }

                item.Balance = balance;
            }
        }


        private string previousSort;

        public void SortTransactions(string sortBy, ListSortDirection direction)
        {
            previousSort = sortBy;

            switch (sortBy)
            {
                case "Date":
                    Transactions.Sort(e => e.TransactionDate, direction);
                    break;
                case "ChkNum":
                    Transactions.Sort(e => e.CheckNumber, direction);
                    break;
                case "Payee":
                    Transactions.Sort(e => e.PayeeId, direction);
                    break;
                case "Clr":
                    Transactions.Sort(e => e.Clear, direction);
                    break;
                case "Credit":
                    Transactions.Sort(e => e.Credit, direction);
                    break;
                case "Debit":
                    Transactions.Sort(e => e.Debit, direction);
                    break;
                default:
                    Transactions.Sort(e => e.TransactionDate, direction);
                    break;
            }

            OnPropertyChanged("Transactions");
        }


        public void RefreshTotals()
        {
            OnPropertyChanged("AccountCredits");
            OnPropertyChanged("EndOfMonthCredits");
            OnPropertyChanged("FutureCredits");
            OnPropertyChanged("AccountDebits");
            OnPropertyChanged("EndOfMonthDebits");
            OnPropertyChanged("FutureDebits");
            OnPropertyChanged("AccountBalance");
            OnPropertyChanged("EndOfMonthBalance");
            OnPropertyChanged("FutureBalance");
        }


        public event ScrollToRecordEventHandler ScrollToRecord;

        protected void OnScrollToRecord(TransactionModel transaction)
        {
            ScrollToRecord?.Invoke(this, new ScrollToRecordEventArgs(transaction));
        }
    }
}
