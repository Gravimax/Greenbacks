using Greenbacks.Common.Models;
using Greenbacks.Controls;
using Greenbacks.Database;
using Greenbacks.DatabaseAccess;
using Greenbacks.Helpers;
using Greenbacks.Messages;
using Greenbacks.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Greenbacks.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Properties


        private AccountModel _selectedAccount;
        public AccountModel SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }


        private ObservableCollection<AccountModel> _accountSummaries;
        public ObservableCollection<AccountModel> AccountSummaries
        {
            get { return _accountSummaries; }
            set
            {
                _accountSummaries = value;
                OnPropertyChanged("AccountSummaries");
            }
        }


        private ObservableCollection<PayeeTrackModel> _trackingSummary;

        public ObservableCollection<PayeeTrackModel> TrackingSummary
        {
            get { return _trackingSummary; }
            set
            {
                _trackingSummary = value;
                OnPropertyChanged("TrackingSummary");
            }
        }


        private string _totalIncome;

        public string TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                _totalIncome = value;
                OnPropertyChanged("TotalIncome");
            }
        }


        private string _totalExpenses;

        public string TotalExpenses
        {
            get { return _totalExpenses; }
            set
            {
                _totalExpenses = value;
                OnPropertyChanged("TotalExpenses");
            }
        }


        private string _totalBalance;

        public string TotalBalance
        {
            get { return _totalBalance; }
            set
            {
                _totalBalance = value;
                OnPropertyChanged("TotalBalance");
            }
        }

        
        public DelegateCommand DoubleClickItemCommand { get; private set; }

        public DelegateCommand EditAccountCommand { get; private set; }


        #endregion


        public DashboardViewModel()
        {
            DoubleClickItemCommand = new DelegateCommand(AccountDoubleClick);
            EditAccountCommand = new DelegateCommand(EditAccount);

            SetMessaging();

            try
            {
                AccountSummaries = new DALAccounts().GetAccountSummaries().ToObservableCollection();
                GetTotals();
                TrackingSummary = new DAL_PayeeTracking().GetPayeeTracks().ToObservableCollection();
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private void SetMessaging()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.UpdateAccountSummariesMessage>(this, (message) =>
            {
                AccountSummaries = new DALAccounts().GetAccountSummaries().ToObservableCollection();
                GetTotals();
            });

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<EditAccountMessage>(this, (message) =>
            //{
            //    EditAccount(message.Args);
            //});

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<DeleteAccountMessage>(this, (message) =>
            {
                DeleteSelectedAccount(message.Args);
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<TransferFundsMessage>(this, (message) =>
            {
                TransferFunds(message.Args);
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ImportStatamentMessage>(this, (message) =>
            {
                ImportBankStatement(null);
            });
        }


        private void GetTotals()
        {
            decimal income = AccountSummaries.Where(y => y.Asset == true).Sum(x => x.Income);
            decimal expenses = AccountSummaries.Where(y => y.Asset == true).Sum(x => x.Expenses);
            TotalIncome = string.Format("{0:C}", income);
            TotalExpenses = string.Format("{0:C}", expenses);
            TotalBalance = string.Format("{0:C}", income - expenses);
        }


        private bool CanEditAccount()
        {
            return SelectedAccount != null;
        }


        private void EditAccount(object args)
        {
            try
            {
                if (CanEditAccount())
                {
                    var dal_Accounts = new DALAccounts();
                    var dal_AccountTypes = new DALAccountTypes();
                    EditAccount view = new EditAccount(dal_Accounts.GetAccount(SelectedAccount.Id), dal_AccountTypes.GetAccountTypes());

                    System.Windows.Window owner = args as System.Windows.Window;
                    if (owner != null)
                    {
                        view.Owner = owner;
                    }

                    view.ShowDialog();

                    if (view.DialogResult == true)
                    {
                        dal_Accounts.UpdateAccount(view.Account);
                        AccountSummaries = dal_Accounts.GetAccountSummaries().ToObservableCollection();
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private bool CanDeleteAccount()
        {
            return AccountSummaries.Count > 0;
        }


        private void DeleteSelectedAccount(object args)
        {
            try
            {
                if (CanDeleteAccount())
                {
                    AccountDelete view = new AccountDelete(AccountSummaries.ToList());

                    System.Windows.Window owner = args as System.Windows.Window;
                    if (owner != null)
                    {
                        view.Owner = owner;
                    }

                    view.ShowDialog();

                    if (view.DialogResult == true)
                    {
                        if (view.BackupAccount)
                        {
                            if (!AccountExport(view.Result.Id))
                            {
                                OnMessageUpdated("Aborted Delete Account!", false);
                                return;
                            }
                        }

                        var dal_Accounts = new DALAccounts();
                        AccountModel account = dal_Accounts.GetAccount(view.Result.Id);
                        dal_Accounts.DeleteAccount(account);
                        AccountSummaries = dal_Accounts.GetAccountSummaries().ToObservableCollection();
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private bool CanTransferFunds()
        {
            return SelectedAccount != null;
        }


        private void TransferFunds(object args)
        {
            try
            {
                if (CanTransferFunds())
                {
                    TransferFunds view = new TransferFunds(AccountSummaries.ToList());

                    System.Windows.Window owner = args as System.Windows.Window;
                    if (owner != null)
                    {
                        view.Owner = owner;
                    }

                    view.ShowDialog();

                    if (view.DialogResult == true)
                    {
                        var dal_Accounts = new DALAccounts();
                        var payees = new DALPayees();
                        var transactions = new DALTransactions();

                        AccountModel fromAccount = dal_Accounts.GetAccount(view.Result.FromAccountId);
                        AccountModel toAccount = dal_Accounts.GetAccount(view.Result.ToAccountId);
                        int catId = new DAL_Categories().GetCategory(4, "Funds").Id;

                        // ---- From transaction ----
                        TransactionModel from = new TransactionModel();
                        from.TransactionDate = DateTime.Now;
                        from.AccountId = view.Result.FromAccountId;
                        from.Credit = 0;
                        from.Debit = view.Result.TransferAmount;
                        from.Memo = view.Result.Memo;
                        from.CategoryId = catId;

                        PayeeModel fromPayee = payees.GetPayee("(" + toAccount.BankName + ") " + toAccount.Name);
                        if (fromPayee == null)
                        {
                            fromPayee = new PayeeModel { Name = "(" + toAccount.BankName + ") " + toAccount.Name };
                            payees.CreatePayee(fromPayee);
                        }

                        from.PayeeId = fromPayee.Id;
                        transactions.CreateTransaction(from);

                        // ---- To transaction ----
                        TransactionModel to = new TransactionModel();
                        to.TransactionDate = DateTime.Now;
                        to.AccountId = view.Result.ToAccountId;
                        to.Credit = view.Result.TransferAmount;
                        to.Debit = 0;
                        to.Memo = view.Result.Memo;
                        to.CategoryId = catId;

                        PayeeModel toPayee = payees.GetPayee("(" + fromAccount.BankName + ") " + fromAccount.Name);
                        if (toPayee == null)
                        {
                            toPayee = new PayeeModel { Name = "(" + fromAccount.BankName + ") " + fromAccount.Name };
                            payees.CreatePayee(toPayee);
                        }

                        to.PayeeId = toPayee.Id;
                        transactions.CreateTransaction(to);

                        OnPropertyChanged("AccountsSummary");

                        OnMessageUpdated("Amount Transfered", false);
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }

        private bool CanExportAccount()
        {
            return SelectedAccount != null;
        }


        private void ExportAccount(object args)
        {
            try
            {
                if (CanExportAccount())
                {
                    if (Utilities.ShowDialogBox("Export the selected account and transactions?"))
                    {
                        AccountExport(SelectedAccount.Id);
                    }
                }

            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
            }
        }


        private void ImportBankStatement(object args)
        {
            OnAddTab(new ImportStatementControl(), "Import Statement", true);
        }


        private bool AccountExport(int accountId)
        {
            //DAL_Accounts dal_Accounts = new DAL_Accounts();
            //DAL_Transactions dal_Trans = new DAL_Transactions();
            //AccountItem item = new AccountItem();

            //item.Account = dal_Accounts.GetAccount(accountId);
            //item.Transactions = dal_Trans.GetAccountTransactions(accountId).ToList();

            //item.Account.Id = 0;

            //string file = Utilities.SaveFile(item.Account.Name);
            //if (!string.IsNullOrEmpty(file))
            //{
            //    XMLFileIO save = new XMLFileIO();
            //    save.Save<AccountItem>(file, item);
            //    OnMessageUpdated(this, new MessageUpdatedEventArgs("Account Exported"));
            //    return true;
            //}

            return false;
        }


        private void AccountDoubleClick(object args)
        {
            if (SelectedAccount != null)
            {
                OnAddTab(new TransactionList(SelectedAccount.Id), "(" + SelectedAccount.BankName + ") " + SelectedAccount.Name, true);
            }
        }


        protected void OnAddTab(UserControl userControl, string header, bool isClosable)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.AddTabMessage>(new Messages.AddTabMessage(userControl, header, isClosable));
        }
    }
}
