using System.ComponentModel.DataAnnotations;

namespace Source.DTOs
{
    public class LogDTO : LogInserirDTO
    {       

        /// <summary>
        /// Flag de arquivado
        /// </summary>
        /// <example>1</example>
        [Required]
        public bool Arquivado { get; set; }

        /// <summary>
        /// Id do usuário que registrou o log
        /// </summary>
        /// <example>1</example>
        [Required]
        public int UsuarioId { get; set; }

        /// <summary>
        /// E-mail do usuário que registrou o log
        /// </summary>
        /// <example>1</example>
        [Required]
        public string Usuario { get; set; }

        /// <summary>
        /// Título do ambiente onde o log foi registrado
        /// </summary>
        /// <example>Produção</example>
        [Required]
        public string Ambiente { get; set; }

        /// <summary>
        /// Título do Tipo do Log
        /// </summary>
        /// <example>Error</example>
        [Required]
        public string Tipo { get; set; }
    }
}
