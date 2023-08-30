using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("TiposTurnos")]
    public class TipoTurno
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoTurnoId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Abreviatura { get; set; }
    }
}
