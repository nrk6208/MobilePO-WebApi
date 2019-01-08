using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NB.MOBILEPO.BAL.Helpers;
using NB.MOBILEPO.BAL.RestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.MOBILEPO.WEBAPI.Extensions
{
    /// <summary>
    /// Class to handle exceptions globally
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes new instance to GlobalExceptionFilter class
        /// </summary>
        /// <param name="logger"></param>
        public GlobalExceptionFilter(ILoggerFactory logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this._logger = logger.CreateLogger("Global Exception Filter");
        }

        /// <summary>
        /// Exception caught
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is AppException)
            { }
            this._logger.LogError(context.Exception,$" An exception Caught, {exception.Message}");

            var response = new ExceptionResponse()
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException
            };
            
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500,
                DeclaredType = typeof(ExceptionResponse)
            };
        }
    }
}
