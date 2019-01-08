using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.Services;
using NB.MOBILEPO.DAL.DbModels;
using NB.MOBILEPO.WEBAPI.Extensions;
using Newtonsoft.Json.Serialization;
using Serilog;
using SoapCore;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Threading.Tasks;

namespace NB.MOBILEPO.WEBAPI.Extensions
{
    /// <summary>
    /// Class to provide custom Configurations
    /// </summary>
    public static class Configurations
    {
        /// <summary>
        /// Add Custom configuration service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuring AppSettings to access through out the application using IOptions
            //var appSettingsSection = configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);
            services.Configure<AppSettings>(options => configuration.Bind("AppSettings", options));
            services.Configure<LNOptions>(options => configuration.Bind("LNOptions", options));
            services.Configure<Subjects>(options => configuration.Bind("Subjects", options));

            //Configuring Database Context
            services.AddDbContext<MobilePoDbContext>(options =>
                            options.UseMySql(configuration.GetConnectionString("defaultconnection")));

            //Configuring Serilog
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
                loggingBuilder.AddSeq(configuration.GetSection("Seq"));
            });

            //Configuring Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearerAuthentication();

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CrossPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //Configururing MVC & WebApi
            services.AddMvc(config => {
                    config.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //configure Swagger
            services.AddSwaggerService();

            // configure DI for application services
            services.ConfigureDIServices();
        }

        /// <summary>
        /// use custom configurations
        /// </summary>
        /// <param name="app"></param>
        public static void UseConfigurations(this IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMiddleware<LogMiddleware>();

            app.UseAuthentication();

            //global policy -assign here or on each controller
            app.UseCors("CrossPolicy");

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSwaggerService();

            app.UseSoapEndpoint<IMobilePOWebService>("/MobilepoWebService.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
        }

        private static void ConfigureDIServices(this IServiceCollection services)
        {
            //Accessing HttpContext on constructor, without using this we will get an error when we try to access HttpContext on constructor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShipmentsService, ShipmentsService>();
            services.AddScoped<IGateEntriesService, GateEntriesService>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IPurchaseOrdersService, PurchaseOrdersService>();
            services.AddScoped<IDashboardService, DashboardService>();
            //services.AddScoped<ICommonService, CommonService>();

            services.AddScoped<IMobilePOWebService, MobilePOWebService>();
        }

        /// <summary>
        /// Configure Kestrel Server with X.509 Certificate
        /// </summary>
        /// <param name="options"></param>
        //public static void ConfigureKestrel(this KestrelServerOptions options)
        //{
        //    var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
        //    var environment = options.ApplicationServices.GetRequiredService<IHostingEnvironment>();

        //    //bind Kestrel server section from appsettings.json
        //    var endpoints = configuration.GetSection("KestrelServer:Endpoints")
        //        .GetChildren()
        //        .ToDictionary(section => section.Key, section =>
        //        {
        //            var endpoint = new KestrelOptions();
        //            section.Bind(endpoint);
        //            return endpoint;
        //        });

        //    foreach (var endpoint in endpoints)
        //    {
        //        var config = endpoint.Value;
        //        var port = config.Port ?? (config.Scheme == "https" ? 443 : 80);

        //        //list out all available IpAddresses of currenct system
        //        var ipAddresses = new List<IPAddress>();
        //        if (config.Host == "localhost")
        //        {
        //            ipAddresses.Add(IPAddress.IPv6Loopback);
        //            ipAddresses.Add(IPAddress.Loopback);
        //        }
        //        else if (IPAddress.TryParse(config.Host, out var address))
        //        {
        //            ipAddresses.Add(address);
        //        }
        //        else
        //        {
        //            ipAddresses.Add(IPAddress.IPv6Any);
        //        }

        //        //listening on each and every IpAddress with Https
        //        foreach (var address in ipAddresses)
        //        {
        //            options.Listen(address, port,
        //                listenOptions =>
        //                {
        //                    if (config.Scheme == "https")
        //                    {
        //                        var certificate = new X509Certificate2(config.FilePath, config.Password);
        //                        listenOptions.UseHttps(certificate);
        //                    }
        //                });
        //        }
        //    }
        //}
    }
}
