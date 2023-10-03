using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [NotMapped]
    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public bool IsAdmin { get; set; }

        public static explicit operator Empleado(Usuario usuario)
        {
            return new Empleado
            {
                EmpleadoId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                IsAdmin = usuario.IsAdmin
            };
        }
    }
}
