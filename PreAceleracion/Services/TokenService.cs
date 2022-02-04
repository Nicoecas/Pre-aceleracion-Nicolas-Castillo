using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PreAceleracion.Entities;
using PreAceleracion.Services.Interfaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PreAceleracion.Services
{
    public class TokenService:ITokenService
    {
        private readonly SymmetricSecurityKey _ssKey;
        public TokenService(IConfiguration config)
        {
            _ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.EmailAddress)
            };

            var credenciales = new SigningCredentials(_ssKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(5),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
