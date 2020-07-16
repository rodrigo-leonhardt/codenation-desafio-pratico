using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Source.DTOs
{
    public class UsuarioLoginDTO
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        /// <example>rodrigo_leonhardt@hotmail.com</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>123456</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Senha { get; set; }
    }
}
