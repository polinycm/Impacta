using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EPT.Repositories.Repository
{
    public class AtestadoRepository:IAtestado
    {
        private readonly AppDbContext _context;

        public AtestadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Alterar(AtestadoViewModel atestado, string atestadoId)
        {
            var atestadoalterar = AtestadoPorId(atestadoId);

            if (atestadoalterar != null)
            {
                atestadoalterar.objeto=atestado.atestado.objeto;
                atestadoalterar.dt_inicio = atestado.atestado.dt_inicio;
                atestadoalterar.dt_fim = atestado.atestado.dt_fim;
                atestadoalterar.valor = atestado.atestado.valor;
                string valorFormatado = atestado.atestado.valor.Replace(',', '.');
                atestadoalterar.valor=valorFormatado;
                atestadoalterar.ContratanteId = atestado.SelectedContratanteId;
                atestadoalterar.descricao = atestado.atestado.descricao;
                atestadoalterar.Link = atestado.atestado.Link;
                atestadoalterar.moeda = atestado.SelectedMoeda;
                _context.SaveChanges();
            }

        }

        public void Excluir(string idatestado)
        {
            var excluir = AtestadoPorId(idatestado);
            _context.Atestado.Remove(excluir);
            _context.SaveChanges();

        }

        public Atestado AtestadoPorId(string idatestado)
        {
            return _context.Atestado.Include(C => C.Contratante)
                                    .FirstOrDefault(f => f.AtestadoId.ToString() == idatestado);
        }

        public void Incluir(AtestadoViewModel atestado)
        {
            string valorFormatado = atestado.atestado.valor.Replace(',', '.');
            atestado.atestado.valor = valorFormatado;
            atestado.atestado.ContratanteId = atestado.SelectedContratanteId;
            atestado.atestado.moeda=atestado.SelectedMoeda;
          
            _context.Atestado.Add(atestado.atestado);
            _context.SaveChanges();

        }

        public IEnumerable<Atestado> Listar()
        {
            return _context.Atestado.Include(s=>s.Contratante);
        }
    }
}
