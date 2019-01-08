using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NB.MOBILEPO.WEBAPI.Extensions
{
    /// <summary>
    /// Class to uses Serilog with Seq to log events
    /// </summary>
    public class LogMiddleware
    {
         const string MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        static readonly ILogger Log = Serilog.Log.ForContext<LogMiddleware>();
        //private static readonly string Bearer = "bearer";
        //private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
        readonly RequestDelegate _next;
        
        /// <summary>
        /// Custom log middleware constructor
        /// </summary>
        /// <param name="next"></param>
        public LogMiddleware(RequestDelegate next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
        }

        /// <summary>
        /// Invoking every Http request
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var sw = Stopwatch.StartNew();
            try
            {
                //Authentication
                string requested_by = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                //var token = httpContext.Request.Headers[HeaderNames.Authorization].ToString();
                //if (!token.ToLower().StartsWith(Bearer))
                //{
                //    throw new InvalidOperationException(string.Format("Expected {0} at the start of the token.", Bearer));
                //}

                //var jwt = _handler.ReadJwtToken(token.Substring(Bearer.Length).TrimStart());
                //httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims));

                //await _next(httpContext);

                var authResult = await httpContext.AuthenticateAsync();
                if (authResult.Succeeded)
                {
                    httpContext.User = authResult.Principal;
                    Log.Write(LogEventLevel.Information, "Authenticated successfully");
                }
                //else if (authResult.Failure?.Message.Contains("token is expired") == true)
                //{
                //    httpContext.Response.StatusCode = StatusCodes.Status410Gone;
                //    return;
                //}

                await _next(httpContext);
                sw.Stop();

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;
                
                ILogger log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;
                log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) when (LogException(httpContext, sw, ex)) { }
        }

        static bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();

            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode, sw.Elapsed.TotalMilliseconds);
            return false;
        }

        static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = Log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }
    }
}