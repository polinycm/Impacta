using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPT.Models
{
    public class Itens
    {
        [Key]
        public int ItensId { get; set; }

        [Required(ErrorMessage = "O item deve ser informado")]
        [Display(Name = "Item")]
        public string Item { get; set; }

        public List<AtestadoItem> AtestadoItems { get; set; }

        public List<SubItens> SubItens { get; set; }


       
    }
}
