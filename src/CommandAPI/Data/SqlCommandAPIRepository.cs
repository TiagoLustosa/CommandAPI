using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data
{
  public class SqlCommandAPIRepository : ICommandAPIRepository
  {
    private readonly CommandContext _context;

    public SqlCommandAPIRepository(CommandContext context)
    {
      _context = context;
    }
    public IEnumerable<Command> GetAllCommands()
    {
      return _context.CommandItems.ToList();
    }

    public Command GetCommandById(int id)
    {
      var commandItem = _context.CommandItems.FirstOrDefault(x => x.Id == id);
      return commandItem;
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