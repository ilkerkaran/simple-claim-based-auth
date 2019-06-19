using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Services
{
    public class AuthService : IAuthService
    {
        public const string secret = "very-complex-and-long-secret";
        private readonly IAudienceService audienceService;

        public AuthService(IAudienceService audienceService)
        {
            this.audienceService = audienceService;
        }

        public async Task<string> Authenticate(string apiKey, string sharedSecret)
        {
            //get audience by apikey and password from database
            //create token from createdobject 
            var audience = await audienceService.GetByCredentials(apiKey, sharedSecret);
            // return null if auudience not found
            if (audience == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            //arange claims from permissions
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, audience.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(audience.Permissions.Where(p => p.Value).Select(p => new Claim(ClaimsIdentity.DefaultRoleClaimType, p.Key.GetHashCode().ToString())));

            var token = new JwtSecurityToken(
                audience.Name,
                audience.Name,
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
