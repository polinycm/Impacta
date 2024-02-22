using EPT.Models;
using EPT.ViewModels;

namespace EPT.Repositories.Interfaces
{
    public interface ISubItens
    {
        void Incluir(SubItemViewModel subitem);

        IEnumerable<SubItens> Listar(int ItemId);

        void AlterarSubItem(SubItens subitem, string subitemId);

        void ExcluirSubItem(string subitem);
        SubItens SubItemPorId(string idsubitem);


    }
}
