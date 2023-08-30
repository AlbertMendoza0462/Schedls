using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class SolicitudCambioBLL
    {
        public Contexto _contexto { get; set; }

        public SolicitudCambioBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public bool Existe(int solicitudCambioId)
        {
            return _contexto.SolicitudesCambios
                .Any(solicitudCambio => solicitudCambio.SolicitudCambioId == solicitudCambioId);
        }

        public SolicitudCambio? Buscar(int solicitudCambioId)
        {
            return _contexto.SolicitudesCambios
                .Where(solicitudCambio => solicitudCambio.SolicitudCambioId == solicitudCambioId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<SolicitudCambio> Listar()
        {
            return _contexto.SolicitudesCambios
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(SolicitudCambio solicitudCambio)
        {
            _contexto.Add(solicitudCambio);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(SolicitudCambio solicitudCambio)
        {
            _contexto.Entry(solicitudCambio).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(SolicitudCambio solicitudCambio)
        {
            return (!Existe(solicitudCambio.SolicitudCambioId)) ? Insertar(solicitudCambio) : Modificar(solicitudCambio);
        }
        public bool Eliminar(SolicitudCambio solicitudCambio)
        {
            _contexto.Entry(solicitudCambio).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }
    }
}
