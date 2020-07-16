using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Models
{
    [Table("tipolog")]
    public class TipoLog
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("titulo")]
        [StringLength(100)]
        [Required]
        public string Titulo { get; set; }

    }
}
