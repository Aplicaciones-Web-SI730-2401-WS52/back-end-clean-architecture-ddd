using Presentation.Request;

namespace Domain;

public interface ITutorialCommandService
{
    Task<int> Handle(CreateTutorialCommand command);
    Task<bool> Handle(UpdateTutorialCommand command);
    Task<bool> Handle(DeleteTutorialCommand command);
}