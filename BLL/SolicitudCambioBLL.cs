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

        public async Task<bool> Existe(int solicitudCambioId)
        {
            return await _contexto.SolicitudesCambios
                .AsNoTracking()
                .AnyAsync(solicitudCambio => solicitudCambio.SolicitudCambioId == solicitudCambioId);
        }

        public async Task<SolicitudCambio?> Buscar(int solicitudCambioId)
        {
            return await _contexto.SolicitudesCambios
                .Include(solicitudCambio => solicitudCambio.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.EstadoSolicitud)
                .Include(solicitudCambio => solicitudCambio.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(solicitudCambio => solicitudCambio.SolicitudCambioId == solicitudCambioId);
        }

        public async Task<List<SolicitudCambio>> Listar()
        {
            return await _contexto.SolicitudesCambios
                .Include(solicitudCambio => solicitudCambio.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.EstadoSolicitud)
                .Include(solicitudCambio => solicitudCambio.Usuario)
                .OrderBy(x => x.EstadoSolicitudId)
                .ThenByDescending(x => x.FechaSolicitud)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<EventoRecurrente>> ListarAprobadas()
        {
            var solicitudes = await _contexto.SolicitudesCambios
                .Where(solicitudCambio => solicitudCambio.EstadoSolicitudId == 3)
                .Include(solicitudCambio => solicitudCambio.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoActual.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.Usuario)
                .Include(solicitudCambio => solicitudCambio.TurnoSolicitado.TipoTurno)
                .Include(solicitudCambio => solicitudCambio.EstadoSolicitud)
                .OrderBy(x => x.EstadoSolicitudId)
                .ThenByDescending(x => x.FechaSolicitud)
                .AsNoTracking()
                .ToListAsync();

            return SolicitudCambio.toEventList(solicitudes);
        }

        public async Task<int> ContarActivas()
        {
            var cantidad = await _contexto.SolicitudesCambios
                .Where(solicitudCambio => solicitudCambio.EstadoSolicitudId == 1 || solicitudCambio.EstadoSolicitudId == 2)
                .CountAsync();

            return cantidad;
        }

        public async Task<bool> Insertar(SolicitudCambio solicitudCambio)
        {
            solicitudCambio.FechaSolicitud = DateTime.Now;

            _contexto.Add(solicitudCambio);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(SolicitudCambio solicitudCambio)
        {
            _contexto.Entry(solicitudCambio).State = EntityState.Modified;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> CambiarEstado(SolicitudCambio sol)
        {
            var solicitudCambio = await Buscar(sol.SolicitudCambioId);

            solicitudCambio.EstadoSolicitudId = sol.EstadoSolicitudId;
            solicitudCambio.Comentario = sol.Comentario;

            _contexto.Entry(solicitudCambio).State = EntityState.Modified;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Guardar(SolicitudCambio solicitudCambio)
        {
            var existe = await Existe(solicitudCambio.SolicitudCambioId);
            if (!existe)
            {
                return await Insertar(solicitudCambio);
            }
            else
            {
                return await Modificar(solicitudCambio);
            }
        }
        public async Task<bool> Eliminar(SolicitudCambio solicitudCambio)
        {
            _contexto.Entry(solicitudCambio).State = EntityState.Deleted;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(solicitudCambio).State = EntityState.Detached;
            return guardo;
        }
    }
}
