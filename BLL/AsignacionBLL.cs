using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class AsignacionBLL
    {
        public Contexto _contexto { get; set; }

        public AsignacionBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool Existe(int asignacionId)
        {
            return _contexto.Asignaciones
                .Any(asignacion => asignacion.AsignacionId == asignacionId);
        }

        public Asignacion? Buscar(int asignacionId)
        {
            return _contexto.Asignaciones
                .Where(asignacion => asignacion.AsignacionId == asignacionId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<Asignacion> Listar()
        {
            return _contexto.Asignaciones
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(Asignacion asignacion)
        {
            _contexto.Add(asignacion);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(asignacion).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(Asignacion asignacion)
        {
            _contexto.Entry(asignacion).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(asignacion).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(Asignacion asignacion)
        {
            return (!Existe(asignacion.AsignacionId)) ? Insertar(asignacion) : Modificar(asignacion);
        }
        public bool Eliminar(Asignacion asignacion)
        {
            _contexto.Entry(asignacion).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(asignacion).State = EntityState.Detached;
            return guardo;
        }
    }
}
