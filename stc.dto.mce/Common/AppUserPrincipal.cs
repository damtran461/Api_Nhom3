using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace stc.dto.mce.Common
{
    public class AppUserPrincipal : ClaimsPrincipal
    {
        public int customer_id { get; set; }
        public string username { get; set; }
        public string customer_name { get; set; }
        public string avatar_url { get; set; }
        public int gender { get; set; }
        public string device_id { get; set; }
        public string fcm_token { get; set; }
        public string session_id { get; set; }
        public string refresh_token { get; set; }
        public int refresh_expiry_time { get; set; }

        public AppUserPrincipal()
        {

        }

        public AppUserPrincipal(List<Claim> claims)
        {
            customer_id = int.Parse(claims.Where(p => p.Type == AppClaimTypes.CustomerID).First().Value);
            username = claims.Where(p => p.Type == AppClaimTypes.Username).First().Value;
            customer_name = claims.Where(p => p.Type == AppClaimTypes.CustomerName).First().Value;
            customer_name = claims.Where(p => p.Type == AppClaimTypes.CustomerName).First().Value;
            avatar_url = claims.Where(p => p.Type == AppClaimTypes.Avatar).First().Value;
            gender = claims.Any(p => p.Type.Contains(AppClaimTypes.Gender)) ? int.Parse(claims.Where(p => p.Type.Contains(AppClaimTypes.Gender)).First().Value) : 0;
            device_id = claims.Where(p => p.Type == AppClaimTypes.DeviceID).First().Value;
            refresh_token = claims.Where(p => p.Type == AppClaimTypes.RefreshToken).First().Value;
            refresh_expiry_time = claims.Any(p => p.Type == AppClaimTypes.RefreshExpiryTime) ? int.Parse(claims.Where(p => p.Type == AppClaimTypes.RefreshExpiryTime).First().Value) : 0;
        }
    }
}
