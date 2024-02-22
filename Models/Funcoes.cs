using System.ComponentModel.DataAnnotations;

namespace EPT.Models
{
    public class Funcoes
    {
        [Key]
        public int FuncoesId { get; set; }

        [Required(ErrorMessage = "O nome da função deve ser informado")]
        [Display(Name = "Nome da função")]
        public string Funcao { get; set; }

        public List<Acervo> Acervos { get; set; }

    }
}
