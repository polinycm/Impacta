using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPT.Models
{
    public class AtestadoItem
    {
        [Key]
        public int AtestadoItemId { get; set; }

        [Required(ErrorMessage = "A unidade deve ser informada")]

        public string unidade { get; set; }

        [Required(ErrorMessage = "A quantidade deve ser informada")]

        public string quantidade { get; set;}

        public string Item { get; set; }


        [ForeignKey("Item")]
        [Required(ErrorMessage = "O item deve ser informada")]

        public int ItensId { get; set; }
        public Itens Itens { get; set; }

        [ForeignKey("Atestado")]
        public int AtestadoId { get; set; }
        public Atestado Atestado { get; set; }


        [ForeignKey("SubItens")]

        public int SubItensId { get; set; }
        public SubItens SubItens { get; set; }


    }
}
