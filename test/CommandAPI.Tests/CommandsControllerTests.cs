using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Profiles;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
  public class CommandsControllerTests : IDisposable
  {
    Mock<ICommandAPIRepository> mockRepository;
    CommandsProfile realProfile;
    MapperConfiguration configuration;
    IMapper mapper;

    public CommandsControllerTests()
    {
      mockRepository = new Mock<ICommandAPIRepository>();
      realProfile = new CommandsProfile();
      configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
      mapper = new Mapper(configuration);
    }

    public void Dispose()
    {
      mockRepository = null;
      mapper = null;
      configuration = null;
      realProfile = null;
    }

    [Fact]
    public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
    {
      //Arrange

      mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

      var controller = new CommandsController(mockRepository.Object, mapper);


      //Act
      var result = controller.GetAllCommands();

      //Assert
      var okResult = result.Result as OkObjectResult;
      var commands = okResult.Value as List<CommandReadDto>;
      Assert.Empty(commands);
    }

    [Fact]
    public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetAllCommands();

      //Assert
      var okResult = result.Result as OkObjectResult;

    }

    [Fact]
    public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetAllCommands();

      //Assert 
      Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetAllCommands();

      //Assert
      Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }

    [Fact]
    public void GetCommandById_Returns404NotFound_WhenNonExistentIDProvided()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetCommandById(1);

      //Assert
      Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommandById_Returns200Ok_WhenValidIdProvided()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetCommandById(1);

      //Assert
      Assert.IsType<OkObjectResult>(result.Result);

    }

    [Fact]
    public void GetCommandById_ReturnsCorrectType_WhenCorrectIdIsProvided()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.GetCommandById(1);

      //Assert
      Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.CreateCommand(new CommandCreateDto
      {
      });

      //Assert
      Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.CreateCommand(new CommandCreateDto
      {
      });

      //Assert
      Assert.IsType<CreatedAtRouteResult>(result.Result);
    }

    [Fact]
    public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.UpdateCommand(1, new CommandUpdateDto
      {
      });

      //Assert
      Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.UpdateCommand(10, new CommandUpdateDto
      {
      });

      //Assert
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.PartialCommandUpdate(10, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto>
      {
      });

      //Assert
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns204NoContent_WhenValidResourceIdSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.DeleteCommand(1);

      //Assert
      Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
    {
      //Arrange
      mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(() => new Command
      {
        Id = 1,
        HowTo = "mock",
        Platform = "mock",
        CommandLine = "mock"
      });

      var controller = new CommandsController(mockRepository.Object, mapper);

      //Act
      var result = controller.DeleteCommand(10);

      //Assert
      Assert.IsType<NotFoundResult>(result);
    }


    private List<Command> GetCommands(int num)
    {
      var commands = new List<Command>();
      if (num > 0)
      {
        commands.Add(new Command
        {
          Id = 0,
          HowTo = "How to generate a migration",
          CommandLine = "dotnet ef migrations add NameOfMigration",
          Platform = ".NET Core EF"
        });
      }
      return commands;
    }
  }
}