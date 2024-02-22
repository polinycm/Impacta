using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;

namespace EPT.Repositories.Repository
{
    public class AcervoRepository:IAcervo
    {

        private readonly AppDbContext _context;

        public AcervoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Acervo AcervoPorId(string idacervo)
        {
            return _context.Acervo.FirstOrDefault(f => f.AcervoId.ToString() == idacervo);
        }

        public Acervo AcervoPorN_Acervo(string n_acervo)
        {
            return _context.Acervo.FirstOrDefault(f => f.numero_acervo == n_acervo);
        }

        public void Alterar(AcervoViewModel acervo, string idacervo)
        {
            var acervoalterar = AcervoPorId(idacervo);

            if (acervoalterar != null)
            {
                acervoalterar.numero_ept = acervo.numero_ept;
                acervoalterar.numero_acervo = acervo.numero_acervo;
                acervoalterar.AcervoId=acervo.AcervoId;
                acervoalterar.ProfissionaisId = acervo.SelectedProfissionaisId;
                acervoalterar.dt_inicio = acervo.dt_inicio;
                acervoalterar.dt_fim = acervo.dt_fim;
                acervoalterar.Link = acervo.Link;
                acervoalterar.FuncoesId = acervo.SelectedFuncoesId;
                _context.SaveChanges();
            }
        }

        public void Excluir(string idacervo)
        {
            var excluir = AcervoPorId(idacervo);
            
            _context.Acervo.Remove(excluir);
            _context.SaveChanges();
        }

        public void Incluir(AcervoViewModel acervo)
        {
            Acervo acervos = new Acervo();
            {
                acervos.numero_acervo = acervo.numero_acervo;
                acervos.numero_ept = acervo.numero_ept;
               
                acervos.AtestadoId = acervo.SelectedAtestadoId;
                acervos.ProfissionaisId = acervo.SelectedProfissionaisId;
                acervos.FuncoesId = acervo.SelectedFuncoesId;
                acervos.dt_inicio = acervo.dt_inicio;
                acervos.dt_fim = acervo.dt_fim;
                acervos.Link=acervo.Link;

            }
            _context.Acervo.Add(acervos);
            _context.SaveChanges();

        }

        public IEnumerable<Acervo> Listar()
        {
            return _context.Acervo;
        }
    }
}
