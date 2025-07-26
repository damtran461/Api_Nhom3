using stc.business.mce;
using Core.API.GlobalEngine;
using Microsoft.Extensions.Configuration;

namespace stc.api.mce
{
    public static class AppSettingRegister
    {
        public static void Binding(IConfiguration configuration)
        {
            ApiConfig.Common = new CommonConfig();
            configuration.Bind("CommonConfig", ApiConfig.Common);

            ApiConfig.ERPUserInfo = new ERPUserInfo();
            configuration.Bind("ERPUserInfo", ApiConfig.ERPUserInfo);

            ApiConfig.Connection = new ConnectionStrings();
            configuration.Bind("ConnectionStrings", ApiConfig.Connection);

            HostBuilderItem.DefaultConnectionString = ApiConfig.Connection.ERP_MSSql;
        }
    }
}
