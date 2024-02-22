using EPT.Models;
using EPT.ViewModels;

namespace EPT.Repositories.Interfaces
{
    public interface IRelatorio
    {
        List<RelatorioAcervoViewModel> Acervo();

        List<RelatorioAcervoViewModel> AcervoFiltro1(int idProfissional, int idAtestado, int idFuncao);

        List<RelatorioAcervoViewModel> AtestadoFiltro1(int ItemId, string quantidadeMenor, string quantidadeMaior, int SubItensId, string unidade, int chk);
        List<RelatorioAcervoViewModel> ItensQuantidade(); 
        List<Atestado> AtestadoFiltro2(string objeto, DateTime dt_inicio, DateTime dt_fim, decimal valor, string descricao);

    }
}
