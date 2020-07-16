using System;
using System.ComponentModel.DataAnnotations;

namespace Source.DTOs
{
    public class LogInserirDTO
    {
        /// <summary>
        /// Id gerado automaticamente pela API
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Origem do log
        /// </summary>
        /// <example>127.0.0.1</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} não pode ter mais de {1} caracteres")]
        public string Origem { get; set; }

        /// <summary>
        /// Data do log
        /// </summary>        
        [Required(ErrorMessage = "{0} deve ser informado")]
        public DateTime Data { get; set; }

        /// <summary>
        /// Título do log
        /// </summary>
        /// <example>Stack Overflow</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} não pode ter mais de {1} caracteres")]
        public string Titulo { get; set; }

        /// <summary>
        /// Detalhes do log
        /// </summary>
        /// <example>Unhlandled exception at 0x00435917 in MeuApp.exe</example>
        public string Detalhes { get; set; }

        /// <summary>
        /// Frequëncia do log
        /// </summary>
        /// <example>100</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int Eventos { get; set; }

        /// <summary>
        /// Id do Tipo do Log
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int TipoId { get; set; }

        /// <summary>
        /// Id do Ambiente
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "{0} deve ser informado")]
        public int AmbienteId { get; set; }

    }
}
