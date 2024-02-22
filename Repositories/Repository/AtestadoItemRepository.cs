using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EPT.Repositories.Repository
{
    public class AtestadoItemRepository:IAtestadoItem
    {
        private readonly AppDbContext _context;
        public AtestadoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AlterarItem(AtestadoItem atestadoItem, string atestadoItemId)
        {
            var itemAlterar = ItemPorId(atestadoItemId);

            if (itemAlterar != null)
            {
                itemAlterar.unidade = atestadoItem.unidade;
                string valorFormatado = atestadoItem.quantidade.Replace(',', '.');
                itemAlterar.quantidade = valorFormatado;
                itemAlterar.AtestadoId = atestadoItem.AtestadoId;
                _context.SaveChanges();
            }
        }

        public void ExcluirItem( string iditem)
        {
            var excluir = ItemPorId(iditem);
           
            _context.AtestadoItem.Remove(excluir);
            _context.SaveChanges();
        }

        public void Incluir(AtestadoItemViewModel itematestado)
        {
            AtestadoItem atestadoItem = new AtestadoItem();
            {

                atestadoItem.unidade=itematestado.AtestadoItem.unidade;
                string valorFormatado = itematestado.AtestadoItem.quantidade.Replace(',', '.');
                atestadoItem.quantidade = valorFormatado;
                atestadoItem.ItensId= itematestado.SelectedItemId;
                atestadoItem.AtestadoId= itematestado.AtestadoId;
                atestadoItem.Item = NomeItem(itematestado.SelectedItemId.ToString()).Item;
                atestadoItem.SubItensId = itematestado.SubItensId;

            }

            _context.AtestadoItem.Add(atestadoItem);
            Listar(itematestado.AtestadoId);
            _context.SaveChanges();

        }

        public AtestadoItem ItemPorId(string iditem)
        {
            return _context.AtestadoItem.FirstOrDefault(f => f.AtestadoItemId.ToString() == iditem);

        }

        public IEnumerable<AtestadoItem> Listar(int atestadoId)
        {

            return _context.AtestadoItem.Where(m => m.AtestadoId == atestadoId).Include(a => a.Itens).Include(b => b.SubItens) ;

        }

        public List<AtestadoItem> ListarUnidade()
        {
            var result = _context.AtestadoItem
                            .GroupBy(item => item.unidade)
                            .Select(group => new
                            {
                                Unidade = group.Key,
                                AtestadoItemId = group.Select(item => item.AtestadoItemId).FirstOrDefault(),
                                Quantidade = group.Select(item => item.quantidade).FirstOrDefault(),
                                Item = group.Select(item => item.Item).FirstOrDefault(),
                                ItensId = group.Select(item => item.ItensId).FirstOrDefault(),
                                AtestadoId = group.Select(item => item.AtestadoId).FirstOrDefault(),
                                SubItensId = group.Select(item => item.SubItensId).FirstOrDefault(),
                            }).ToList();

            List<AtestadoItem> listaUnidade = result.Select(item => new AtestadoItem
            {
                AtestadoItemId = item.AtestadoItemId,
                unidade = item.Unidade,
                quantidade = item.Quantidade,
                Item = item.Item,
                ItensId = item.ItensId,
                AtestadoId = item.AtestadoId,
                SubItensId = item.SubItensId
            }).ToList();

            return listaUnidade;

        }

        public IEnumerable<SubItens> ListarSubItens(int idItem)
        {
            return _context.SubItens.Where(m => m.ItensId == idItem);
        }

        public Itens NomeItem(string iditem)
        {
            return _context.Itens.FirstOrDefault(f => f.ItensId.ToString() == iditem);

        }
    }
}
