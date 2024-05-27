using Cadasvan01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace Cadasvan01.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Funcao, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cidade> Cidades { get; set; }  

        public DbSet<ConfirmacaoDePresenca> Presencas { get; set;}
        public DbSet<Aviso> Avisos { get; set; }    
    }
}
