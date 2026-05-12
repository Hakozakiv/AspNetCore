using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
namespace AspNetCore.Models
{
    public class Professor : Pessoa
    {
        public string Siap { get; set; }
        public string Area{ get; set; }
    }
}