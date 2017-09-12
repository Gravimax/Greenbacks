using Greenbacks.Common.Models;
using Greenbacks.Database;
using Greenbacks.RecordValidation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Greenbacks.Views
{
    /// <summary>
    /// Interaction logic for AddEditPayee.xaml
    /// </summary>
    public partial class AddEditPayee : Window, INotifyPropertyChanged
    {
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

        private List<PayeeTrackModel> _trackList;

        public List<PayeeTrackModel> TrackList
        {
            get { return _trackList; }
            set
            {
                _trackList = value;
                OnPropertyChanged("TrackList");
            }
        }

        private PayeeTrackModel _selectedTrack;

        public PayeeTrackModel SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                _selectedTrack = value;
                OnPropertyChanged("SelectedTrack");
            }
        }

        public bool trackingEnabled { get; set; }

        public AddEditPayee(PayeeModel payee)
        {
            InitializeComponent();

            SelectedPayee = payee;
            if (payee.Id > 0) { TrackList = new DALPayeeTracking().GetPayeeTrack(payee.Id); }
            trackingEnabled = payee.Id > 0;

            this.DataContext = this;
        }

        private void miAddTrack_Click(object sender, RoutedEventArgs e)
        {
            //PayeeTrackingItem newTrack = new PayeeTrackingItem { PayeeId = SelectedPayee.Id };
            //EditPayeeTrack ept = new EditPayeeTrack(newTrack);

            //ept.Owner = this;
            //ept.Closed += (s, ea) =>
            //{
            //    if (ept.DialogResult == true)
            //    {
            //        DAL_PayeeTracking tracking = new DAL_PayeeTracking();

            //        if (!tracking.CheckTrackExists(ept.TrackItem.AccountId, ept.TrackItem.PayeeId))
            //        {
            //            PayeeTrack track = new PayeeTrack
            //            {
            //                AccountId = ept.TrackItem.AccountId,
            //                Amount = ept.TrackItem.Amount,
            //                DueOn = ept.TrackItem.DueOn,
            //                Note = ept.TrackItem.Note,
            //                PayeeId = ept.TrackItem.PayeeId
            //            };


            //            tracking.AddPayeeTrack(track);
            //            newTrack.TrackId = track.Id;
            //            TrackList.Add(newTrack);
            //        }
            //        else
            //        {
            //            Utilities.ShowErrorMessageBox("This payee is already tracked for the selected account");
            //        }
            //    }
            //};
            //ept.ShowDialog();
        }

        private void miEditTrack_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTrack != null)
            {
                //EditPayeeTrack ept = new EditPayeeTrack(_selectedTrack);
                //ept.Owner = this;
                //ept.Closed += (s, ea) =>
                //{
                //    if (ept.DialogResult == true)
                //    {
                //        new DAL_PayeeTracking().UpdatePayeeTrack(_selectedTrack);
                //    }
                //};
                //ept.ShowDialog();
            }
        }

        private void miRemTrack_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTrack != null)
            {
                new DALPayeeTracking().DeletePayeeTrack(_selectedTrack);
                TrackList.Remove(_selectedTrack);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PayeeValidation vr = new PayeeValidation(SelectedPayee);
            vr.Validate();

            if (vr.HasErrors)
            {
                MessageBox.Show(vr.Message, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
