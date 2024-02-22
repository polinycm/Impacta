using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPT.Models
{
    public class Acervo
    {
        [Key]
        public int AcervoId { get; set; }

        [Required(ErrorMessage = "O numero controle da ept deve ser informado")]
        public string numero_ept { get; set; }

        [Required(ErrorMessage = "O numero do acervo deve ser informado")]
        public string numero_acervo { get; set; }

       

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt_inicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt_fim { get; set; }

        [Display(Name = "Link do acervo")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]

        public string Link { get; set; }

        [ForeignKey("Atestado")]
        public int AtestadoId { get; set; }
        public Atestado Atestado { get; set; }



        [ForeignKey("Profissionais")]
        public int ProfissionaisId { get; set; }
        public Profissionais Profissionais { get; set; }


        [ForeignKey("Funcoes")]
        public int FuncoesId { get; set; }
        public Funcoes Funcoes { get; set; }

       

    }
}
