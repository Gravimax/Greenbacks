using System.Windows.Controls;

namespace Greenbacks.Messages
{
    public class AddTabMessage
    {
        public AddTabMessage(UserControl userControl, string header, bool isClosable)
        {
            this.UserControl = userControl;
            this.Header = header;
            this.IsClosable = isClosable;
        }

        public UserControl UserControl;
        public string Header;
        public bool IsClosable;
    }
}
