using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Source.DTOs
{
    public class TipoLogDTO
    {
        /// <summary>
        /// Id gerado automaticamente pela API
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Título descricionário
        /// </summary>
        /// <example>Warning</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} não pode ter mais de {1} caracteres")]
        public string Titulo { get; set; }

    }
}
