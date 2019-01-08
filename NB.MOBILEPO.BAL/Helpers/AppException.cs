using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Helpers
{
    /// <summary>
    /// Custom Exception
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Initializes new instance to AppException class
        /// </summary>
        public AppException()
        { }

        /// <summary>
        /// Initializes new instance to AppException class with message
        /// </summary>
        /// <param name="message"></param>
        public AppException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance to AppException class with message and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AppException(Exception innerException, string message = "Server exception caused.")
            : base(message, innerException)
        { }
    }
}
