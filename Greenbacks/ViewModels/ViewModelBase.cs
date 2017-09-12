using System.ComponentModel;

namespace Greenbacks.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnMessageUpdated(string message, bool IsError)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Messages.EventMessage>(new Messages.EventMessage(message, IsError));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
