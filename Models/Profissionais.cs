using System.ComponentModel.DataAnnotations;

namespace EPT.Models
{
    public class Profissionais
    {
        [Key]
        public int ProfissionaisId { get; set; }

        [Required(ErrorMessage = "O nome do profissional deve ser informado")]
        [Display(Name = "Nome do profissional")]
        public string Nome { get; set; }

        public List<Acervo> Acervos { get; set; }
    }
}
