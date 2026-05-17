using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Email == "admin@escola.com" && model.Senha == "1234")
            {
                return RedirectToAction("Index", "Aluno");
            }

            ModelState.AddModelError(string.Empty, "E-mail ou senha invalidos.");
            return View(model);
        }
    }
}
