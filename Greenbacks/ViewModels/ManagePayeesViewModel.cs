using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.Helpers;
using Greenbacks.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Greenbacks.ViewModels
{
    public class ManagePayeesViewModel : ViewModelBase
    {
        private DALPayees dal_Payees = new DALPayees();

        private List<AccountModel> accountSummaries;

        private ObservableCollection<PayeeModel> _payees;

        public ObservableCollection<PayeeModel> Payees
        {
            get { return _payees; }
            set 
            {
                _payees = value;
                OnPropertyChanged("Payees");
            }
        }

        private PayeeModel _selectedPayee;

        public PayeeModel SelectedPayee
        {
            get { return _selectedPayee; }
            set 
            {
                _selectedPayee = value;
                OnPropertyChanged("SelectedPayee");
            }
        }

        public DelegateCommand NewPayeeCommand { get; private set; }
        public DelegateCommand EditPayeeCommand { get; private set; }
        public DelegateCommand DeletePayeeCommand { get; private set; }
        public DelegateCommand DoubleClickItemCommand { get; private set; }

        public ManagePayeesViewModel(List<AccountModel> accountSummaries)
        {
            Payees = dal_Payees.GetPayees().OrderBy(x => x.Name).ToObservableCollection();
            this.accountSummaries = accountSummaries;

            NewPayeeCommand = new DelegateCommand(CreatePayee);
            EditPayeeCommand = new DelegateCommand(EditPayee, CanEditPayee);
            DeletePayeeCommand = new DelegateCommand(DeletePayee, CanDeleteCategory);
            DoubleClickItemCommand = new DelegateCommand(PayeeDoubleClick);
        }

        public void CreatePayee(object args)
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
                            dal_Payees.CreatePayee(addEdit.SelectedPayee);
                            Payees.Add(addEdit.SelectedPayee);
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

        private void PayeeDoubleClick(object args)
        {
            if (CanEditPayee(null))
            {
                EditPayee(args);
            }
        }

        private bool CanEditPayee(object args)
        {
            return SelectedPayee != null;
        }

        public void EditPayee(object args)
        {
            try
            {
                AddEditPayee addEdit = new AddEditPayee(SelectedPayee);
                addEdit.Closed += (sender, e) =>
                {
                    if (addEdit.DialogResult == true)
                    {
                        try
                        {
                            dal_Payees.UpdatePayee(SelectedPayee);
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

        private bool CanDeleteCategory(object args)
        {
            return SelectedPayee != null;
        }

        public void DeletePayee(object args)
        {
            try
            {
                if (!dal_Payees.IsPayeeInUse(SelectedPayee.Id))
                {
                    if (Utilities.ShowDialogBox("Delete Payee?") == true)
                    {
                        try
                        {
                            dal_Payees.DeletePayee(SelectedPayee);
                            Payees.Remove(SelectedPayee);
                            SelectedPayee = null;
                            OnMessageUpdated("Payee Deleted", false);

                        }
                        catch (Exception ex)
                        {
                            OnMessageUpdated(ex.Message, true);
                        }
                    }
                }
                else
                {
                    OnMessageUpdated("Cannot Delete Payee. Payee is currently in use.", true);
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
