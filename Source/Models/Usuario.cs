using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Source.Models
{
    [Table("usuario")]
    public class Usuario
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("senha")]
        [Required]
        [StringLength(50)]
        public string Senha { get; set; }

    }
}
