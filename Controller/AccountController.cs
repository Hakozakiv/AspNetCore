using AspNetCore.Data;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var email = model.Email.Trim().ToLowerInvariant();
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.SenhaHash))
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha invalidos.");
                return View(model);
            }

            HttpContext.Session.SetString("Usuario", usuario.Email);

            return RedirectToAction("Index", "Aluno");
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var email = model.Email.Trim().ToLowerInvariant();
            var usuarioJaExiste = await _context.Usuarios.AnyAsync(u => u.Email == email);

            if (usuarioJaExiste)
            {
                ModelState.AddModelError(nameof(model.Email), "Este e-mail ja esta cadastrado.");
                return View(model);
            }

            var usuario = new Usuario
            {
                Email = email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha)
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("Usuario", usuario.Email);

            return RedirectToAction("Index", "Aluno");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
