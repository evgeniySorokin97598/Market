using Microsoft.IdentityModel.Tokens;
using Market.ConfigLib.Entities;
using Market.IdentetyServer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;

namespace Back.Auth
{
    public class JwtGenerator : IJwtGenerator
    {
        private SymmetricSecurityKey _key;

        public JwtGenerator(GlobalConfig global)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(global.TokenKey));
        }

        public string CreateToken(IdentetyUser user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };
            DateTime now = DateTime.Now;

            var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
            notBefore: now,
        claims: claims,
        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);

            //         var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //	Subject = new ClaimsIdentity(claims),
            //	Expires = DateTime.Now.AddDays(7),
            //	SigningCredentials = credentials
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }
    }
}
