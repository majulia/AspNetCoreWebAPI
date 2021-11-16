using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebApi.Data;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
   public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add (aluno);
            
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }

            return BadRequest ("aluno não cadastro");
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado");

              _repo.Update(aluno);
             if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }

                return BadRequest("Não possível atualizar.");

        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
             var alu = _repo.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);
             if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }

                return BadRequest("Não possível atualizar.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
             if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado.");
            }

                return BadRequest("Não possível deletar.");

            // _context.Remove (aluno);
            // _context.SaveChanges();
            // return Ok();
        }

        // api/aluno/nome
        // [HttpGet("byName")]
        // public IActionResult GetByName(string nome, string sobrenome)
        // {
        //     var aluno =
        //         _context
        //             .Alunos
        //             .FirstOrDefault(a =>
        //                 a.Nome.Contains(nome) &&
        //                 a.Sobrenome.Contains(sobrenome));

        //     if (aluno == null) return BadRequest("Aluno não encontrado");
        //     return Ok(aluno);
        // }
    }
}
