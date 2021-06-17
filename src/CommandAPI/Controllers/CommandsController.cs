using System.Collections.Generic;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace CommandAPI.Controllers
{
  [Route("api/commands")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandAPIRepository _repository;

    public CommandsController(ICommandAPIRepository repository)
    {
      _repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Command>> Get()
    {
      var commandItems = _repository.GetAllCommands();
      return Ok(commandItems);
    }
    [HttpGet("{id}")]
    public ActionResult<Command> GetCommandById(int id)
    {
      var commandItem = _repository.GetCommandById(id);
      if (commandItem == null)
      {
        return NotFound();
      }
      return Ok(commandItem);
    }
  }
}