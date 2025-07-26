using Core.API.Swagger;
using Core.Common.Configs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace stc.api.mce.Configs
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            var domainName = AppDomain.CurrentDomain.FriendlyName;

            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

            var xmlFile = $"{domainName}.xml";
            var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            var swaggerScopes = new Dictionary<string, string>();
            foreach (var scope in AppCoreConfig.OauthSwagger.Scopes.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(scope))
                    swaggerScopes.Add(scope, $"access to {scope}");
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{AppCoreConfig.URLConnection.IDSUrl}/connect/authorize"),
                        Scopes = swaggerScopes,
                        TokenUrl = new Uri($"{AppCoreConfig.URLConnection.IDSUrl}/connect/token"),
                    }
                },
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Type = SecuritySchemeType.Http,
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new string[] { }
                }
            });

            options.CustomOperationIds(e => $"{e.RelativePath}");
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var domainName = AppDomain.CurrentDomain.FriendlyName;

            var info = new OpenApiInfo()
            {
                Title = domainName,
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
