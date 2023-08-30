using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class EmpleadoBLL
    {
        public Contexto _contexto { get; set; }

        public EmpleadoBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool Existe(int empleadoId)
        {
            return _contexto.Empleados
                .Any(empleado => empleado.EmpleadoId == empleadoId);
        }

        public Empleado? Buscar(int empleadoId)
        {
            return _contexto.Empleados
                .Where(empleado => empleado.EmpleadoId == empleadoId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<Empleado> Listar()
        {
            return _contexto.Empleados
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(Empleado empleado)
        {
            _contexto.Add(empleado);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(empleado).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(Empleado empleado)
        {
            _contexto.Entry(empleado).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(empleado).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(Empleado empleado)
        {
            return (!Existe(empleado.EmpleadoId)) ? Insertar(empleado) : Modificar(empleado);
        }
        public bool Eliminar(Empleado empleado)
        {
            _contexto.Entry(empleado).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(empleado).State = EntityState.Detached;
            return guardo;
        }
    }
}
