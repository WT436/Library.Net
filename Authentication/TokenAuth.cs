using Authentication.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Authentication
{
    public class TokenAuth
    {
        #region Create a token
        /// <summary>
        /// lấy thông tin tài khoản và khởi tạo token 
        /// </summary>
        /// <returns></returns>
        public string GenerateToken(JwtCustomClaims infoToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id",infoToken.Id.ToString()),
                    new Claim("UserName",infoToken.UserName.ToString()),
                    new Claim("Email",infoToken.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = "https://localhost:44306/",
                Audience = "https://localhost:44306/",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("0159c0ca-c966-4410-8650-7fbb4e535513")),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Validating A Token
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateCurrentToken(string token)
        {
            var mySecret = "0159c0ca-c966-4410-8650-7fbb4e535513";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "https://localhost:44306/";
            var myAudience = "https://localhost:44306/";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Read claims
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
        #endregion
      
    }
}