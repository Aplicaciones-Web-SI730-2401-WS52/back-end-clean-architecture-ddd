using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Repositories;
using LearningCenter.Domain.IAM.Services;

namespace Application.IAM.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;
    
    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetUserByIdAsync(GetUserByIdQuery query)
    {
      return  await _userRepository.GetUserByIdAsync(query.Id);
    }
}