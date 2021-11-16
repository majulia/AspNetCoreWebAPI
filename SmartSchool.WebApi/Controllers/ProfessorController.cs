using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
    private readonly IRepository _repo;

    public ProfessorController(IRepository repo){
            _repo = repo;                                
    }

        [HttpGet]
        public IActionResult Get(){
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet ("{id}")]

        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorByID(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }

         [HttpPost]
        public IActionResult Post(Professor professor)
        {
             if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest ("Professor não cadastro");
        }

        [HttpPut("{id}")]
       public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorByID(id, false);
            if (prof == null) return BadRequest("Aluno não encontrado");
            
             _repo.Update(professor);
             if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

                return BadRequest("Não possível atualizar.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor){
        var prof =  _repo.GetProfessorByID(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

             _repo.Update(professor);
             if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

                return BadRequest("Não possível atualizar.");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
        var professor = _repo.GetProfessorByID(id, false);
        if (professor == null) return BadRequest("Professor não encontrado");

         _repo.Delete(professor);
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