using AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
 

namespace AspNetCore.Data;

public class ProfessorRepository
{
    readonly  DbSet<Professor> _context;

    public ProfessorRepository( DbSet<Professor> context)
    {
        _context = context;
    }

    public async Task<bool> CriarProfessorAsync(Professor professor)
    {
        await _context.AddAsync(professor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Professor>> GetAllProfessoresAsync()
    {
        return await _context.Professor.ToListAsync();
    }

    public async Task<Professor?> GetByIdAsync(int id)
    {
        // Usa FindAsync para buscar pela Chave Primária (Id)
        return await _context.Professor.FindAsync(id);
    }

    public async Task<bool> AtualizarAsync(Professor professor)
    {
        _context.Professor.Update(professor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var professor = await GetByIdAsync(id);
        if (professor == null) return false;

        _context.Professor.Remove(professor);
        await _context.SaveChangesAsync();
        return true;
    }
}