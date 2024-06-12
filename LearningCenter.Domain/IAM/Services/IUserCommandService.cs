using LearningCenter.Domain.IAM.Models.Comands;

namespace LearningCenter.Domain.IAM.Services;

public interface IUserCommandService
{
    Task<string> Handle(SigninCommand command);
    Task<int> Handle(SingupCommand command);
}