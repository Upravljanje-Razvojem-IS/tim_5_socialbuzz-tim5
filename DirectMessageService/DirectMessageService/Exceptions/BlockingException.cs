﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Exceptions
{
    [Serializable]
    public class BlockingException : Exception
    {
        public BlockingException(string message)
              : base(message)
        {

        }
        public BlockingException(string message, Exception inner)
               : base(message, inner)
        {

        }
    }
}
