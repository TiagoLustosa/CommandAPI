using System.Collections.Generic;
using CommandAPI.Models;
namespace CommandAPI.Data
{
  public class MockCommandAPIRepository : ICommandAPIRepository
  {
    public IEnumerable<Command> GetAllCommands()
    {
      var commands = new List<Command> {
          new Command{Id=0, HowTo="How to generate a migration", CommandLine="dotnet ef migrations add NameOfMigration", Platform=".NET Core EF"},
          new Command{Id=1, HowTo="Run Migrations", CommandLine="dotnet ef database update", Platform=".NET Core EF"},
          new Command{Id=2, HowTo="List active migrations", CommandLine="dotnet ef migrations list", Platform=".NET Core EF"}
      };
      return commands;
    }

    public Command GetCommandById(int id)
    {
      return new Command { Id = 0, HowTo = "How to generate a migration", CommandLine = "dotnet ef migrations add NameOfMigration", Platform = ".NET Core EF" };
    }
    public void CreateCommand(Command command)
    {
      throw new System.NotImplementedException();
    }
    public void UpdateCommand(Command command)
    {
      throw new System.NotImplementedException();
    }
    public void DeleteCommand(Command command)
    {
      throw new System.NotImplementedException();
    }
    public bool SaveChanges()
    {
      throw new System.NotImplementedException();
    }

  }
}