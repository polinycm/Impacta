using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EPT.Repositories.Repository
{
    public class RelatorioRepository:IRelatorio
    {
        private readonly AppDbContext _context;

        public RelatorioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<RelatorioAcervoViewModel> Acervo()
        {
            var consulta = from acervo in _context.Acervo
                           join atestado in _context.Atestado on acervo.AtestadoId equals atestado.AtestadoId
                           join funcao in _context.Funcoes on acervo.FuncoesId equals funcao.FuncoesId
                           join profissional in _context.Profissionais on acervo.ProfissionaisId equals profissional.ProfissionaisId
                          
                           select new RelatorioAcervoViewModel
                           {
                               NumeroEPT = acervo.numero_ept,
                               NumeroAcervo = acervo.numero_acervo,
                               Nome = profissional.Nome,
                               Funcao = funcao.Funcao,
                               AtestadoId = atestado.AtestadoId
                           };
            List<RelatorioAcervoViewModel> listaAcervo = consulta.ToList();
            return listaAcervo;
        }

        public List<RelatorioAcervoViewModel> AcervoFiltro1(int idProfissional, int idAtestado, int idFuncao)
        {
            var consulta = _context.Acervo
                .Include(a => a.Atestado)
                .Include(a => a.Funcoes)
                .Include(a => a.Profissionais)
                .AsQueryable();

            if (idProfissional != 0)
            {
                consulta = consulta.Where(a => a.ProfissionaisId == idProfissional);
            }

            if (idFuncao != 0)
            {
                consulta = consulta.Where(a => a.FuncoesId == idFuncao);
            }

            if (idAtestado!=0)
            {
                consulta = consulta.Where(a => a.AtestadoId == idAtestado);
            }

            var listaAcervo = consulta.Select(acervo => new RelatorioAcervoViewModel
            {
                NumeroEPT = acervo.numero_ept,
                NumeroAcervo = acervo.numero_acervo,
                Nome = acervo.Profissionais.Nome,
                Funcao = acervo.Funcoes.Funcao,
                AtestadoId = acervo.Atestado.AtestadoId
            }).ToList();

           
            return listaAcervo;

        }

        public List<RelatorioAcervoViewModel> AtestadoFiltro1( int ItemId, string quantidadeMenor, string quantidadeMaior,int SubItensId,string unidade, int chk)
        {
            if(chk==1)
            {
                SubItensId = 0;
            }

            var consulta = _context.AtestadoItem
                .Include(a => a.Atestado)
                .Include(a => a.Itens)
                .Include(a => a.SubItens)
                .AsQueryable();

            if (ItemId != 0)
            {
                consulta = consulta.Where(a => a.ItensId == ItemId);
            }
            if (SubItensId != 0)
            {
                consulta = consulta.Where(a => a.SubItensId == SubItensId);
            }
            if (unidade != "0")
            {
                consulta = consulta.Where(a => a.unidade == unidade);
            }

            if (!string.IsNullOrEmpty(quantidadeMaior) && !string.IsNullOrEmpty(quantidadeMenor))
            {
                decimal qtdMaior = Convert.ToDecimal(quantidadeMaior);
                decimal qtdMenor = Convert.ToDecimal(quantidadeMenor);

                consulta = consulta.Where(a => Convert.ToDecimal(a.quantidade) >= qtdMenor && Convert.ToDecimal(a.quantidade) <= qtdMaior);
            }


            var listaAcervo = consulta.Select(acervo => new RelatorioAcervoViewModel
            {
                quantidade = Convert.ToDecimal(acervo.quantidade), 
                unidade = acervo.unidade,
                Item= acervo.Item,
                AtestadoId = acervo.AtestadoId,
                totalQtd = Convert.ToDecimal(acervo.quantidade),
                SubItem=acervo.SubItens.SubItem
            }).ToList();

            

            return listaAcervo;

        }


        public List<RelatorioAcervoViewModel> ItensQuantidade()
        {
            var consulta = from atestadoitem in _context.AtestadosItem

                           select new RelatorioAcervoViewModel
                           {
                               quantidade = Convert.ToDecimal(atestadoitem.quantidade),
                               unidade = atestadoitem.unidade,
                               Item = atestadoitem.Item,
                               AtestadoId = atestadoitem.AtestadoId,
                               totalQtd = Convert.ToDecimal(atestadoitem.quantidade),
                               SubItem = atestadoitem.SubItens.SubItem
                           };
            List<RelatorioAcervoViewModel> listaAcervo = consulta.ToList();


            return listaAcervo;
        }

        public List<Atestado> AtestadoFiltro2(string objeto, DateTime dt_inicio, DateTime dt_fim, decimal valor, string descricao)
        {
            var consulta = _context.Atestado.Include(s=>s.Contratante)
                .AsQueryable();

            if (!string.IsNullOrEmpty(objeto))
            {
                consulta = consulta.Where(a => a.objeto.IndexOf(objeto, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (valor!=0)
            {
                consulta = consulta.Where(a => a.valor.Equals(valor));
            }

            if (!string.IsNullOrEmpty(descricao))
            {
                consulta = consulta.Where(a => a.descricao.Contains(descricao));
            }
            if (dt_inicio!= new DateTime(1, 1, 1) && dt_fim!= new DateTime(1, 1, 1))
            {
                consulta = consulta.Where(a => (a.dt_inicio >= dt_inicio && a.dt_fim <= dt_fim));
            }


            var listaAcervo = consulta.Select(acervo => new Atestado
            {
               objeto = acervo.objeto,
                valor = acervo.valor,
                descricao = acervo.descricao,
                dt_fim = acervo.dt_fim,
                dt_inicio = acervo.dt_inicio,
                AtestadoId = acervo.AtestadoId

            }).ToList();


            return listaAcervo;

        }

       
    }
}
