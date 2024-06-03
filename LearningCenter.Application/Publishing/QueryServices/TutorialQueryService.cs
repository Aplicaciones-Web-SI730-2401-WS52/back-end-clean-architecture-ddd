using _1_API.Response;
using AutoMapper;
using Domain;
using LearningCenter.Domain.Publishing.Models.Queries;

namespace Application;

public class TutorialQueryService : ITutorialQueryService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IMapper _mapper;
    
    public TutorialQueryService(ITutorialRepository tutorialRepository,IMapper mapper)
    {
        _tutorialRepository = tutorialRepository;
        _mapper = mapper;
    }

    public async Task<List<TutorialResponse>?> Handle(GetAllTutorialsQuery query)
    {
       var data =  await _tutorialRepository.GetAllAsync();
        var result = _mapper.Map<List<Tutorial>, List<TutorialResponse>>(data);
        return result;
    }

    public async Task<TutorialResponse?> Handle(GetTutorialsByIdQuery query)
    {
        var data =  await _tutorialRepository.GetById(query.Id);
        var result = _mapper.Map<Tutorial, TutorialResponse>(data);
        return result;
    }
}