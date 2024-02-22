using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;

namespace EPT.Repositories.Repository
{
    public class ProfissionaisRepository : IProfissionais
    {
        private readonly AppDbContext _context;
        public ProfissionaisRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Alterar(Profissionais profissionais, string idprofissional)
        {
            var profissionalalterar = ProfissionalPorId(idprofissional);
            if(profissionalalterar!=null)
            {
                profissionalalterar.Nome = profissionais.Nome;
                _context.SaveChanges();

            }
        }

        public void Excluir(string idprofissional)
        {
            var excluir = ProfissionalPorId(idprofissional);
            _context.Profissionais.Remove(excluir);
            _context.SaveChanges();

        }

        public void Incluir(Profissionais profissional)
        {
            _context.Profissionais.Add(profissional);
            _context.SaveChanges();

        }

        public IEnumerable<Profissionais> Listar()
        {
            return _context.Profissionais;
        }

        public Profissionais ProfissionalPorId(string idprofissional)
        {
            return _context.Profissionais.FirstOrDefault(f => f.ProfissionaisId.ToString() == idprofissional);
        }
    }
}
