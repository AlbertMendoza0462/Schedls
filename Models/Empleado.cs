using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpleadoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
    }
}
