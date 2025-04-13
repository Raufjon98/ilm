﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ilmV3.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration configuration)
    {
        var jwtKey = configuration["JWT:SignInKey"];
        ArgumentNullException.ThrowIfNullOrEmpty(jwtKey);
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    }
    public string CreateToken(ApplicationUserDto user)
    {
         var roleClaims = user.Role
          .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
          .Select(role => new Claim(ClaimTypes.Role, role));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
        }
        .Concat(roleClaims)
        .ToList();

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JwT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
