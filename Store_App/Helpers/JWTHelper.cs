using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AuthApp.Helpers
{

    public class JWTHelper
    {
        private static readonly string Issuer = "https://localhost:44307/";
        private static readonly string Audience = "http://localhost:44443/";
        private static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("PMEZamqA@6bJXS2Xn76Ca6axY!##9yC!%ko&bNW97rZ#Fga4wG2ysD^&2SWVXm%8"));
        private static SymmetricSecurityKey EncryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("42MD#tf!nPwE6JTso#rCYr3a6RdPnHz2"));

        public static string GetToken(int userId)
        {
            var signingCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha512);
            var encryptingCredentials = new EncryptingCredentials(EncryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);
            var currentTime = System.DateTime.Now;
            var expires = System.DateTime.Now.AddDays(1);

            var descriptor = new SecurityTokenDescriptor();
            descriptor.Issuer = Issuer;
            descriptor.Audience = Audience;
            descriptor.Subject = new ClaimsIdentity();
            descriptor.Subject.AddClaim(new Claim("userId", userId.ToString()));
            descriptor.NotBefore = currentTime;
            descriptor.Expires = expires;
            descriptor.SigningCredentials = signingCredentials;
            descriptor.EncryptingCredentials = encryptingCredentials;

            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(descriptor);
        }

        public static bool ValidateToken(string encodedToken)
        {
            var handler = new JsonWebTokenHandler();
            var parameters = new TokenValidationParameters();
            parameters.IssuerSigningKey = SigningKey;
            parameters.TokenDecryptionKey = EncryptionKey;
            parameters.ValidateAudience = false;
            parameters.ValidateIssuer = false;
                
            var validation = handler.ValidateTokenAsync(encodedToken, parameters);
            return validation.Result.IsValid;

        }

        public static int GetUserId(string encodedToken)
        {
            var handler = new JsonWebTokenHandler();
            var parameters = new TokenValidationParameters();
            parameters.IssuerSigningKey = SigningKey;
            parameters.TokenDecryptionKey = EncryptionKey;
            parameters.ValidateAudience = false;
            parameters.ValidateIssuer = false;

            var validation = handler.ValidateTokenAsync(encodedToken, parameters);

            int value = -1;
            if (validation.Result.IsValid)
            {
                var claims = validation.Result.ClaimsIdentity;
                var claim = claims.FindFirst("userId");
                if (claim != null) value = Int32.Parse(claim.Value.ToString());
            }
            return value;
        }
    }
}