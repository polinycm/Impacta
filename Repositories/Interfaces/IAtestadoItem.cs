using EPT.Models;
using EPT.ViewModels;

namespace EPT.Repositories.Interfaces
{
    public interface IAtestadoItem
    {

        void Incluir(AtestadoItemViewModel itematestado);

        IEnumerable<AtestadoItem> Listar(int atestadoId); 
        List<AtestadoItem> ListarUnidade();
        void AlterarItem(AtestadoItem atestadoItem, string atestadoItemId);

        AtestadoItem ItemPorId(string iditem);


        void ExcluirItem(string iditem);

        IEnumerable<SubItens> ListarSubItens(int idItem);



    }
}
