using System.ComponentModel.DataAnnotations;

namespace EPT.Models
{
    public class Contratante
    {
        [Key]
        public int ContratanteId { get; set; }

        [Required(ErrorMessage = "O nome da contratante deve ser informado")]
        [Display(Name = "Nome da contratante")]
        [StringLength(100)]
        public string contratante { get; set; }

        public List<Atestado> Atestado { get; set; }

    }
}
