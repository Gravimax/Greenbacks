using Greenbacks.Common.Models;
using Greenbacks.Controls;
using Greenbacks.Database;
using Greenbacks.Helpers;
using Greenbacks.Messages;
using Greenbacks.View;
using Greenbacks.Views;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Greenbacks.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        #region Commands


        public DelegateCommand EditPayeesCommand { get; private set; }
        public DelegateCommand EditCategoriesCommand { get; private set; }
        public DelegateCommand EditConfigurationCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand NewAccountCommand { get; private set; }

        public DelegateCommand DeleteAccountCommand { get; private set; }
        public DelegateCommand TransferFundsCommand { get; private set; }

        public DelegateCommand ManageCategoriesCommand { get; private set; }
        public DelegateCommand ManagePayeesCommand { get; private set; }
        public DelegateCommand ExportAccountCommand { get; private set; }
        public DelegateCommand ImportAccountCommand { get; private set; }
        public DelegateCommand ReportsCommand { get; private set; }
        public DelegateCommand BackupCommand { get; private set; }
        public DelegateCommand RestoreCommand { get; private set; }

        public DelegateCommand ImportBankStatementCommand { get; private set; }


        #endregion


        #region Ctors

        public ApplicationViewModel()
        {
            SetApplication();
        }


        private void SetApplication()
        {
            EditConfigurationCommand = new DelegateCommand(EditConfiguration, CanEditConfiguration);
            ExitCommand = new DelegateCommand(Exit, CanExitApplication);
            NewAccountCommand = new DelegateCommand(CreateAccount, CanCreateAccount);

            DeleteAccountCommand = new DelegateCommand(DeleteSelectedAccount);
            TransferFundsCommand = new DelegateCommand(TransferFunds);
            ManageCategoriesCommand = new DelegateCommand(ManageCategories);
            ManagePayeesCommand = new DelegateCommand(ManagePayees);

            ExportAccountCommand = new DelegateCommand(ExportAccount);
            ImportAccountCommand = new DelegateCommand(ImportAccount);
            ReportsCommand = new DelegateCommand(ShowReports);
            BackupCommand = new DelegateCommand(BackupDatabase);
            RestoreCommand = new DelegateCommand(RestoreDatabase);
            ImportBankStatementCommand = new DelegateCommand(ImportBankStatement);

            OnAddTab(new Greenbacks.Controls.Dashboard(), "Dashboard", false);
        }


        #endregion


        #region Command Methods


        private bool CanEditConfiguration(object args)
        {
            return ApplicationConfiguration.GreenbacksConfig != null;
        }


        private void EditConfiguration(object args)
        {
            try
            {
                EditConfiguration view = new EditConfiguration(ApplicationConfiguration.GreenbacksConfig);

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    view.Owner = owner;
                }

                view.ShowDialog();
                if (view.DialogResult == true)
                {
                    ApplicationConfiguration.Save(ApplicationConfiguration.GreenbacksConfig);
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private bool CanExitApplication(object args)
        {
            return true;
        }


        private void Exit(object args)
        {
            OnExitApplication();
        }


        private bool CanCreateAccount(object args)
        {
            return true;
        }


        private void CreateAccount(object args)
        {
            try
            {
                var dal_Accounts = new DALAccounts();
                var dal_AccountTypes = new DALAccountTypes();
                NewAccount view = new NewAccount(dal_AccountTypes.GetAccountTypes());

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    view.Owner = owner;
                }

                view.ShowDialog();

                if (view.DialogResult == true)
                {
                    AccountModel account = new AccountModel
                    {
                        Name = view.NewSelectedAccount.AccountName,
                        AccountTypeId = view.NewSelectedAccount.AccountTypeId,
                        BankName = view.NewSelectedAccount.BankName,
                        Notes = view.NewSelectedAccount.Notes
                    };

                    dal_Accounts.CreateAccount(account);

                    TransactionModel newTransaction = new TransactionModel
                    {
                        AccountId = account.Id,
                        Credit = 0.00M,
                        Debit = 0.00M,
                        TransactionDate = DateTime.Now,
                        Memo = "Starting Balance",
                        PayeeId = 1,
                        CategoryId = 2
                    };

                    AccountTypeModel type = dal_AccountTypes.GetAccountTypes().Single(x => x.Id == account.AccountTypeId);
                    switch (type.Asset)
                    {
                        case true:
                            newTransaction.Credit = view.NewSelectedAccount.StartingBalance;
                            break;
                        case false:
                            newTransaction.Debit = view.NewSelectedAccount.StartingBalance;
                            break;
                    }

                    DALCategories cats = new DALCategories();
                    var cat = cats.GetCategory("Starting Balance");

                    DALPayees payees = new DALPayees();
                    var payee = payees.GetPayee("New Account");

                    newTransaction.CategoryId = cat.Id;
                    newTransaction.PayeeId = payee.Id;

                    DALTransactions trans = new DALTransactions();
                    trans.CreateTransaction(newTransaction);

                    OnMessageUpdated("New account added: " + "(" + account.BankName + ") " + account.Name, false);
                    OnSummariesUpdated();
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        //private void EditAccount(object args)
        //{
        //    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<EditAccountMessage>(new EditAccountMessage(args));
        //}


        private void DeleteSelectedAccount(object args)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<DeleteAccountMessage>(new DeleteAccountMessage(args));
        }


        private void TransferFunds(object args)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<TransferFundsMessage>(new TransferFundsMessage(args));
        }


        private void ManageCategories(object args)
        {
            try
            {
                CategoryManagement view = new CategoryManagement();
                OnAddTab(view, "Category Management", true);
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private void ManagePayees(object args)
        {
            try
            {
                PayeeManagement view = new PayeeManagement(new DALAccounts().GetAccountSummaries().ToList());
                OnAddTab(view, "Payee Management", true);
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        #endregion


        #region Import/Export


        private void ExportAccount(object args)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ExportAccountMessage>(new ExportAccountMessage(args));
        }


        private void ImportBankStatement(object args)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ImportStatamentMessage>(new ImportStatamentMessage());
        }


        private void ImportAccount(object args)
        {
            try
            {
                // ToDo: Need some way of gracefully backing out of import if there is any serious error.
                //using (GreenbacksEntities context = new GreenbacksEntities())
                //{
                //    DAL_Accounts dal_Accounts = new DAL_Accounts();
                //    DAL_Transactions dal_Trans = new DAL_Transactions();
                //    AccountItem item;

                //    string file = Utilities.OpenFile();
                //    if (!string.IsNullOrEmpty(file))
                //    {
                //        if (MessageBox.Show("Import the selected account and transactions?", "Import?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                //        {
                //            List<Category> categories = context.Categories.ToList();

                //            item = new XMLFileIO().Load<AccountItem>(file);

                //            dal_Accounts.AddAccount(item.Account);

                //            // Add back in each transaction, checking if the previous category is still valid.
                //            // If not set to defalt category.
                //            foreach (var trans in item.Transactions)
                //            {
                //                trans.AccountId = item.Account.Id;
                //                if (categories.SingleOrDefault(x => x.Id == trans.CategoryId) == null)
                //                {
                //                    trans.CategoryId = 1;
                //                }
                //            }

                //            dal_Trans.AddTransactions(item.Transactions);
                //            OnMessageUpdated(this, new MessageUpdatedEventArgs("Account Imported"));
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
            }
        }


        #endregion


        #region Reports


        private void ShowReports(object args)
        {
            ReportsControl control = new ReportsControl();
            OnAddTab(control, "Reports", true);
        }

        #endregion

        #region Backup/Restore


        public void BackupDatabase(object args)
        {
            OnMessageUpdated("Not currently implemented.", true);
        }


        public void RestoreDatabase(object args)
        {
            OnMessageUpdated("Not currently implemented.", true);
        }


        #endregion


        #region Events


        protected void OnSummariesUpdated()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<UpdateAccountSummariesMessage>(new UpdateAccountSummariesMessage());
        }


        protected void OnAddTab(UserControl userControl, string header, bool isClosable)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.AddTabMessage>(new Messages.AddTabMessage(userControl, header, isClosable));
        }


        protected void OnExitApplication()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ExitAppMessage>(new ExitAppMessage());
        }


        #endregion
    }
}
