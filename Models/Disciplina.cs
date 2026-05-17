using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCore.Models
{
    public class Disciplina

    {
        public int id { get; set; }
        public string Nome { get; set; } = "";
        public int CargaHoraria { get; set; }
        public Professor? Professor { get; set; }
        public List<Aluno>? Alunos { get; set; }
        public string Periodo { get; set; }= "";

    }
}