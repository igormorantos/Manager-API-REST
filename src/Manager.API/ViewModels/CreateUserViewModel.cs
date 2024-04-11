using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "o nome não pode ser vazio")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no minimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O Nome deve ter no minimo 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "o email não pode ser vazio")]
        [MinLength(10, ErrorMessage = "O email deve ter no minimo 10 caracteres")]
        [MaxLength(180, ErrorMessage = "O Nome deve ter no minimo 80 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia.")]
        [MinLength(10, ErrorMessage = "A senha deve ter no mínimo 10 caracteres.")]
        [MaxLength(80, ErrorMessage = "A senha deve ter no máximo 80 caracteres.")]
        public string Password { get; set; }
    }
}
