﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks.Messages
{
    public class DeleteAccountMessage
    {
        public DeleteAccountMessage(object args)
        {
            this.Args = args;
        }

        public object Args;
    }
}