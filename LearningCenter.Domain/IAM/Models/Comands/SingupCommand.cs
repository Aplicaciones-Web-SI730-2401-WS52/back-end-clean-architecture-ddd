namespace LearningCenter.Domain.IAM.Models.Comands;

public record SingupCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}