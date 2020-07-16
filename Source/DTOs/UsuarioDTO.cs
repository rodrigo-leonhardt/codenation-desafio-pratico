using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Source.DTOs
{
    public class UsuarioDTO
    {

        /// <summary>
        /// Id gerado automaticamente pela API
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// E-mail do usuário
        /// </summary>
        /// <example>rodrigo@mail.com.br</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} não pode ter mais de {1} caracteres")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>123456</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} deve tern entre {2} e {1} caracteres")]
        public string Senha { get; set; }

    }
}
