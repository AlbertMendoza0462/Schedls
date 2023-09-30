using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class TipoTurnoBLL
    {
        public Contexto _contexto { get; set; }

        public TipoTurnoBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int tipoTurnoId)
        {
            return await _contexto.TiposTurnos
                .AsNoTracking()
                .AnyAsync(tipoTurno => tipoTurno.TipoTurnoId == tipoTurnoId);
        }

        public async Task<TipoTurno?> Buscar(int tipoTurnoId)
        {
            return await _contexto.TiposTurnos
                .AsNoTracking()
                .FirstOrDefaultAsync(tipoTurno => tipoTurno.TipoTurnoId == tipoTurnoId);
        }

        public async Task<List<TipoTurno>> Listar()
        {
            return await _contexto.TiposTurnos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insertar(TipoTurno tipoTurno)
        {
            _contexto.Add(tipoTurno);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(TipoTurno tipoTurno)
        {
            _contexto.Entry(tipoTurno).State = EntityState.Modified;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Guardar(TipoTurno tipoTurno)
        {
            var existe = await Existe(tipoTurno.TipoTurnoId);
            if (!existe)
            {
                return await Insertar(tipoTurno);
            }
            else
            {
                return await Modificar(tipoTurno);
            }
        }
        public async Task<bool> Eliminar(TipoTurno tipoTurno)
        {
            _contexto.Entry(tipoTurno).State = EntityState.Deleted;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }
    }
}
