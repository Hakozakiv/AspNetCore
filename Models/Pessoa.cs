using Microsoft.AspNetCore.Identity;
namespace Models
{
    public class Pessoa 
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}