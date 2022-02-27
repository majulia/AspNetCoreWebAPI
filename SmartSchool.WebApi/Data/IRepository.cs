using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebApi.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool SaveChanges();

        //Alunos
        Task<PageList<Aluno>> GetAllAlunosAsync( PageParams pageParams,
        bool includeProfessor);

        Aluno[] GetAllAlunos(bool includeProfessor = false);

        Task<Aluno[]> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false);

        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        //Professores
        Professor[] GetAllProfessores(bool includeAluno = false);

        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);

        Professor GetProfessorByID(int professorId, bool includeAlunos = false);
        Professor[] GetProfessoresByAluno(int alunoId, bool includeAlunos = false);
    }
}
