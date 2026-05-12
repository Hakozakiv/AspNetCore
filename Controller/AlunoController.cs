using Microsoft.AspNetCore.Mvc; // Resolve IActionResult, Controller, HttpPost
using AspNetCore.Models;       // Resolve Aluno e Professor (ajuste se seu namespace for outro)
using System.Collections.Generic; 

namespace AspNetCore.Controllers // Verifique se o nome da pasta é Controller ou Controllers
{
    public class AlunoController : Controller
    {
        // Simulação de uma lista (em um sistema real, viria do Banco de Dados)
        private static List<Aluno> alunos = new List<Aluno>();

        // GET: Aluno
        public IActionResult Index()
        {
            return View(alunos);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Create
        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                alunos.Add(aluno); // Salva na lista temporária
                return RedirectToAction("Index");
            }
            return View(aluno);
        }
    }
}