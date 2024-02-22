using EPT.Context;
using EPT.Models;
using EPT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EPT.Repositories.Repository
{
    public class ItensRepository : IItens
    {
        private readonly AppDbContext _context;
        public ItensRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Alterar(Itens itens, string itemId)
        {
            var itemalterar = ItemPorId(itemId);

            if (itemalterar != null)
            {
                itemalterar.Item = itens.Item;
                _context.SaveChanges();
            }
        }

        public void Excluir(string itemId)
        {
            var excluir = ItemPorId(itemId);
            _context.Itens.Remove(excluir);
            _context.SaveChanges();
        }


        public void Incluir(Itens itens)
        {
            _context.Itens.Add(itens);
            _context.SaveChanges();
        }

        public IEnumerable<Itens> Listar()
        {
            return _context.Itens;
        }

        public Itens ItemPorId(string iditens)
        {
            return _context.Itens.FirstOrDefault(f => f.ItensId.ToString() == iditens);
        }
    }
}
