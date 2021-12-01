using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebApi.Data;
using AutoMapper;

namespace SmartSchool.WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AlunoController : ControllerBase
  {
    public readonly IRepository _repo;
    private readonly IMapper _mapper;

    public AlunoController(IRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

  [HttpGet]
    public IActionResult Get()
    {
      var alunos = _repo.GetAllAlunos(true);
      return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
    }

    [HttpGet("getRegister")]
    public IActionResult GetRegister()
    {
      return Ok(new AlunoRegistrarDto());
    }

    // api/aluno/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var aluno = _repo.GetAlunoById(id, false);
      if (aluno == null) return BadRequest("Aluno não encontrado");

      var alunoDto = _mapper.Map<AlunoDto>(aluno);

      return Ok(alunoDto);
    }

    // api/aluno
    [HttpPost]

     [HttpPost]
    public IActionResult Post(AlunoRegistrarDto model)
    {
      var aluno = _mapper.Map<Aluno>(model);
    
    _repo.Add(aluno);
   if (_repo.SaveChanges())
      {
        return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
      }
      return BadRequest("aluno não cadastrado");
    }

    // api/aluno
    [HttpPut("{id}")]
    public IActionResult Put(int id, AlunoRegistrarDto model)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno não encontrado");

      _mapper.Map(model, aluno);

      _repo.Update(aluno);
      if (_repo.SaveChanges())
      {
        return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
      }

      return BadRequest("Não possível atualizar.");

    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, AlunoRegistrarDto  model)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno não encontrado");

      _mapper.Map(model, aluno);

      _repo.Update(aluno);
      if (_repo.SaveChanges())
      {
        return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
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
