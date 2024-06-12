using Infraestructure.Contexts;
using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.Infraestructure.IAM.Persistence;

public class UserRepository : IUserRepository
{
    private readonly LearningCenterContext _learningCenterContext;
    
    public UserRepository(LearningCenterContext learningCenterContext)
    {
        _learningCenterContext = learningCenterContext;
    }
    
    
    public async Task<int> RegisterAsync(User user)
    {
        _learningCenterContext.Users.Add(user);
        await _learningCenterContext.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
       var user = await _learningCenterContext.Users.FirstOrDefaultAsync(x => x.Username == username);
       return user;
    }
}