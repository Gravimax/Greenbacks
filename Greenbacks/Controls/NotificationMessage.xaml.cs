using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for NotificationMessage.xaml
    /// </summary>
    public partial class NotificationMessage : UserControl
    {
        public NotificationMessage()
        {
            InitializeComponent();

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.EventMessage>(this, (message) =>
            {
                txtStatusMessage.Content = message.Message;
                if (!message.IsError)
                {
                    notificationMessage.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    notificationMessage.Background = new SolidColorBrush(Colors.Red);
                }

                ((Storyboard)this.Resources["OpenMessageAnimation"]).Begin(this);
            });
        }

        //public void OpenStatusMessage(object sender, MessageUpdatedEventArgs e)
        //{
        //    txtStatusMessage.Content = e.Message;
        //    if (!e.IsError)
        //    {
        //        notificationMessage.Background = new SolidColorBrush(Colors.Green);
        //    }
        //    else
        //    {
        //        notificationMessage.Background = new SolidColorBrush(Colors.Red);
        //    }

        //    ((Storyboard)this.Resources["OpenMessageAnimation"]).Begin(this);
        //}

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            ((Storyboard)this.Resources["CloseMessageAnimation"]).Begin(this);
        }
    }
}
