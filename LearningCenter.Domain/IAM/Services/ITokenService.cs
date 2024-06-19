using LearningCenter.Domain.IAM.Models;

namespace LearningCenter.Domain.IAM.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
    
}