using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login Não pode ser vazio")]
        
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha Não pode ser vazia")]
        public string Password { get; set; }
    }
}
