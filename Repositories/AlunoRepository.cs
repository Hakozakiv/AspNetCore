using AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Data
{
    public class AlunoRepository
    {
        private readonly ApplicationDbContext _context;

        public AlunoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Busca todos os alunos de forma assíncrona
        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        // Busca por ID (retorna null se não encontrar)
        public async Task<Aluno?> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task AdicionarAsync(Aluno aluno)
        {
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
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CriarAlunoAsync(Aluno aluno)
        {
            aluno.Matricula = $"{DateTime.Now.Year}{_context.Aluno.CountAsync().Result}{new Random().Next(0, 99)}";
            await _context.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}