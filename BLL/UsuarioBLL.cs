using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;
using Schedls.Utils;

namespace Schedls.BLL
{
    public class UsuarioBLL
    {
        public Contexto _contexto { get; set; }

        public UsuarioBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int usuarioId)
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .AnyAsync(usuario => usuario.UsuarioId == usuarioId);
        }

        private async Task<Usuario?> BuscarUsuario(int usuarioId)
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => usuario.UsuarioId == usuarioId);
        }

        public async Task<Empleado?> BuscarEmpleado(int usuarioId)
        {
            var usuario = await _contexto.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => usuario.UsuarioId == usuarioId);
            return (usuario != null) ? (Empleado)usuario : null;
        }

        public async Task<Empleado?> BuscarPorCorreoYClave(string correo, string clave)
        {
            var usuario = await _contexto.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => (usuario.Correo == correo && usuario.Clave == EncryptSHA256.GetSHA256(clave)));
            return (usuario != null) ? (Empleado)usuario : null;
        }

        public async Task<List<Empleado>> Listar()
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .Select(usuario => (Empleado)usuario)
                .ToListAsync();
        }

        public async Task<bool> Insertar(Usuario usuario)
        {
            usuario.Clave = EncryptSHA256.GetSHA256(usuario.Clave);
            usuario.UltimoTokenValido = "";
            usuario.Clave = EncryptSHA256.GetSHA256("1234");

            _contexto.Add(usuario);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(usuario).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(Empleado empleado)
        {
            var usuario = await BuscarUsuario(empleado.EmpleadoId);
            if (usuario != null)
            {
                usuario.Nombre = empleado.Nombre;
                usuario.Apellido = empleado.Apellido;
                usuario.Correo = empleado.Correo;
                usuario.IsAdmin = empleado.IsAdmin;

                _contexto.Entry(usuario).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(usuario).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> GuardarSesion(Empleado empleado, string token)
        {
            var usuario = await BuscarUsuario(empleado.EmpleadoId);
            if (usuario != null)
            {
                usuario.UltimoTokenValido = token;

                _contexto.Entry(usuario).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(usuario).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<string> ObtenerSesion(Empleado empleado)
        {
            var usuario = await BuscarUsuario(empleado.EmpleadoId);
            if (usuario != null)
            {
                return usuario.UltimoTokenValido;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> EliminarSesion(Empleado empleado)
        {
            var usuario = await BuscarUsuario(empleado.EmpleadoId);
            if (usuario != null)
            {
                usuario.UltimoTokenValido = "";

                _contexto.Entry(usuario).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(usuario).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> CambiarClave(Usuario usuario, string claveActual)
        {
            var usuarioActual = await BuscarUsuario(usuario.UsuarioId);

            if (usuarioActual != null)
            {
                if (EncryptSHA256.GetSHA256(claveActual) != usuarioActual.Clave)
                {
                    throw new Exception($"La clave actual es incorrecta.");
                }

                usuarioActual.Clave = EncryptSHA256.GetSHA256(usuario.Clave);

                _contexto.Entry(usuarioActual).State = EntityState.Modified;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(usuarioActual).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }

        public async Task<bool> Guardar(Usuario usuario)
        {
            var existe = await Existe(usuario.UsuarioId);
            if (!existe)
            {
                return await Insertar(usuario);
            }
            else
            {
                return await Modificar((Empleado)usuario);
            }
        }
        public async Task<bool> Eliminar(Empleado empleado)
        {
            var usuario = await BuscarUsuario(empleado.EmpleadoId);
            if (usuario != null)
            {
                _contexto.Entry(usuario).State = EntityState.Deleted;
                var guardo = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(usuario).State = EntityState.Detached;
                return guardo;
            }
            else
            {
                throw new Exception("El usuario no existe en la base de datos.");
            }
        }
    }
}
