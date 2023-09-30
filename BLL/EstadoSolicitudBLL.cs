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

        public async Task<bool> Existe(int estadoSolicitudId)
        {
            return await _contexto.EstadosSolicitudes
                .AsNoTracking()
                .AnyAsync(estadoSolicitud => estadoSolicitud.EstadoSolicitudId == estadoSolicitudId);
        }

        public async Task<EstadoSolicitud?> Buscar(int estadoSolicitudId)
        {
            return await _contexto.EstadosSolicitudes
                .AsNoTracking()
                .FirstOrDefaultAsync(estadoSolicitud => estadoSolicitud.EstadoSolicitudId == estadoSolicitudId);
        }

        public async Task<List<EstadoSolicitud>> Listar()
        {
            return await _contexto.EstadosSolicitudes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insertar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Add(estadoSolicitud);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Entry(estadoSolicitud).State = EntityState.Modified;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Guardar(EstadoSolicitud estadoSolicitud)
        {
            var existe = await Existe(estadoSolicitud.EstadoSolicitudId);
            if (!existe)
            {
                return await Insertar(estadoSolicitud);
            }
            else
            {
                return await Modificar(estadoSolicitud);
            }
        }
        public async Task<bool> Eliminar(EstadoSolicitud estadoSolicitud)
        {
            _contexto.Entry(estadoSolicitud).State = EntityState.Deleted;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(estadoSolicitud).State = EntityState.Detached;
            return guardo;
        }
    }
}
