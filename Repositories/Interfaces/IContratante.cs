using EPT.Models;

namespace EPT.Repositories.Interfaces
{
    public interface IContratante
    {
        void Incluir(Contratante contratante);
        void Excluir(string idcontratante);
        void Alterar(Contratante contratante, string idcontratante);

        Contratante ContratantePorId(string idcontratante);
        IEnumerable<Contratante> Listar();
    }
}
