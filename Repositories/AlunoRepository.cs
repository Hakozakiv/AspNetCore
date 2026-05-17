using AspNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Data
{
    public class AlunoRepository
    {
        private readonly ApplicationDbContext _context;

        public AlunoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos
                .OrderBy(a => a.Nome)
                .ToListAsync();
        }

        public async Task<Aluno?> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task AdicionarAsync(Aluno aluno)
        {
            if (string.IsNullOrWhiteSpace(aluno.Matricula))
            {
                var totalAlunos = await _context.Alunos.CountAsync();
                aluno.Matricula = $"{DateTime.Now.Year}{totalAlunos + 1:D4}";
            }

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var aluno = await ObterPorIdAsync(id);
            if (aluno == null)
            {
                return;
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
