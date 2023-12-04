using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Store_App.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Store_App.Helpers
{

    public class JWTHelper : IJWTHelper
    {
        private static readonly string Issuer = "https://localhost:44307/";
        private static readonly string Audience = "http://localhost:44443/";
        private static readonly SymmetricSecurityKey SigningKey = new(Encoding.ASCII.GetBytes("PMEZamqA@6bJXS2Xn76Ca6axY!##9yC!%ko&bNW97rZ#Fga4wG2ysD^&2SWVXm%8"));
        private static readonly SymmetricSecurityKey EncryptionKey = new(Encoding.ASCII.GetBytes("42MD#tf!nPwE6JTso#rCYr3a6RdPnHz2"));
        private static readonly SigningCredentials signingCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha512);
        private static readonly EncryptingCredentials encryptingCredentials = new(EncryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

        public  string GetToken(int accountId)
        {
            var currentDate = System.DateTime.Now;
            var expirationDate = currentDate.AddDays(1);
            var descriptor = GetDescriptor(accountId, currentDate, expirationDate);
            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(descriptor);
        }

        private SecurityTokenDescriptor GetDescriptor(int accountId, DateTime currentDate, DateTime expirationDate)
        {
            var descriptor = new SecurityTokenDescriptor();          
            descriptor.Issuer = Issuer;
            descriptor.Audience = Audience;
            descriptor.Subject = new ClaimsIdentity();
            descriptor.Subject.AddClaim(new Claim("accountId", accountId.ToString()));
            descriptor.NotBefore = currentDate;
            descriptor.Expires = expirationDate;
            descriptor.SigningCredentials = signingCredentials;
            descriptor.EncryptingCredentials = encryptingCredentials;
            return descriptor;
        }

        public bool ValidateToken(string encodedToken)
        {
            var handler = new JsonWebTokenHandler();
            var parameters = GetTokenValidationParameters();   
            var validation = handler.ValidateTokenAsync(encodedToken, parameters);
            return validation.Result.IsValid;
        }

        public int GetAccountId(string encodedToken)
        {
            var handler = new JsonWebTokenHandler();
            var parameters = GetTokenValidationParameters();
            var validation = handler.ValidateToken(encodedToken, parameters);

            if (!validation.IsValid)
            {
                throw new UnauthorizedException("User not authorized to access this resouce.");
            }

            var claims = validation.ClaimsIdentity;
            var claim = claims.FindFirst("accountId");

            if (claim is null)
            {
                throw new UnauthorizedException("User not authorized to access this resouce.");
            }

            return int.Parse(claim.Value);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var parameters = new TokenValidationParameters();
            parameters.IssuerSigningKey = SigningKey;
            parameters.TokenDecryptionKey = EncryptionKey;
            parameters.ValidateAudience = false;
            parameters.ValidateIssuer = false;
            return parameters;
        }
    }
}