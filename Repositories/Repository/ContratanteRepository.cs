using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;

namespace EPT.Repositories.Repository
{
    public class ContratanteRepository : IContratante
    {
        private readonly AppDbContext _context;

        public ContratanteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Alterar(Contratante contratante, string idcontratante)
        {
            var contratanteoalterar = ContratantePorId(idcontratante);

            if (contratanteoalterar != null)
            {
                contratanteoalterar.contratante = contratante.contratante;
                _context.SaveChanges();
            }

        }

        public Contratante ContratantePorId(string idcontratante)
        {
            return _context.Contratante.FirstOrDefault(f => f.ContratanteId.ToString() == idcontratante);
        }

        public void Excluir(string contratanteId)
        {
            var excluir = ContratantePorId(contratanteId);
            _context.Contratante.Remove(excluir);
            _context.SaveChanges();
        }

        public void Incluir(Contratante contratante)
        {
            _context.Contratante.Add(contratante);
            _context.SaveChanges();
        }

        public IEnumerable<Contratante> Listar()
        {
            return _context.Contratante;
        }
    }
}
