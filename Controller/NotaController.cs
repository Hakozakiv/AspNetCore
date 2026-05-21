using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    [AutenticacaoSession]
    public class NotaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var notas = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .OrderBy(n => n.Aluno!.Nome)
                .ThenBy(n => n.Disciplina!.Nome)
                .ToListAsync();

            return View(notas);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarCombosAsync();
            return View(new Nota());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nota nota)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombosAsync(nota.AlunoId, nota.DisciplinaId);
                return View(nota);
            }

            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Nota cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }

            await CarregarCombosAsync(nota.AlunoId, nota.DisciplinaId);
            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await CarregarCombosAsync(nota.AlunoId, nota.DisciplinaId);
                return View(nota);
            }

            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Nota atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nota = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
            }

            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Nota excluida com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarCombosAsync(int? alunoId = null, int? disciplinaId = null)
        {
            var alunos = await _context.Alunos.OrderBy(a => a.Nome).ToListAsync();
            var disciplinas = await _context.Disciplinas.OrderBy(d => d.Nome).ToListAsync();

            ViewBag.Alunos = new SelectList(alunos, "Id", "Nome", alunoId);
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", disciplinaId);
        }
    }
}
