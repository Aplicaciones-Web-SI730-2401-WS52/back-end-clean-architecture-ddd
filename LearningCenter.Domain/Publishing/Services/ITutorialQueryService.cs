using _1_API.Response;
using LearningCenter.Domain.Publishing.Models.Queries;

namespace Domain;

public interface ITutorialQueryService
{    
    Task<List<TutorialResponse>?> Handle(GetAllTutorialsQuery query);
    Task<TutorialResponse?> Handle(GetTutorialsByIdQuery query);
    
}