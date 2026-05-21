using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [AutenticacaoSession]
    public class ProfessorController : Controller
    {
        private readonly ProfessorRepository _professorRepository;

        public ProfessorController(ProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var professores = await _professorRepository.ObterTodosAsync();
            return View(professores);
        }

        public IActionResult Create()
        {
            return View(new Professor());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return View(professor);
            }

            await _professorRepository.AdicionarAsync(professor);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Professor {professor.Nome} cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var professor = await _professorRepository.ObterPorIdAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Professor professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(professor);
            }

            await _professorRepository.AtualizarAsync(professor);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Professor {professor.Nome} atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var professor = await _professorRepository.ObterPorIdAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _professorRepository.ExcluirAsync(id);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Professor excluido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
