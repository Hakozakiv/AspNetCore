using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Professor)
                .OrderBy(d => d.Nome)
                .ToListAsync();

            return View(disciplinas);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarProfessoresAsync();
            return View(new Disciplina());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disciplina disciplina)
        {
            if (!ModelState.IsValid)
            {
                await CarregarProfessoresAsync(disciplina.ProfessorId);
                return View(disciplina);
            }

            await _context.Disciplinas.AddAsync(disciplina);
            await _context.SaveChangesAsync();
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Disciplina {disciplina.Nome} cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }

            await CarregarProfessoresAsync(disciplina.ProfessorId);
            return View(disciplina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await CarregarProfessoresAsync(disciplina.ProfessorId);
                return View(disciplina);
            }

            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Disciplina {disciplina.Nome} atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas
                .Include(d => d.Professor)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina != null)
            {
                _context.Disciplinas.Remove(disciplina);
                await _context.SaveChangesAsync();
            }

            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Disciplina excluida com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarProfessoresAsync(int? professorId = null)
        {
            var professores = await _context.Professores
                .OrderBy(p => p.Nome)
                .ToListAsync();

            ViewBag.Professores = new SelectList(professores, "Id", "Nome", professorId);
        }
    }
}
