using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("/api/command")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandController(ICommandRepo repo, IMapper mapper)
        {
            _repo=repo;
            _mapper=mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> getAllCommand()
        {
            var commandItems = _repo.getAllCommand();
            
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}",Name="getCommand")]
        public ActionResult<CommandReadDto> getCommand(int id)
        {
            var commandItems = _repo.getCommandByIp(id);
            if(commandItems!=null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItems));
            }
            return NotFound();
            
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto command)
        {
            var commanModal = _mapper.Map<Command>(command);
            _repo.CreateCommand(commanModal);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commanModal);

            //return Ok(commandReadDto);

            return CreatedAtRoute(nameof(getCommand),new{Id = commandReadDto.Id},commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<CommandReadDto> UpdateCommand(int id,CommandCreateDto commandCreateDto)
        {
            var commandModalFromRepo = _repo.getCommandByIp(id);
            if(commandModalFromRepo == null)
            {
                return NotFound();
            }
            //data already updated
            _mapper.Map( commandCreateDto ,commandModalFromRepo);
            //_repo.updateCommand(commandModalFromRepo);

            _repo.SaveChanges();
            
            return NoContent();
            //return CreatedAtRoute(nameof(getCommand),new{Id = commandModalFromRepo.Id},commandModalFromRepo);
        }

        //Patch/Api
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModalFromRepo = _repo.getCommandByIp(id);
            if(commandModalFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch= _mapper.Map<CommandUpdateDto>(commandModalFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map( commandToPatch ,commandModalFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var modal = _repo.getCommandByIp(id);
            if(modal==null)
            {
                return NotFound();
            }
            _repo.Delete(modal);
            _repo.SaveChanges();
            return Ok(modal);
        }
        
    }
}