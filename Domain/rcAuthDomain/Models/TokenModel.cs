using Microsoft.IdentityModel.Tokens;
using rcAuthDomain.Entities;
using rcAuthDomain.Interfaces;
using rcCryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rcAuthDomain.Models
{
    public class TokenModel : ITokenModel
    {
        public string GenerateToken(AuthEntity authEntity) 
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            Claim[] myClaims = new[] {
                new Claim("login", authEntity.Login),
                new Claim("system", "sistema"), //authEntity.System),
                new Claim("guid", Guid.NewGuid().ToString())
            };

            string myHashKey = Crypto.GetKeyMD5("rc-authentication-key");
            SymmetricSecurityKey myKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(myHashKey));

            SigningCredentials myCredentials = new SigningCredentials(myKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(myClaims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = myCredentials,
                Issuer = "rc-issuer",
                Audience = "rc-audience"
            };
            
            SecurityToken security = handler.CreateToken(descriptor);
            string token = handler.WriteToken(security);

            return token;
        }
    }
}
