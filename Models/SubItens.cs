using System.ComponentModel.DataAnnotations.Schema;

namespace EPT.Models
{
    public class SubItens
    {
        public int SubItensId { get; set; }

        public string SubItem { get; set; }

        [ForeignKey("Item")]
        public int ItensId { get; set; }
        public Itens Itens { get; set; }

        public List<AtestadoItem> AtestadoItem { get; set; }
    }
}
