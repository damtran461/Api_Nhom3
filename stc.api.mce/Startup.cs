using Autofac;
using stc.api.mce.Configs;
using stc.business.mce;
using Core.API.GlobalEngine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace stc.api.mce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettingRegister.Binding(configuration);
            HostBuilderItem.ConfigurationItem = configuration;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            HostBuilderItem.ServiceCollectonItem = services;
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
            });

            //if (ApiConfig.Common.ModuleAllowSwagger)
            //{
            //    services.AddSwaggerGen();
            //    services.ConfigureOptions<ConfigureSwaggerOptions>();
            //}
        }
        //this method call by the runtime. when use register use AutofacServiceProviderFactory in function startup in program.cs
        public void ConfigureContainer(ContainerBuilder builder)
        {
            HostBuilderItem.ContainerBuilderItem = builder;
            builder.RegisterModule<AutoFacModule>();
            builder.RegisterConnectionDB();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            HostBuilderItem.ApplicationBuilderItem = app;

            //if (ApiConfig.Common.ModuleAllowSwagger)
            //{
            //    app.UseSwaggerWithVersioning();
            //}
        }
    }
}
