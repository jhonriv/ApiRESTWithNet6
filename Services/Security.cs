using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiRESTWithNet6.Services
{
    public static class Security
    {
        public static string CreateSHA256(string pass)
        {
            ASCIIEncoding encoding = new();
            StringBuilder sb = new();
            byte[]? stream = SHA256.HashData(encoding.GetBytes(pass));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string? CreateToken(User user)
        {
            try
            {
                var jwtconfig = Settings.GetJwtConfig();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfig.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.GivenName, user.Name ?? ""),
                new Claim(ClaimTypes.Surname, user.LastName ?? ""),
                new Claim(ClaimTypes.Role, user.Role)
            };

                var token = new JwtSecurityToken(
                        jwtconfig.Issuer,
                        jwtconfig.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credentials
                    );


                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
