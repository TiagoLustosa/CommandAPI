using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace CommandAPI.Controllers
{
  [Route("api/commands")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandAPIRepository _repository;
    private IMapper _mapper;

    public CommandsController(ICommandAPIRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
      var commandItems = _repository.GetAllCommands();
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }
    [Authorize]
    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
      var commandItem = _repository.GetCommandById(id);
      if (commandItem == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      _repository.CreateCommand(commandModel);
      _repository.SaveChanges();

      var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

      return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
    }
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
      var commandModelFromRepository = _repository.GetCommandById(id);
      if (commandModelFromRepository == null)
      {
        return NotFound();
      }
      _mapper.Map(commandUpdateDto, commandModelFromRepository);
      _repository.UpdateCommand(commandModelFromRepository);
      _repository.SaveChanges();
      return NoContent();
    }
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
    {
      var commandModelFromRepository = _repository.GetCommandById(id);
      if (commandModelFromRepository == null)
      {
        return NotFound();
      }
      var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepository);
      patchDocument.ApplyTo(commandToPatch, ModelState);
      if (!TryValidateModel(commandToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(commandToPatch, commandModelFromRepository);
      _repository.UpdateCommand(commandModelFromRepository);
      _repository.SaveChanges();
      return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
      var commandModelFromRepository = _repository.GetCommandById(id);
      if (commandModelFromRepository == null)
      {
        return NotFound();
      }
      _repository.DeleteCommand(commandModelFromRepository);
      _repository.SaveChanges();
      return NoContent();
    }
  }
}