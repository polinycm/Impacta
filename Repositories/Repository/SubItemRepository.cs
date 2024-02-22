using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using EPT.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EPT.Repositories.Repository
{
    public class SubItemRepository:ISubItens
    {
        private readonly AppDbContext _context;

        public SubItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AlterarSubItem(SubItens subitem, string subitemId)
        {
            var subitemalterar = SubItemPorId(subitemId);

            if (subitemalterar != null)
            {
                subitemalterar.SubItem = subitem.SubItem;
                _context.SaveChanges();
            }
        }

        public void ExcluirSubItem(string subitem)
        {
            var excluir = SubItemPorId(subitem);
            _context.SubItens.Remove(excluir);
            _context.SaveChanges();
        }

        public void Incluir(SubItemViewModel subitem)
        {
            SubItens subitens = new SubItens();
            {
                subitens.ItensId = Convert.ToInt32(subitem.ItemId);
                subitens.SubItem=subitem.SubItem;
            }
            _context.SubItens.Add(subitens);
            _context.SaveChanges();
        }

        public IEnumerable<SubItens> Listar(int ItemId)
        {
            return _context.SubItens.Where(s=>s.ItensId == ItemId).ToList();

        }

        public SubItens SubItemPorId(string idsubitem)
        {
            return _context.SubItens.FirstOrDefault(f => f.SubItensId.ToString() == idsubitem);

        }
    }
}
