using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;
namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Maria",
                Sobrenome = "Oliveira",
                Telefone = "1231546"
            },
            new Aluno(){
                Id = 2,
                Nome = "Thais",
                Sobrenome = "Oliveira",
                Telefone = "1231546"
            },
            new Aluno(){
                Id = 3,
                Nome = "Ana P",
                Sobrenome = "Oliveira",
                Telefone = "1231546"
            },
            new Aluno(){
                Id = 4,
                Nome = "João G",
                Sobrenome = "Oliveira",
                Telefone = "1231546"
            },
        };

        public AlunoController(){}

        [HttpGet]

        public IActionResult Get()
        {
             return Ok(Alunos);
        }

        // api/aluno/1
        [HttpGet ("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
             return Ok(aluno);
        }
        
        // api/aluno/nome
        [HttpGet ("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome)
            );

            if (aluno == null) return BadRequest("Aluno não encontrado");
             return Ok(aluno);
        }
        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
             return Ok(aluno);
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
             return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
             return Ok(aluno);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             return Ok();
        }
    }
}