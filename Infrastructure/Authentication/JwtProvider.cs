using Application.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<JwtProvider> _logger;
        public JwtProvider(IOptions<JwtOptions> options, ILogger<JwtProvider> logger)
        {
            _jwtOptions = options.Value;
            _logger = logger;
        }
        public string Generate(Login login)
        {
            var claims = new Claim[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserName!),

            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(2),
                signingCredentials
                );
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("Token created");
            return stringToken;
        }
    }
}
