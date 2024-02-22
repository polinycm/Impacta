using EPT.Models;

namespace EPT.Repositories.Interfaces
{
    public interface IItens
    {
        void Incluir(Itens itens);
        void Excluir(string iditens);
        void Alterar(Itens itens, string iditens);
        Itens ItemPorId(string iditens);
        IEnumerable<Itens> Listar();
    }
}
