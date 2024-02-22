using EPT.Models;

namespace EPT.ViewModels
{
    public class AtestadoViewModel
    {
        public Atestado atestado { get; set; }

        public List<Contratante> Contratantes { get; set; }

        public Contratante Contratante { get; set; }

        public int SelectedContratanteId { get; set; }
        public string SelectedMoeda { get; set; }

    }
}
