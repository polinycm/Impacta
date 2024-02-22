namespace EPT.ViewModels
{
    public class RelatorioAcervoViewModel
    {
        public string NumeroEPT { get; set; }
        public string NumeroAcervo { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public int AtestadoId { get; set; }

        public decimal quantidade { get; set; } 
        public string unidade { get; set; }
        public string Item { get; set; }

        public string SubItem { get; set; }
        public decimal totalQtd { get; set; }   

    
    }
}
