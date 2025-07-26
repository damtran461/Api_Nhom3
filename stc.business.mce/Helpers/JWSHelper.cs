using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace stc.business.mce.Helpers
{
    public static class JWSHelper
    {
        public static T ConvertToObject<T>(string signedToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(signedToken);
            var payload = jsonToken.Payload;
            var jsonObject = payload.SerializeToJson();

            return JsonConvert.DeserializeObject<T>(jsonObject);
        }
    }
}
