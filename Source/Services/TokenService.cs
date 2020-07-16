using Microsoft.IdentityModel.Tokens;
using Source.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Source.Services
{
    public class TokenService
    {

        private static string _secret = "fedaf7d8863b48e197b9287d492b708e";

        public static byte[] Secret()
        {
            return Encoding.UTF8.GetBytes(_secret);
        }

        public static string GenerateToken(string email, string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Secret();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, email), new Claim(ClaimTypes.Sid, id) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
