using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NB.MOBILEPO.WEBAPI.Extensions
{
    /// <summary>
    /// Class to provide custom Swagger implementation
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// Add swagger to provide API documenatation
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            //Configuring Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MOBILEPO APIs",
                    Description = "nicheBees's MOBILEPO Web APIs",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "MOBILEPO", Email = "contact@nichebees.com", Url = "www.nichebees.com" }
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
                });

                ////Include comments to Swaager
                //c.IncludeXmlComments(string.Format(@"{0}\NB.MOBILEPO.WEBAPI.xml",
                //           System.AppDomain.CurrentDomain.BaseDirectory));

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.IgnoreObsoleteProperties();
                c.IgnoreObsoleteActions();
                c.DescribeAllEnumsAsStrings();

                //Register File Upload Operation Filter
                c.OperationFilter<FileUploadOperationFilter>();
                
            });


        }

        /// <summary>
        /// Use swagger to project
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet Core WebApi 2.1");
                c.DocExpansion(DocExpansion.None);
            });
            
        }
    }

    /// <summary>
    /// Custom upload filter
    /// </summary>
    public class FileUploadOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Method to provide upload control
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters != null && operation.Parameters.Any(p => p.In.ToLower() == "query"))
            {
                var formFileParams = context.ApiDescription.ActionDescriptor.Parameters
                                    .Where(x => x.ParameterType.IsAssignableFrom(typeof(IFormFile)))
                                    .Select(x => x.Name)
                                    .ToList(); ;

                var formFileSubParams = context.ApiDescription.ActionDescriptor.Parameters
                    .SelectMany(x => x.ParameterType.GetProperties())
                    .Where(x => x.PropertyType.IsAssignableFrom(typeof(IFormFile)))
                    .Select(x => x.Name)
                    .ToList();

                var allFileParamNames = formFileParams.Union(formFileSubParams);

                if (!allFileParamNames.Any())
                    return;

                var paramsToRemove = new List<IParameter>();
                foreach (var param in operation.Parameters)
                {
                    //paramsToRemove.AddRange(from fileParamName in allFileParamNames where param.Name.StartsWith(fileParamName + ".") select param);
                    paramsToRemove.AddRange(from fileParamName in allFileParamNames where param.In.ToLower() == "query" select param);
                }
                paramsToRemove.ForEach(x => operation.Parameters.Remove(x));
                foreach (var paramName in allFileParamNames)
                {
                    var fileParam = new NonBodyParameter
                    {
                        Type = "file",
                        Name = paramName,
                        In = "formData"
                    };
                    operation.Parameters.Add(fileParam);
                }
                foreach (IParameter param in operation.Parameters)
                {
                    param.In = "formData";
                }

                operation.Consumes = new List<string>() { "multipart/form-data" };
            }
        }
    }
}
