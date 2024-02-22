using EPT.Models;

namespace EPT.ViewModels
{
    public class RelatorioAcervoDetalheViewModel
    {
        public Atestado atestado { get; set; }
        public List<AtestadoItem> itens { get; set; }

        public List<Itens> item { get; set; }
        public string n_ept { get; set; }
        public string n_acervo { get; set; }
        public string profissional { get; set; }
        public string funcao { get; set; }

        public int AtestadoId { get; set; }
        public int AcervoId { get; set; }
        public DateTime dt_inicio { get; set; }
        public DateTime dt_fim { get; set; }

    }
}
