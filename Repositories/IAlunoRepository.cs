using AspNetCore.Models;
using Aluno = AspNetCore.Controllers.AlunoController;
namespace AspNetCore.Data;
public interface IAlunoRepository
{
    Task<List<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(int id);
    Task AdicionarAsync(Aluno aluno);
    Task AtualizarAsync(Aluno aluno);
    Task DeletarAsync(int id);
}