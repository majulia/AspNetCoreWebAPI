using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.v1.Dtos;
using SmartSchool.WebAPI.Data;
using AutoMapper;
using SmartSchool.WebApi.Helpers;

namespace SmartSchool.WebAPI.v1.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiversion}/[controller]")]
  public class AlunoController : ControllerBase
  {
    public readonly IRepository _repo;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>

    public AlunoController(IRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }


 //localhost:5000/api/v1/aluno?pageNumber=1&pageSize=5&ativo=1
 
    /// <summary>
    /// Métodos responsável por retornar todos os alunos
    /// </summary>
    /// <returns></returns>

  [HttpGet]
    public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
    {
      var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

      var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

      Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);


      return Ok(alunosResult);
    }

     /// <summary>
     /// Métodos responsável por retornar único aluno
     /// </summary>
     /// <returns></returns>

        [HttpGet("getRegister")]
    public IActionResult GetRegister()
    {
      return Ok(new AlunoRegistrarDto());
    }


    /// <summary>
    /// Responsável por retornar pelo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    // api/aluno/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var aluno = _repo.GetAlunoById(id, false);
      if (aluno == null) return BadRequest("Aluno não encontrado");

      var alunoDto = _mapper.Map<AlunoDto>(aluno);

      return Ok(alunoDto);
    }
        /// <summary>
        /// Cadastrar aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
