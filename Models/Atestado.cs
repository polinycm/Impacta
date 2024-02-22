using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPT.Models
{
    public class Atestado
    {
        [Key]
        public int AtestadoId { get; set; }

        public string n_atestado { get; set; }

        [Required(ErrorMessage = "O objeto do atestado deve ser informado")]
        [Display(Name = "Objeto")]
        [StringLength(800, MinimumLength = 1, ErrorMessage = "O {0} deve ter no máximo {2} caracteres")]
        public string objeto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt_inicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt_fim { get; set; }

        [Display(Name = "Link do atestado")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Link { get; set; }

        public string valor { get;set; }

        [Required(ErrorMessage = "A moeda deve ser informada")]
        [MaxLength(20)]
        public string moeda { get; set; }

        public string descricao { get; set; }

        public List<Acervo> Acervos { get; set; }

        public List<AtestadoItem> AtestadoItems { get; set; }


        [ForeignKey("Contratante")]
        public int ContratanteId { get; set; }
        public Contratante Contratante { get; set; }



    }
}
