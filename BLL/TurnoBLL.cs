using Microsoft.EntityFrameworkCore;
using Schedls.DAL;
using Schedls.Models;

namespace Schedls.BLL
{
    public class TurnoBLL
    {
        public Contexto _contexto { get; set; }

        public TurnoBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Existe(int turnoId)
        {
            return await _contexto.Turnos
                .AsNoTracking()
                .AnyAsync(turno => turno.TurnoId == turnoId);
        }

        public async Task<Turno?> Buscar(int turnoId)
        {
            return await _contexto.Turnos
                .Include(turno => turno.TipoTurno)
                .Include(turno => turno.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(turno => turno.TurnoId == turnoId);
        }

        public async Task<List<Turno>> Listar()
        {
            return await _contexto.Turnos
                .Include(turno => turno.TipoTurno)
                .Include(turno => turno.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<EventoRecurrente>> ListarEventos(List<EventoRecurrente> aprobadas)
        {
            var turnos = await _contexto.Turnos
                .Include(turno => turno.TipoTurno)
                .Include(turno => turno.Usuario)
                .Select(turno => (EventoRecurrente)turno)
                .AsNoTracking()
                .ToListAsync();

            turnos.ForEach((turno) =>
            {
                var encontradas = aprobadas.FindAll((e) => e.Id == turno.Id);
                if (encontradas != null)
                {
                    encontradas.ForEach((encontrada) =>
                    {
                        turno.Exdate.Add(encontrada.rrule.Dtstart);

                    });
                }
            });

            return turnos;
        }

        public async Task<bool> Insertar(Turno turno)
        {
            _contexto.Add(turno);
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Modificar(Turno turno)
        {
            _contexto.Entry(turno).State = EntityState.Modified;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }

        public async Task<bool> Guardar(Turno turno)
        {
            var existe = await Existe(turno.TurnoId);
            if (!existe)
            {
                return await Insertar(turno);
            }
            else
            {
                return await Modificar(turno);
            }
        }
        public async Task<bool> Eliminar(Turno turno)
        {
            _contexto.Entry(turno).State = EntityState.Deleted;
            var guardo = await _contexto.SaveChangesAsync() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }
    }
}
