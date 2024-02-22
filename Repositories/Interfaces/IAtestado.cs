using EPT.Models;
using EPT.ViewModels;

namespace EPT.Repositories.Interfaces
{
    public interface IAtestado
    {
        void Incluir(AtestadoViewModel atestado);
        void Excluir(string idatestado);
        void Alterar(AtestadoViewModel atestado, string atestadoId);

        Atestado AtestadoPorId(string idatestado);
        IEnumerable<Atestado> Listar();
    }
}
