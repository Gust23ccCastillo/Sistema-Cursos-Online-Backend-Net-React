using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Security.Services_Token
{
    public class JwtGenerateServices : IJwtGenerateContract
    {
        public string CreateTokenSecurityApplication(UserApplication userApplication)
        {
            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userApplication.UserName),
                new Claim(ClaimTypes.NameIdentifier, userApplication.UserName),
            };

            var keyJwt = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mi_clave_secreta_applicacion_xxxxxxxxxxxxxxxxxxxxxx_xxxxxxxxxxxxxxxxxxxxxxx"));

            var credentials = new SigningCredentials(keyJwt, 
                     SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptions = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptions);
            return tokenHandler.WriteToken(token);
        }
    }
}
