using EPT.Models;

namespace EPT.ViewModels
{
    public class AtestadoItemViewModel
    {

        public List<Itens> Itens { get; set; }
        public AtestadoItem AtestadoItem { get; set; }

        public int contratanteId { get; set; }
        public int SelectedItemId { get; set; }
        public int SubItensId { get; set; }
        public int AtestadoId { get; set; }

        public IEnumerable<AtestadoItem> listaAtestadoItem { get; set; }

    }
}
