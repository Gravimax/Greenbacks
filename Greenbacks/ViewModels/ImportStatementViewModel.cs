using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Helpers;
using Greenbacks.ImportData.Models;
using Greenbacks.Models;
using Greenbacks.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Greenbacks.ViewModels
{
    public class ImportStatementViewModel : ViewModelBase
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
                ImportQifFileCommand.RaiseCanExecuteChanged();
            }
        }


        private List<AccountModel> _accountList;

        public List<AccountModel> AccountList
        {
            get { return _accountList; }
            set
            {
                _accountList = value;
                OnPropertyChanged("AccountList");
            }
        }


        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged("FilePath");
                ImportQifFileCommand.RaiseCanExecuteChanged();
            }
        }


        private string _importMessage;

        public string ImportMessage
        {
            get { return _importMessage; }
            set
            {
                _importMessage = value;
                OnPropertyChanged("ImportMessage");
            }
        }


        private ObservableCollection<ImportTransTemp> _unfiledTransactions;

        public ObservableCollection<ImportTransTemp> UnfiledTransactions
        {
            get { return _unfiledTransactions; }
            set
            {
                _unfiledTransactions = value;
                OnPropertyChanged("UnfiledTransactions");
            }
        }


        private ObservableCollection<PayeeModel> _payeeList;
        public ObservableCollection<PayeeModel> PayeeList
        {
            get { return _payeeList; }
            set
            {
                _payeeList = value;
                OnPropertyChanged("PayeeList");
            }
        }


        private ObservableCollection<GenericComboItem> _categoryList;
        public ObservableCollection<GenericComboItem> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                OnPropertyChanged("CategoryList");
            }
        }


        private ImportData.Models.ImportModel _model = null;

        public ImportModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }


        public DelegateCommand SelectImportFileCommand { get; private set; }
        public DelegateCommand ImportQifFileCommand { get; private set; }
        public DelegateCommand ImportMatchedCommand { get; private set; }
        public DelegateCommand AddNewPayeeCommand { get; private set; }


        #endregion


        #region Ctors


        public ImportStatementViewModel()
        {
            SelectImportFileCommand = new DelegateCommand(SelectImportFile);
            ImportQifFileCommand = new DelegateCommand(ImportQifFile, CanImportQifFile);
            ImportMatchedCommand = new DelegateCommand(ImportMatched);
            AddNewPayeeCommand = new DelegateCommand(AddNewPayee);

            AccountList = new DALAccounts().GetAccountSummaries().ToList();
            PayeeList = new ObservableCollection<PayeeModel>();
            PayeeList.Add(new PayeeModel { Id = -1, Name = "---- Auto Create New Payee ----" });
            PayeeList.AddRange(new DALPayees().GetPayees().OrderBy(x => x.Name).ToObservableCollection());

            CategoryList = new ObservableCollection<GenericComboItem>();
            CategoryList.Add(new GenericComboItem { ID = -1, Value = "---- Auto Assign ----" });
            CategoryList.AddRange(new DALCategories().GetCategoriesList());
        }


        #endregion


        private void AddNewPayee(object args)
        {
            try
            {
                PayeeModel newPayee = new PayeeModel();
                AddEditPayee addEdit = new AddEditPayee(newPayee);
                addEdit.Closed += (sender, e) =>
                {
                    if (addEdit.DialogResult == true)
                    {
                        try
                        {
                            new DALPayees().CreatePayee(addEdit.SelectedPayee);
                            PayeeList.Add(addEdit.SelectedPayee);
                        }
                        catch (Exception ex)
                        {
                            OnMessageUpdated(ex.Message, true);
                        }
                    }
                };

                System.Windows.Window owner = args as System.Windows.Window;
                if (owner != null)
                {
                    addEdit.Owner = owner;
                }

                addEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private void SelectImportFile(object args)
        {
            FilePath = FileUtilities.SelectFile("Select a file to import", "Quicken|*.qfx|All files|*.*", null);

            if (!string.IsNullOrEmpty(FilePath))
            {
                ImportData.Quicken.ImportQuicken quicken = new ImportData.Quicken.ImportQuicken();
                Model = quicken.ProcessImport(FileUtilities.LoadFile(_filePath));

                if (SelectedAccount == null)
                {
                    SelectedAccount = AccountList.FirstOrDefault(x => x.AccountNumber == Security.GetMd5Hash(Model.AccountInfo.AccountId));
                }

                ImportMessage = string.Empty;
            }
        }


        private bool CanImportQifFile(object args)
        {
            return SelectedAccount != null && !string.IsNullOrEmpty(_filePath);
        }


        private void ImportQifFile(object args)
        {
            try
            {
                List<ImportTransTemp> foundTrans = new List<ImportTransTemp>();
                List<ImportTransTemp> newTrans = new List<ImportTransTemp>();
                List<BankTransaction> noPayeeFound = new List<BankTransaction>();

                DateTime startDate = Model.DateStart.Value.AddDays(-7); // Go back 7 days? More?
                List<TransactionModel> transactions = new DALTransactions().GetTransactionsByDate(startDate, Model.DateEnd.Value);

                // Try to match payees directly first. Then by date and amount.
                List<PayeeModel> payees = new DALPayees().GetPayees();

                BankTransaction[] btTemp = Model.TransactionList.ToArray();
                List<ImportTransTemp> temp = new List<ImportTransTemp>();
                foreach (var payee in payees)
                {
                    var tTemp = btTemp.Where(x => x.Name.Contains(payee.Name.ToUpper())).ToList(); // This will prob create dupes....need to check later.

                    foreach (var item in tTemp)
                    {
                        temp.Add(new ImportTransTemp { BankTrans = item, PayeeId = payee.Id, PayeeName = payee.Name });
                        if (Model.TransactionList.FirstOrDefault(x => x.TransactionId == item.TransactionId) != null)
                        {
                            Model.TransactionList.Remove(item);
                        }
                    }
                }


                // if payee is found, but not amount, it should be new.
                // if amount is found on payee, but not date, it's existing.
                foreach (var trans in temp)
                {
                    bool isDebit = trans.BankTrans.TransactionAmount < 0;
                    TransactionModel tm;

                    if (isDebit)
                    {
                        tm = transactions.FirstOrDefault(x => x.PayeeId == trans.PayeeId && x.Debit == -(trans.BankTrans.TransactionAmount)  && x.Clear == false && x.TransactionDate >= trans.BankTrans.DatePosted.AddDays(-7));
                    }
                    else
                    {
                        tm = transactions.FirstOrDefault(x => x.PayeeId == trans.PayeeId && x.Credit == trans.BankTrans.TransactionAmount && x.Clear == false && x.TransactionDate >= trans.BankTrans.DatePosted.AddDays(-7));
                    }

                    if (tm != null) // it exists. TransModel null if new.
                    {
                        trans.TransModel = tm;
                        foundTrans.Add(trans);
                    }
                    else
                    {
                        newTrans.Add(trans);
                    }
                }


                // For ones that we cant match, use the StatementPayee list
                List<StatementPayeeModel> stPayees = new DALStatementPayees().GetPayeeList();
                stPayees = stPayees.OrderByDescending(x => x.StatementName.Length).ToList(); // Sorting by length should help to prevent dupes. large to small.

                BankTransaction[] dtTemp = Model.TransactionList.ToArray();
                temp = new List<ImportTransTemp>();
                foreach (var payee in stPayees)
                {
                    var tTemp = dtTemp.Where(x => x.Name.Contains(payee.StatementName.ToUpper())).ToList(); // This will prob create dupes, for example: Wells Fargo, Wells Fargo Visa.

                    foreach (var item in tTemp)
                    {
                        if (temp.FirstOrDefault(x => x.BankTrans.TransactionId == item.TransactionId) == null) // Check for a dupe first.
                        {
                            temp.Add(new ImportTransTemp { BankTrans = item, PayeeId = payee.PayeeId, PayeeName = payee.StatementName });
                            if (Model.TransactionList.FirstOrDefault(x => x.TransactionId == item.TransactionId) != null)
                            {
                                Model.TransactionList.Remove(item);
                            }
                        }
                    }
                }


                // if payee is found, but not amount, it should be new.
                // if amount is found on payee, but not date, it's existing.
                foreach (var trans in temp)
                {
                    TransactionModel tm;

                    if (trans.BankTrans.TransactionAmount < 0)
                    {
                        tm = transactions.FirstOrDefault(x => x.PayeeId == trans.PayeeId && x.Debit == -(trans.BankTrans.TransactionAmount) && x.Clear == false && x.TransactionDate >= trans.BankTrans.DatePosted.AddDays(-7));
                    }
                    else
                    {
                        tm = transactions.FirstOrDefault(x => x.PayeeId == trans.PayeeId && x.Credit == trans.BankTrans.TransactionAmount && x.Clear == false && x.TransactionDate >= trans.BankTrans.DatePosted.AddDays(-7));
                    }

                    if (tm != null) // it exists. TransModel null if new.
                    {
                        trans.TransModel = tm;
                        foundTrans.Add(trans);
                    }
                    else
                    {
                        newTrans.Add(trans);
                    }
                }


                DALTransactions dalTrans = new DALTransactions();

                // Update found transaction. Set to cleared.
                foreach (var item in foundTrans)
                {
                    if (item.TransModel.Clear == false) // It hasent been added yet.
                    {
                        item.TransModel.Clear = true;
                        dalTrans.UpdateTransaction(item.TransModel);
                    }
                }

                // Add new transactions. Set to cleared.
                foreach (var item in newTrans)
                {
                    var transModel = new TransactionModel();
                    transModel.AccountId = SelectedAccount.Id;

                    transModel.CategoryId = dalTrans.GetTransactionCategory(item.PayeeId); // Get the first payee category in the trans database.
                    if (transModel.CategoryId <= 0) // It didn't find anyting so use default category.
                    {
                        transModel.CategoryId = new DALCategories().GetCategory("Category").Id;
                    }

                    transModel.Clear = true;
                    if (item.BankTrans.TransactionAmount < 0)
                    {
                        transModel.Debit = -(item.BankTrans.TransactionAmount);
                    }
                    else
                    {
                        transModel.Credit = item.BankTrans.TransactionAmount;
                    }
                    transModel.Memo = item.BankTrans.Memo;
                    transModel.PayeeId = item.PayeeId;
                    transModel.TransactionDate = item.BankTrans.DatePosted;

                    dalTrans.CreateTransaction(transModel);
                }


                // For any that still dont match, display and ask user to match manually
                // If a payee does not exist, allow the user to create a new one or auto create from bank statement.

                UnfiledTransactions = new ObservableCollection<ImportTransTemp>();
                foreach (var item in Model.TransactionList)
                {
                    UnfiledTransactions.Add(new ImportTransTemp { BankTrans = item });
                }

                if (UnfiledTransactions.Count > 0)
                {
                    ImportMessage = "The following transactions could not be imported. Please match/create the payees and categories to import.";
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }


        private void ImportMatched(object args)
        {
            try
            {
                DALTransactions dalTrans = new DALTransactions();
                DALPayees dalPayees = new DALPayees();

                // For each trans that the user did not matche or created a new payee for, skip and remove the rest that have been added.
                var temp = UnfiledTransactions.Where(x => x.PayeeId != 0).ToList();

                foreach (var item in temp)
                {
                    if (item.PayeeId == -1) // Create new payee from statment name
                    {
                        PayeeModel payee = new PayeeModel { Name = item.BankTrans.Name };
                        dalPayees.CreatePayee(payee);
                        item.PayeeId = payee.Id;
                    }

                    var transModel = new TransactionModel();
                    transModel.AccountId = SelectedAccount.Id;
                    if (item.CategoryId <= 0)
                    {
                        transModel.CategoryId = dalTrans.GetTransactionCategory(item.PayeeId); // Get the first payee category in the trans database.
                        if (transModel.CategoryId <= 0) // It didn't find anyting so use default category.
                        {
                            transModel.CategoryId = new DALCategories().GetCategory("Category").Id;
                        }
                    }
                    else
                    {
                        transModel.CategoryId = item.CategoryId;
                    }

                    transModel.Clear = true;
                    if (item.BankTrans.TransactionAmount < 0)
                    {
                        transModel.Debit = -(item.BankTrans.TransactionAmount);
                    }
                    else
                    {
                        transModel.Credit = item.BankTrans.TransactionAmount;
                    }
                    transModel.Memo = item.BankTrans.Memo;
                    transModel.PayeeId = item.PayeeId;
                    transModel.TransactionDate = item.BankTrans.DatePosted;

                    dalTrans.CreateTransaction(transModel);

                    UnfiledTransactions.Remove(item); // Were done with it so remove from list of unfiled transactions.
                }
            }
            catch (Exception ex)
            {
                OnMessageUpdated(ex.Message, true);
                ExceptionLogger.AppendExceptionLog(ex);
            }
        }
    }
}
