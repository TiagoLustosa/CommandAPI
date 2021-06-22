using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;
using System;

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
      if (command == null)
      {
        throw new ArgumentNullException(nameof(command));
      }
      _context.CommandItems.Add(command);
      SaveChanges();
    }

    public void UpdateCommand(Command command)
    {
    }
    public void DeleteCommand(Command command)
    {
      if (command == null)
      {
        throw new ArgumentNullException();
      }
      _context.CommandItems.Remove(command);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

  }
}