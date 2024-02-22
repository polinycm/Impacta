using EPT.Models;

namespace EPT.Repositories.Interfaces
{
    public interface IFuncoes
    {
        void Incluir(Funcoes funcoes);
        void Excluir(string idfuncao);
        void Alterar(Funcoes funcoes,string funcaoId);
        Funcoes FuncaoPorId(string  idfuncao);
        IEnumerable<Funcoes> Listar();
    }
}
