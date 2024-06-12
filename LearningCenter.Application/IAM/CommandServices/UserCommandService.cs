using System.Data;
using Domain;
using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Models.Comands;
using LearningCenter.Domain.IAM.Repositories;
using LearningCenter.Domain.IAM.Services;
using Shared;

namespace Application.IAM.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;
    
    public UserCommandService(IUserRepository userRepository,IEncryptService encryptService,ITokenService tokenService)
    {
        _userRepository = userRepository;
        _encryptService = encryptService;
        _tokenService = tokenService;
    }

 
    
    public async Task<string> Handle(SigninCommand command)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(command.Username);
        if (existingUser == null)
        {
            throw new DataException("User doesn't exist");
        }
        

        
        if (!_encryptService.Verify(command.Password, existingUser.PasswordHashed))
        {
            throw new DataException("Invalid password or username");
        }

       return _tokenService.GenerateToken(existingUser);
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
            Role = command.Role,
            PasswordHashed = _encryptService.Encrypt(command.Password)
        };
        var result = await _userRepository.RegisterAsync(user);
        return result;
    }
}