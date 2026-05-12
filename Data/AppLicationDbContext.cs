using Microsoft.EntityFrameworkCore;
using AspNetCore.Models;

namespace AspNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        // O construtor é necessário para receber as configurações de conexão
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definimos os DbSets, que se tornarão as tabelas no banco
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        // Opcional: Configurações extras de modelagem
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemplo: Definir que o nome é obrigatório e tem tamanho máximo
            modelBuilder.Entity<Aluno>().Property(a => a.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Professor>().Property(p => p.Nome).IsRequired().HasMaxLength(100);
        }
    }
}