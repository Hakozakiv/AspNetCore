using Microsoft.AspNetCore.Mvc; // Resolve IActionResult, Controller, HttpPost
using AspNetCore.Models;       // Resolve Aluno e Professor (ajuste se seu namespace for outro)
using System.Collections.Generic; 

namespace AspNetCore.Controllers
{
    public class AccountController : Controller
    {
        // 1. Carrega a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 2. Recebe os dados digitados
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Simulação de validação (depois você buscará no Banco de Dados)
                if (model.Email == "admin@escola.com" && model.Senha == "1234")
                {
                    // Se estiver correto, manda para a página inicial do sistema
                    return RedirectToAction("Index", "Home");
                }

                // Se estiver errado, exibe erro
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            }

            return View(model);
        }
    }
}