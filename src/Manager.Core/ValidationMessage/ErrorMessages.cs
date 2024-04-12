using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core.ValidationMessage
{
    public static class ErrorMessages
    {
        public const string UserAlreadyExists
            = "Já existe um usuário cadastrado com o email informado.";

        public const string UserNotFound
            = "Não existe nenhum usuário com o id informado.";

        public static string UserInvalid(string errors)
            => "Os campos informados para o usuário estão inválidos" + errors;
    }
}
