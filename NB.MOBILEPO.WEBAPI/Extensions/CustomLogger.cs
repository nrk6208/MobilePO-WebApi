using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.MOBILEPO.WEBAPI.Extensions
{
    /// <summary>
    /// Class to log events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomLogger<T> where T:class
    {
        private const string DefaultErrorMessage = "Invalid data";
        private const string ModelStateErrorMessage = "ModelState is InValid";
        private readonly ILogger<T> _log;

        /// <summary>
        /// Initializes a new instance to CustomLogger class
        /// </summary>
        /// <param name="log"></param>
        public CustomLogger(ILogger<T> log) => _log = log;

        /// <summary>
        /// log Error messages
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args) => _log.LogError(message, args);

        /// <summary>
        /// log Error messages with default message
        /// </summary>
        public void Error() => _log.LogError(DefaultErrorMessage);

        /// <summary>
        /// log Error messages for ModeState failures
        /// </summary>
        /// <param name="modelState"></param>
        public void Error(ModelStateDictionary modelState) => _log.LogError(ModelStateErrorMessage);

        /// <summary>
        /// log Exception with message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Exception(Exception ex, string message, params object[] args) => _log.LogError(ex, message, args);

        /// <summary>
        /// log Exception with message
        /// </summary>
        /// <param name="ex"></param>
        public void Exception(Exception ex) => _log.LogError(ex, ex.Message);

        /// <summary>
        /// log Information message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args) => _log.LogInformation(message, args);
    }
}
