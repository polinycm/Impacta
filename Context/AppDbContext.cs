using EPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EPT.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Acervo> Acervo {  get; set; }
        public DbSet<Atestado> Atestado { get; set; }
        public DbSet<AtestadoItem> AtestadoItem { get; set; }
        public DbSet<Funcoes> Funcoes { get; set; }
        public DbSet<Itens> Itens { get; set; }
        public DbSet<AtestadoItem> AtestadosItem { get; set; }
        public DbSet<Profissionais> Profissionais { get; set; }
        public DbSet<Contratante> Contratante { get; set; }
        public DbSet<SubItens> SubItens { get; set; }
    }
}
