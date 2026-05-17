using AspNetCore.Models;
namespace AspNetCore.Data;
public interface IProfessorRepository
{
    Task<List<Professor>> GetAllAsync();
    Task<Professor?> GetByIdAsync(int id);
    Task AdicionarAsync(Professor professor);
    Task AtualizarAsync(Professor professor);
    Task DeletarAsync(int id);
}