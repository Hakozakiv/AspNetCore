using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
namespace AspNetCore.Models
{
    public class Aluno : Pessoa
    {
        public string Matricula { get; set; }
        public string Curso { get; set; }
        public List<Disciplina> Disciplinas {get; set;}
    }
}