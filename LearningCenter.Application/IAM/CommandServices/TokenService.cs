using System.Security.Claims;
using System.Text;
using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace _2_Domain.IAM.CommandServices;

public class TokenService :ITokenService
{
    public string GenerateToken(User user)
    {
        var secret = "learnig-center-upc-wx-2024-asdasdadadadasd";
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }
}