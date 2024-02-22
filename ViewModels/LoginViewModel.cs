using System.ComponentModel.DataAnnotations;

namespace EPT.ViewModels
{
    public class LoginViewModel

    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Informe o nome")]
        [Display(Name ="Usuário")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Informe o senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public string Perfil { get; set; }
    }
}
