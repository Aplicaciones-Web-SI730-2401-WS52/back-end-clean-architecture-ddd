using _1_API.Response;
using Application;
using Infraestructure;
using AutoMapper;
using Domain;
using LearningCenter.Domain.Publishing.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Presentation.Request;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorialController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITutorialCommandService _tutorialCommandService;
    private readonly ITutorialQueryService _tutorialQueryService;

    public TutorialController(ITutorialQueryService tutorialQueryService, ITutorialCommandService tutorialCommandService,
        IMapper mapper)
    {
        _tutorialQueryService = tutorialQueryService;
        _tutorialCommandService = tutorialCommandService;
        _mapper = mapper;
    }


    // GET: api/Tutorial
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        
        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/Search
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> GetSearchAsync(string? name, int? year)
    {
        //var data = await _tutorialRepository.GetSearchAsync(name, year);

        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/5
    [HttpGet("{id}", Name = "GetAsync")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _tutorialQueryService.Handle(new GetTutorialsByIdQuery(id));

        if (result==null) StatusCode(StatusCodes.Status404NotFound);
        
        return Ok(result);
    }

    // POST: api/Tutorial
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateTutorialCommand command)
    {
        if (!ModelState.IsValid) return BadRequest();


        var result = await _tutorialCommandService.Handle(command);

        if (result > 0)
            return StatusCode(StatusCodes.Status201Created, result);

        return BadRequest();
    }

    // PUT: api/Tutorial/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateTutorialCommand command)
    {
        command.Id = id;
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }

    // DELETE: api/Tutorial/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        DeleteTutorialCommand command = new DeleteTutorialCommand { Id = id };

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }
}