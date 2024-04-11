using Manager.API.ViewModels;
using System;
using System.Collections.Generic;

namespace Manager.API.Utils
{
    public static class Responses
    {
        public static ResultViewModel ApplicationErrorMessage(Exception ex)
        {
            return new ResultViewModel
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." + ex.Message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "A combinação de login e senha está incorreta!",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel InternalServerErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Ocorreu um erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }
    }
}
