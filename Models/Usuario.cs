using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EPT.Models
{
    public class Usuario
    {
        public string Id { get; set; }

        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        
    }
}
