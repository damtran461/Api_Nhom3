using Autofac;
using Autofac.Core;
using stc.business.mce;
using Core.DataAccess.Implement;
using Core.DataAccess.Interface;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using static stc.business.mce.Constants;

namespace stc.api.mce
{
    public static class ConnectionDBConfig
    {
        public static void RegisterConnectionDB(this ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnection(ApiConfig.Connection.ERP_MSSql)).As<IDbConnection>().InstancePerLifetimeScope();

            builder.RegisterType<DapperReadOnlyRepository>().As<IReadOnlyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DapperRepository>().As<IRepository>().InstancePerLifetimeScope();

            //DefaultMSSql
            //builder.Register(c => new SqlConnection(ApiConfig.Connection.DefaultConnectionMSSql)).Keyed<IDbConnection>(ConnectionEnum.DefaultMSSql.ToString()).InstancePerLifetimeScope();

            //builder.RegisterType<DapperReadOnlyRepository>().Keyed<IReadOnlyRepository>(ConnectionEnum.DefaultMSSql.ToString())
            //    .WithParameter(new ResolvedParameter((pi, ctx) => pi.Name == "Connection",
            //    (pi, ctx) => ctx.ResolveKeyed<IDbConnection>(ConnectionEnum.DefaultMSSql.ToString()))).InstancePerLifetimeScope();

            //builder.RegisterType<DapperRepository>().Keyed<IRepository>(ConnectionEnum.DefaultMSSql.ToString())
            //    .WithParameter(new ResolvedParameter((pi, ctx) => pi.Name == "Connection",
            //    (pi, ctx) => ctx.ResolveKeyed<IDbConnection>(ConnectionEnum.DefaultMSSql.ToString()))).InstancePerLifetimeScope();
            

        }
    }
}
