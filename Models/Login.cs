using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [NotMapped]
    public class Login
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
