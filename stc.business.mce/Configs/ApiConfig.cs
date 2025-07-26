namespace stc.business.mce
{
    public class ApiConfig
    {
        public static CommonConfig Common;
        public static ERPUserInfo ERPUserInfo;
        public static ConnectionStrings Connection;
    }

    public class CommonConfig
    {
        /// <summary>
        /// Sử dụng trong core get Permission
        /// </summary>
        public string ClientName { get; set; }
        public string Environment { get; set; }
        public string UploadFileFolder { get; set; }
        public int SystemUserID { get; set; }
        public string SystemUsername { get; set; }
        public int MaxRequestBodySize { get; set; }
        public bool ModuleAllowSwagger { get; set; }
        public bool DisableAuthen { get; set; }
        public bool IsUsedWowza { get; set; }
        public int AppCacheTime { get; set; } = 10;
        public int ExcelRecordMaxValue { get; set; }
    }
    public class ConnectionStrings
    {
        public string ERP_MSSql { get; set; }
        //public string DefaultConnectionMSSql { get; set; }
    }

    public class ERPUserInfo
    {
        public bool IsEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientCredentialId { get; set; }
        public string ClientCredentialSecret { get; set; }
        public string ApiScope { get; set; }
    }

}
