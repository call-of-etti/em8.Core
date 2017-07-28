using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CoE.em8.Core.Numeric
{
    [Serializable]
    public class NotSignedIntegerException : NotIntegerException
    {
        public NotSignedIntegerException()
        {
        }

        public NotSignedIntegerException(string message) : base(message)
        {
        }

        public NotSignedIntegerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSignedIntegerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
