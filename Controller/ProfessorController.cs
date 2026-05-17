using Microsoft.AspNetCore.Mvc; // Resolve IActionResult, Controller, HttpPost
using AspNetCore.Models;       // Resolve Aluno e Professor (ajuste se seu namespace for outro)
using System.Collections.Generic;
namespace AspNetCore.Controllers
{
    public class ProfessorController : Controller
    {
        private static List<Professor> professores = new List<Professor>();

        // GET: Professor
        public async Task<IActionResult> Index()
        {
            var professores = await _professorRepository.ObterTodosAsync();
            return View(professores);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        [HttpPost]
        public IActionResult Create(Professor professor)
        {
            if (ModelState.IsValid)
            {
                professores.Add(professor);
                return RedirectToAction("Index");
            }
            return View(professor);
        }

    }
}