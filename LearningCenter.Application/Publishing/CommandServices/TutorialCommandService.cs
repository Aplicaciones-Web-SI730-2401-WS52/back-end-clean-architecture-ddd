using System.Data;
using AutoMapper;
using Domain;
using Infraestructure;
using Presentation.Request;
using Shared;

namespace Application;

public class TutorialCommandService : ITutorialCommandService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IMapper _mapper;

    public TutorialCommandService(ITutorialRepository tutorialRepository, IMapper mapper)
    {
        _tutorialRepository = tutorialRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTutorialCommand command)
    {
        var tutorial = _mapper.Map<CreateTutorialCommand, Tutorial>(command);

        var existingTutorial = await _tutorialRepository.GetByNameAsync(tutorial.Name);
        if (existingTutorial != null) throw new DuplicateNameException("Tutorial already exists");

        var total = (await _tutorialRepository.GetAllAsync()).Count;
        if (total >= Constants.MAX_TUTORIALS)
            throw new ConstraintException("Max tutorials reached " + Constants.MAX_TUTORIALS);

        if (tutorial.Sections.Count < Constants.MIN_SECCTIONS)
            throw new ConstraintException("Min sections required " + Constants.MIN_SECCTIONS);

        return await _tutorialRepository.SaveAsync(tutorial);
    }

    public async Task<bool> Handle(UpdateTutorialCommand command)
    {
        var existingTutorial = await _tutorialRepository.GetById(command.Id);
        var tutorial = _mapper.Map<UpdateTutorialCommand, Tutorial>(command);

        if (existingTutorial == null) throw new TutorialNotException("Tutorial not found");

        if (existingTutorial.Description != tutorial.Description)
            throw new ConstraintException("Description can not be updated");

        return await _tutorialRepository.Update(tutorial, tutorial.Id);
    }

    public async Task<bool> Handle(DeleteTutorialCommand command)
    {
        var existingTutorial = _tutorialRepository.GetById(command.Id);
        if (existingTutorial == null) throw new TutorialNotException("Tutorial not found");
        return await _tutorialRepository.Delete(command.Id);
    }
}