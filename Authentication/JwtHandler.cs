using Authentication.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Authentication
{
    public class JwtHandler : IJwtHandler
    {
        private readonly ExternalClientJsonConfiguration _settings;
        private static IOptions<ExternalClientJsonConfiguration> options;
        private static readonly ExternalClientJsonConfiguration externalClientJsonConfiguration = new ExternalClientJsonConfiguration();

        public JwtHandler()
        {
            if (externalClientJsonConfiguration.Issuer == null)
            {
                var configurationBuilder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();
                configurationBuilder.GetSection("ExternalClientServer")
                                    .Bind(externalClientJsonConfiguration);

                options = Options.Create(externalClientJsonConfiguration);
            }

            _settings = options.Value;
        }

        public JwtResponse CreateToken(JwtCustomClaims claims)
        {
            var privateKey = _settings.RsaPrivateKey.ToByteArray();
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };
            var now = DateTime.Now;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
            var jwt = new JwtSecurityToken(
                audience: _settings.Audience,
                issuer: _settings.Issuer,
                claims: new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(nameof(claims.Id),claims.Id.ToString()),
                    new Claim(nameof(claims.UserName),claims.UserName.ToString()),
                    new Claim(nameof(claims.Email),claims.Email.ToString())
                },
                notBefore: now,
                expires: now.AddMinutes(1),
                signingCredentials: signingCredentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtResponse
            {
                Token = token
            };
        }

        public JwtCustomClaims ReadFullHisTory(string token)
        {
            JwtCustomClaims infoToken = new JwtCustomClaims();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                infoToken.Id = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.Id)).Value;
                infoToken.UserName = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.UserName)).Value;
                infoToken.Email = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.Email)).Value;
            }
            return infoToken;
        }

        public JwtCustomClaims ReadFullInfomation(string token)
        {
            JwtCustomClaims infoToken = new JwtCustomClaims();
            if (!string.IsNullOrEmpty(token) && ValidateToken(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                infoToken.Id = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.Id)).Value;
                infoToken.UserName = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.UserName)).Value;
                infoToken.Email = securityToken.Claims.First(claim => claim.Type == nameof(infoToken.Email)).Value;
            }
            return infoToken;
        }

        public bool ValidateToken(string token)
        {
            var publicKey = _settings.RsaPublicKey.ToByteArray();
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(publicKey, out _);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new RsaSecurityKey(rsa),

                CryptoProviderFactory = new CryptoProviderFactory()
                {
                    CacheSignatureProviders = false
                }
            };
            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, validationParameters, out var validatedSecurityToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}