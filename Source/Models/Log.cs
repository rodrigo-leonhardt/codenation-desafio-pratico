using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Models
{
    [Table("log")]
    public class Log
    {

        [Key]
        public int Id { get; set; }

        [Column("arquivado")]
        [Required]
        public bool Arquivado { get; set; }

        [Column("origem")]
        [StringLength(50)]
        [Required]
        public string Origem { get; set; }

        [Column("data")]
        [Required]
        public DateTime Data { get; set; }

        [Column("titulo")]
        [StringLength(100)]
        [Required]
        public string Titulo { get; set; }

        [Column("detalhes", TypeName = "varchar(max)")]       
        public string Detalhes { get; set; }

        [Column("eventos")]
        [Required]
        public int Eventos { get; set; }

        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Ambiente")]
        [Column("ambiente_id")]
        [Required]
        public int AmbienteId { get; set; }
        public Ambiente Ambiente { get; set; }

        [ForeignKey("Tipo")]
        [Column("tipo_id")]
        [Required]
        public int TipoId { get; set; }
        public TipoLog Tipo { get; set; }        

    }
    
}
