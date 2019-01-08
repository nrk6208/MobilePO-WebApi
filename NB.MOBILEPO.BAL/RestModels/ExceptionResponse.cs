using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class ExceptionResponse
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Exception InnerException { get; set; }
    }
}
