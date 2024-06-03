using AutoMapper;
using Domain;
using Infraestructure;
using Moq;
using NSubstitute;
using Presentation.Request;

namespace Application.Test;

public class TutorialApplicationUnitTest
{
    [Fact]
    public async Task SaveAsync_ValidInput_ReturnValidId()
    {
        //Arrange
        var mock = new Mock<ITutorialRepository>();
        var mockMapper = new Mock<IMapper>();

        CreateTutorialCommand command = new CreateTutorialCommand
        {
            Name = "Tutorial 1",
            Description = "Description",
            Sections = new List<SectionRequest>
            {
                new SectionRequest()
                {
                    Title = "Section 1"
                },
                new SectionRequest()
                {
                    Title = "Section 2"
                }
            }
        };

        mockMapper.Setup(m => m.Map<CreateTutorialCommand, Tutorial>(command)).Returns(new Tutorial
        {
            Name = command.Name,
            Description = command.Description,
            Sections = new List<Section>
            {
                new Section()
                {
                    Title = "Section 1"
                },
                new Section()
                {
                    Title = "Section 2"
                }
            }
        });

        mock.Setup(data => data.GetByNameAsync(command.Name)).ReturnsAsync((Tutorial)null);
        mock.Setup(data => data.GetAllAsync()).ReturnsAsync(new List<Tutorial>());
        mock.Setup(data => data.SaveAsync(It.IsAny<Tutorial>())).ReturnsAsync(1);

        TutorialCommandService tutorialCommandService = new TutorialCommandService(mock.Object, mockMapper.Object);


        //ACt
        var result = await tutorialCommandService.Handle(command);

        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeletAsync_ExistingId_ReturnsTrue()
    {
        //Arrange
        var id = 10;
        var tutorialDataMock = Substitute.For<ITutorialRepository>();
        var mockMapper = Substitute.For<IMapper>();

        tutorialDataMock.GetById(id).Returns(new Tutorial());
        tutorialDataMock.Delete(id).Returns(true);

        DeleteTutorialCommand command = new DeleteTutorialCommand
        {
            Id = id
        };

        TutorialCommandService tutorialCommandService = new TutorialCommandService(tutorialDataMock, mockMapper);

        //Act
        var result = await tutorialCommandService.Handle(command);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeletAsync_NotExistingId_ReturnsFalse()
    {
        //Arrange
        var id = 10;
        var tutorialDataMock = Substitute.For<ITutorialRepository>();
        var mockMapper = Substitute.For<IMapper>();

        tutorialDataMock.GetById(id).Returns((Tutorial)null);
        tutorialDataMock.Delete(id).Returns(true);

        DeleteTutorialCommand command = new DeleteTutorialCommand
        {
            Id = id
        };

        TutorialCommandService tutorialCommandService = new TutorialCommandService(tutorialDataMock, mockMapper);


        //Act and Assert
        Assert.ThrowsAsync<Exception>(() => tutorialCommandService.Handle(command));
    }
}