using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.v1.Dtos;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/[controller]")]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;

        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorByID(id, false);
            if (professor == null)
                return BadRequest("Professor não encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professor);    
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}",
                _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorByID(id, false);
            if (professor == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map (model, professor);

            _repo.Update (professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}",
                _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Não possível atualizar.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorByID(id, false);
            if (professor == null)
                return BadRequest("Professor não encontrado");

            _repo.Update (professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}",
                _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Não possível atualizar.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorByID(id, false);
            if (professor == null)
                return BadRequest("Professor não encontrado");

            _repo.Delete (professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado.");
            }

            return BadRequest("Não possível deletar.");
        }

        //  [HttpGet ("byName")]
        // public IActionResult GetByName(string nome)
        // {
        //    var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome)
        //     );

        //     if (professor == null) return BadRequest("Aluno não encontrado");
        //      return Ok(professor);
        // }
    }
}
