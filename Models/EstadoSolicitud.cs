using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("EstadosSolicitudes")]
    public class EstadoSolicitud
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoSolicitudId { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
