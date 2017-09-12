using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Greenbacks
{
    public class AddTabEventArgs : EventArgs
    {
        public AddTabEventArgs(UserControl userControl, string name, bool isClosable = true)
        {
            this.Control = userControl;
            this.Name = name;
            this.IsClosable = isClosable;
        }

        public readonly UserControl Control;
        public readonly string Name;
        public readonly bool IsClosable;
    }
}
