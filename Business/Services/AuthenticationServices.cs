using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IConfiguration _config;

        public AuthenticationServices(IConfiguration config)
        {
            _config = config;
        }

        private async Task<AuthenticationInfo> SetAuthToken(string username)
        {

            var secretKey = _config.GetValue<string>("Jwt:secretKey"); // "Information"
            dynamic expiryTime = _config.GetValue<string>("Jwt:ExpiryMinutes");
            expiryTime = expiryTime == null ? 5 : int.Parse(expiryTime);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = new AuthenticationInfo { username = username, token = tokenHandler.WriteToken(token), expiresIn = expiryTime * 60 };
            return result;

        }

        public async Task<ServiceResponse> Login(string credentialsUserName, string credentialsPassword)
        {

            if (credentialsUserName == "prueba" && credentialsPassword == "password")
            {
                var authResponse = await SetAuthToken(credentialsUserName);
                return new ServiceResponse()
                {

                    success = true,
                    message = "",
                    code = "AUTH_OK",
                    data = authResponse
                };
            }
            else
            {
                return new ServiceResponse()
                {
                    success = false,
                    code = "WRONG_AUTHENTICATION",
                    message = "Wrong Username or password",
                    data = null
                };
            }


        }


        private static string EncryptPassword(string password)
        {


            var saltString = Environment.GetEnvironmentVariable("API_MANAGEMENT_SALT_KEY");

            byte[] salt = Encoding.UTF8.GetBytes(saltString);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

    }

    public interface IAuthenticationServices
    {
        Task<ServiceResponse> Login(string credentialsUserName, string credentialsPassword);

    }


    public sealed class AuthenticationInfo
    {
        public string username { get; set; }

        public string token { get; set; }

        public bool authenticated { get; set; } = true;

        public int expiresIn { get; set; }

    }

    public class ServiceResponse
    {
        public bool success { get; set; }
        public string code { get; set; } = "";
        public string message { get; set; }
        public dynamic data { get; set; }
        public bool exception { get; set; } = false;
    }
}