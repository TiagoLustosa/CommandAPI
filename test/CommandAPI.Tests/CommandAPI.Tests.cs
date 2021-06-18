using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
  public class CommandTests : IDisposable
  {
    Command testCommand;
    public CommandTests()
    {
      testCommand = new Command
      {
        HowTo = "Do something awesome",
        Platform = "xUnit",
        CommandLine = "dotnet test"
      };
    }

    public void Dispose()
    {
      testCommand = null;
    }
    [Fact]
    public void CanChangeHowTo()
    {
      //Arrange
      //Act
      testCommand.HowTo = "Execute Unit Tests";

      //Assert
      Assert.Equal("Execute Unit Tests", testCommand.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
      //Act
      testCommand.Platform = ".NET";

      //Assert
      Assert.Equal(".NET", testCommand.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
      //Arrange
      var testCommand = new Command
      {
        HowTo = "Do something awesome",
        Platform = "xUnit",
        CommandLine = "dotnet test"
      };

      //Act
      testCommand.Platform = "Platform Test";

      //Assert
      Assert.Equal("Platform Test", testCommand.Platform);
    }
  }
}
