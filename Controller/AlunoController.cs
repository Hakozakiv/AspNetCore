using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [AutenticacaoSession]
    public class AlunoController : Controller
    {
        private readonly AlunoRepository _alunoRepository;

        public AlunoController(AlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoRepository.ObterTodosAsync();
            return View(alunos);
        }

        public IActionResult Create()
        {
            return View(new Aluno());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return View(aluno);
            }

            await _alunoRepository.AdicionarAsync(aluno);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(aluno);
            }

            await _alunoRepository.AtualizarAsync(aluno);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alunoRepository.ExcluirAsync(id);
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = "Aluno excluido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
