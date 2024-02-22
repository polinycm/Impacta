using EPT.Models;

namespace EPT.Repositories.Interfaces
{
    public interface IProfissionais
    {
        void Incluir(Profissionais profissional);
        void Excluir(string idprofissional);
        void Alterar(Profissionais profissionais, string idprofissional);
        Profissionais ProfissionalPorId(string idprofissional);
        IEnumerable<Profissionais> Listar();
    }
}
