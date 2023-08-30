using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class EstadoSolicitudBLL
    {
        public Contexto _contexto { get; set; }

        public EstadoSolicitudBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool Existe(int estadoSolicitudId)
        {
            return _contexto.EstadosSolicitudes
                .Any(estadoSolicitud => estadoSolicitud.EstadoSolicitudId == estadoSolicitudId);
        }

        public EstadoSolicitud? Buscar(int estadoSolicitudId)
        {
            return _contexto.EstadosSolicitudes
                .Where(estadoSolicitud => estadoSolicitud.EstadoSolicitudId == estadoSolicitudId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<EstadoSolicitud> Listar()
        {
            return _contexto.EstadosSolicitudes
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Add(estadoSolicitud);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Entry(estadoSolicitud).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(EstadoSolicitud estadoSolicitud)
        {
            return (!Existe(estadoSolicitud.EstadoSolicitudId)) ? Insertar(estadoSolicitud) : Modificar(estadoSolicitud);
        }
        public bool Eliminar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Entry(estadoSolicitud).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }
    }
}
