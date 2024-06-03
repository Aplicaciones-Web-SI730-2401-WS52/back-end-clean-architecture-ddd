using _1_API.Response;
using Application;
using Infraestructure;
using AutoMapper;
using Domain;
using LearningCenter.Domain.Publishing.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace TestProject1LearningCenter.Presetation.Test;

public class TutorialControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ResultOk()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockTutorialQueryService = new Mock<ITutorialQueryService>();
        var mockTutorialCommandService = new Mock<ITutorialCommandService>();

        var fakeList = new List<TutorialResponse>()
        {
            new TutorialResponse()
        };
        
        mockTutorialQueryService.Setup(t => t.Handle(new GetAllTutorialsQuery())).ReturnsAsync(fakeList);

        var controller = new TutorialController(mockTutorialQueryService.Object, mockTutorialCommandService.Object, mockMapper.Object);

        //Act
        var result = await controller.GetAsync();

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAsync_ResultNotFound()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockTutorialQueryService = new Mock<ITutorialQueryService>();
        var mockTutorialCommandService = new Mock<ITutorialCommandService>();

        var fakeList = new List<TutorialResponse>();
        
        mockTutorialQueryService.Setup(t => t.Handle(new GetAllTutorialsQuery())).ReturnsAsync(fakeList);

        var controller = new TutorialController(mockTutorialQueryService.Object, mockTutorialCommandService.Object, mockMapper.Object);

        //Act
        var result = await controller.GetAsync();

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
}