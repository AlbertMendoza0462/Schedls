using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [NotMapped]
    public class Claves
    {
        public string ClaveActual { get; set; }
        public string ClaveNueva { get; set; }
        public string ClaveConfirmacion { get; set; }
    }
}
