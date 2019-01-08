using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NB.MOBILEPO.BAL.Models;

namespace Microsoft.AspNetCore.Authentication
{
    /// <summary>
    /// class to provide and validate JWT Authentication
    /// </summary>
    public static class JWTAuthenticationExtensions
    {
        /// <summary>
        /// Method to call Authentication with configuration options
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwtBearerAuthentication(this AuthenticationBuilder builder)
            => builder.AddJWTBearerAuthentication(_ => { });

        /// <summary>
        /// Methos to call Authentication
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJWTBearerAuthentication(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJWTOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureJWTOptions: IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AppSettings _appSettings;
            public ConfigureJWTOptions(IOptions<AppSettings> appSettings)
            {
                _appSettings = appSettings.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecretKey);

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidated,
                    OnAuthenticationFailed = OnAuthenticationFailed
                };
            }
            private Task OnAuthenticationFailed(AuthenticationFailedContext arg)
            {
                return Task.FromResult(0);
            }

            private Task OnTokenValidated(TokenValidatedContext arg)
            {
                return Task.FromResult(0);
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
