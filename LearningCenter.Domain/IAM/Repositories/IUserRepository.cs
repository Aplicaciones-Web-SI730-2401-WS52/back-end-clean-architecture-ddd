using LearningCenter.Domain.IAM.Models;

namespace LearningCenter.Domain.IAM.Repositories;

public interface IUserRepository
{
    Task<int> RegisterAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(int id);
}