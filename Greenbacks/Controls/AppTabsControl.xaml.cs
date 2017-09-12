using Greenbacks.Messages;
using System.Windows;
using System.Windows.Controls;

namespace Greenbacks.Controls
{
    /// <summary>
    /// Interaction logic for AppTabsControl.xaml
    /// </summary>
    public partial class AppTabsControl : UserControl
    {
        public AppTabsControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.AddHandler(CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Messages.AddTabMessage>(this, (message) =>
            {
                AddTab(message);
            });
        }


        public void AddTab(AddTabMessage e)
        {
            if (e.IsClosable)
            {
                CloseableTabItem tabItem = new CloseableTabItem();
                tabItem.Content = e.UserControl;
                tabItem.Header = e.Header;
                tabControl.Items.Add(tabItem);
                tabControl.SelectedIndex = tabControl.Items.Count - 1;
            }
            else
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = e.UserControl;
                tabItem.Header = e.Header;
                tabControl.Items.Add(tabItem);
            }
        }


        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = args.OriginalSource as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                {
                    tabControl.Items.Remove(tabItem);
                    OnRemoveTab();
                }
            }
        }


        protected void OnRemoveTab()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<UpdateAccountSummariesMessage>(new UpdateAccountSummariesMessage());
        }
    }
}
