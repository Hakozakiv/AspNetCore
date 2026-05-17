using AspNetCore.Models;
using AspNetCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspNetCore.Controllers;

public class AlunoController : Controller
{
    readonly AlunoRepository _alunoRepository;

    public AlunoController(AlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var alunos = await _alunoRepository.GetAllAlunosAsync();
        return View(alunos);
    }

    public IActionResult CriarAluno()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarAlunoAsync(Aluno aluno)
    {
        if(await _alunoRepository.CriarAlunoAsync(aluno))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} cadastrado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} não cadastrado!";
        }
        return RedirectToAction("CriarAluno");
    }

    public IActionResult AtualizarAluno()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarAlunoAsync(Aluno aluno)
    {
        if(await _alunoRepository.AtualizarAlunoAsync(aluno))
        {
            TempData["Tipo"] = "success";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} atualizado com sucesso!";
        } else
        {
            TempData["Tipo"] = "danger";
            TempData["Mensagem"] = $"Aluno {aluno.Nome} não atualizado!";
        }
        return RedirectToAction("AtualizarAluno");
    }
}