using EPT.Models;
using System.ComponentModel.DataAnnotations;

namespace EPT.ViewModels
{
    public class AcervoViewModel
    {
        public List<Funcoes> Funcoes { get; set; }

        public Funcoes Funcao { get; set; }

        public List<Profissionais> Profissionais { get; set; }
        public Profissionais Profissional { get; set; }

        public List<Atestado> Atestado { get; set; }
        public Atestado Atestados { get; set; }

        [Required(ErrorMessage = "Informe o acervo")]

        public string numero_acervo { get; set; }

        [Required(ErrorMessage = "Informe o nº ept")]
        public string numero_ept { get; set; }

        public int SelectedAtestadoId { get; set; }

        public int SelectedProfissionaisId { get; set; }

        public int SelectedFuncoesId { get; set; }
        public int AcervoId { get; set; }

        public DateTime dt_inicio { get; set; }
        public DateTime dt_fim { get; set; }

        public string nome { get; set; }
        public string funcao { get; set; }
        public string Link { get; set; }
       
    }
}
