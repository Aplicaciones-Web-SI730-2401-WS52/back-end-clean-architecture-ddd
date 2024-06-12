using System.Data;
using Domain;
using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Models.Comands;
using LearningCenter.Domain.IAM.Repositories;
using LearningCenter.Domain.IAM.Services;

namespace Application.IAM.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptService _encryptService;
    
    public UserCommandService(IUserRepository userRepository,IEncryptService encryptService)
    {
        _userRepository = userRepository;
        _encryptService = encryptService;
    }
 
    
    public async Task<string> Handle(SigninCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Handle(SingupCommand command)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(command.Username);
        if (existingUser != null)
        {
            throw new ConstraintException("User already exists");
        }
        
        var user = new User()
        {
            Username = command.Username,
            PasswordHashed = _encryptService.Encrypt(command.Password)
        };
        var result = await _userRepository.RegisterAsync(user);
        return result;
    }
}