using System.Net.Mime;
using _1_API.Response;
using Application;
using Infraestructure;
using AutoMapper;
using Domain;
using LearningCenter.Domain.Publishing.Models.Queries;
using LearningCenter.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
    ///<summary>Obtain all the active tutorials</summary>
    /// <remarks>
    /// GET /api/Tutorial
    ///   </remarks>
    /// <response code="200">Returns all the tutorials</response>
    /// <response code="404">If there are no tutorials</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet]
    [ProducesResponseType( typeof(List<TutorialResponse>), 200)]
    [ProducesResponseType( typeof(void),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
    [Produces(MediaTypeNames.Application.Json)]
    [CustomAuthorize("mkt")]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        
        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/Search
    [HttpGet]
    [Route("Search")]
    [CustomAuthorize("mkt")]
    public async Task<IActionResult> GetSearchAsync(string? name, int? year)
    {
        //var data = await _tutorialRepository.GetSearchAsync(name, year);

        var result = await _tutorialQueryService.Handle(new GetAllTutorialsQuery());
        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    // GET: api/Tutorial/5
    [HttpGet("{id}", Name = "GetAsync")]
    [CustomAuthorize("mkt")]

    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _tutorialQueryService.Handle(new GetTutorialsByIdQuery(id));

        if (result==null) StatusCode(StatusCodes.Status404NotFound);
        
        return Ok(result);
    }

    // POST: api/Tutorial
    // POST: api/Tutorial
    /// <summary>
    /// Creates a new tutorial.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/tutorial
    ///     {
    ///        "name": "New tutorial",
    ///        "description": ""
    ///     }
    ///
    /// </remarks>
    /// <param name="CreateTutorialCommand">The tutorial to create</param>
    /// <returns>A newly created tutorial</returns>
    /// <response code="201">Returns the newly created tutorial</response>
    /// <response code="400">If the tutorial has invalid property</response>
    /// <response code="409">Error validating data</response>
    /// <response code="500">Unexpected error</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
    [Produces(MediaTypeNames.Application.Json)]
    [CustomAuthorize("admin")]
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
    [CustomAuthorize("admin")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateTutorialCommand command)
    {
        command.Id = id;
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest);

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }

    // DELETE: api/Tutorial/5
    [HttpDelete("{id}")]
    [CustomAuthorize("admin")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        DeleteTutorialCommand command = new DeleteTutorialCommand { Id = id };

        var result = await _tutorialCommandService.Handle(command);

        return Ok();
    }
}