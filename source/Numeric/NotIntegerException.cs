using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CoE.em8.Core.Numeric
{
    [Serializable]
    public class NotIntegerException : ApplicationException
    {
        public NotIntegerException()
        {
        }

        public NotIntegerException(string message) : base(message)
        {
        }

        public NotIntegerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotIntegerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
