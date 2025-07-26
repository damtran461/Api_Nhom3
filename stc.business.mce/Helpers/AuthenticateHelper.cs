using Core.Common.Configs;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace stc.business.mce.Helpers
{
    public static class AuthenticateHelper
    {
        public static List<Claim> ValidateToken(string token)
        {
            try
            {
                var cert = new X509Certificate2(Convert.FromBase64String(AppCoreConfig.JWT.Base64PublicKey));
                var parameters = new TokenValidationParameters
                {
                    AudienceValidator = (a, b, c) => true,
                    ValidIssuer = AppCoreConfig.JWT.Issuer,
                    IssuerSigningKeyResolver = (string token1, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters)
                                                => new List<X509SecurityKey> { new X509SecurityKey(cert) }
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken jwt);
                return principal.Claims.ToList();
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw ex;
            }
        }

        public static async Task<string> GetERPTokenByAccount()
        {
            var tokenClient = new TokenClient(new HttpClient(), new TokenClientOptions
            {
                Address = AppCoreConfig.URLConnection.IDSUrl + "/connect/token",
                ClientId = ApiConfig.ERPUserInfo.ClientCredentialId,
                ClientSecret = ApiConfig.ERPUserInfo.ClientCredentialSecret
            });
            var tokenResponse = await tokenClient.RequestPasswordTokenAsync(ApiConfig.ERPUserInfo.Username, ApiConfig.ERPUserInfo.Password, ApiConfig.ERPUserInfo.ApiScope);

            return tokenResponse.AccessToken;
        }
    }
}
