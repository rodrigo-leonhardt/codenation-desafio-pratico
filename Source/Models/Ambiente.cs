using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Models
{
    [Table("ambiente")]
    public class Ambiente
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
