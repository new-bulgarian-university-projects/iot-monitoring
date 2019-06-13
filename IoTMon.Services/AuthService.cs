using IoTMon.Models.DTO;
using IoTMon.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IoTMon.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthParameters authParams;

        public AuthService(AuthParameters authParams)
        {
            if (authParams == null)
            {
                throw new ArgumentNullException("authParams");
            }

            this.authParams = authParams;
        }

        public string GenerateToken(UserDto user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authParams.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: this.authParams.Issuer,
                audience: this.authParams.Issuer,
                claims: new List<Claim>()
                {
                        new Claim("email", user.Email),
                        new Claim("id", user.Id.ToString()),
                        new Claim("firstName", user.FirstName)
                },
                expires: DateTime.Now.AddMinutes(this.authParams.LifeTimeMinutes),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
