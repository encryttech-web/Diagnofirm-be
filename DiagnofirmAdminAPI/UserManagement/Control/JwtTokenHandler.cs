using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Contaxdb;

namespace UserManagement.Controllers
{
    internal class JwtTokenHandler
    {
        private readonly string sgid;
        private readonly string mailid;
        private readonly string userName;

        private const string SGIDLABLE = "SGID";
        private const string MAILLABLE = "MailID";
        private const string USERNAME = "Username";

        private readonly ApplicationSettings _settings;
        public JwtTokenHandler(ApplicationSettings settings, string userid, string username, string usermailId)
        {
            sgid = userid;
            userName = username;
            mailid = usermailId;
            _settings = settings;
        }

        internal string createToken()
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                                  {
                                           new Claim(SGIDLABLE,sgid),
                                           new Claim(MAILLABLE,mailid),
                                           new Claim(USERNAME,userName)
                                  }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWT_Secret)), SecurityAlgorithms.HmacSha384Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
