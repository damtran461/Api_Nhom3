using Core.Common.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace stc.api.mce.Configs
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            IServiceProvider services = app.ApplicationServices;
            var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    options.OAuthClientId(AppCoreConfig.OauthSwagger.ClientName);
                    options.OAuthAppName(AppCoreConfig.OauthSwagger.ClientName);
                    options.OAuthClientSecret(AppCoreConfig.OauthSwagger.ClientSecret);
                    options.DefaultModelsExpandDepth(-1);
                }
            });

            return app;
        }
    }
}
