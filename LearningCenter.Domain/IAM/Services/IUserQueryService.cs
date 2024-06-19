using LearningCenter.Domain.IAM.Models;

namespace LearningCenter.Domain.IAM.Services;

public interface IUserQueryService
{
    Task<User?> GetUserByIdAsync(GetUserByIdQuery query);
}