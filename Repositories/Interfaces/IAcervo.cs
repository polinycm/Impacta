using EPT.Models;
using EPT.ViewModels;

namespace EPT.Repositories.Interfaces
{
    public interface IAcervo
    {
        void Incluir(AcervoViewModel acervo);
        void Excluir(string idacervo);
        void Alterar(AcervoViewModel acervo, string idacervo);
        Acervo AcervoPorN_Acervo(string n_acervo);
        Acervo AcervoPorId(string idacervo);
        IEnumerable<Acervo> Listar();
    }
}
