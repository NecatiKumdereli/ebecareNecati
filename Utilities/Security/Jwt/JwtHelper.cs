using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {

        public IConfiguration _configuration { get; }
        private TokenOptions _tokenOptions = new TokenOptions();
        private DateTime _accessTokenExpiration;
        
        public JwtHelper(IConfiguration configuration, ILogger<JwtHelper> logger)
        {
            _configuration = configuration;

            _tokenOptions.Issuer = _configuration["Jwt:Issuer"].ToString() ?? "";
            _tokenOptions.Audience = _configuration["Jwt:Audience"].ToString() ?? "";
            _tokenOptions.AccessTokenExpiration = int.Parse(_configuration["Jwt:AccessTokenExpiration"].ToString());
            _tokenOptions.SecurityKey = _configuration["Jwt:Key"].ToString();

        }
        /// <summary>
        /// claims_options tipi dynamic ör: new {FullName = "SAİD YUNUS", UserName="saidxs2016", RoleName="Admin", AuthID="Admin.fc30ce45-b631-4c5d-8cd4-bfa1a33e6e4b"}
        /// bir token oluşturulması için gönderilmesi gereken parametereler FullName:Name+Surname, UserName, RoleName, AuthID:Rolename.UserID
        /// Aynen bu formatta
        /// </summary>
        /// <param name="claims_options">Bu bir dynamic tip </param>
        /// <returns></returns>
        public AccessToken CreateToken(dynamic claims_options)
        {
            _accessTokenExpiration = DateTime.UtcNow.AddDays(_tokenOptions.AccessTokenExpiration);
            AccessToken accessToken = new AccessToken(); 
            try
            {  
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var jwt = CreateJwtSecurityToken(_tokenOptions, claims_options, signingCredentials);
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtSecurityTokenHandler.WriteToken(jwt);

                accessToken.Token = token;
                accessToken.Expiration = _accessTokenExpiration;
                //accessToken.ReToken = CreateRefreshToken()
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Token creation failed.");
            }
            return accessToken;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }

        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, dynamic claims_options, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                claims: SetClaims(claims_options),
                signingCredentials: signingCredentials
                );

            return jwt;
        }
        private IEnumerable<Claim> SetClaims(dynamic claims_options)
        {
            var Name = claims_options.GetType().GetProperty("FullName").GetValue(claims_options);
            var RoleName = claims_options.GetType().GetProperty("RoleName").GetValue(claims_options);
            var UserName = claims_options.GetType().GetProperty("UserName").GetValue(claims_options);
            var AuthID = claims_options.GetType().GetProperty("AuthID").GetValue(claims_options);
            //var AuthID = RoleName+"."+Uid; || 
            var claims = new List<Claim>();
            claims.Add(new Claim("FullName", Name));
            claims.Add(new Claim("UserName", UserName));
            claims.Add(new Claim("RoleName", RoleName));
            claims.Add(new Claim("AuthID", AuthID));
            return claims;
        }
    }
}
