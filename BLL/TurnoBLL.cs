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

        public bool Existe(int turnoId)
        {
            return _contexto.Turnos
                .Any(turno => turno.TurnoId == turnoId);
        }

        public Turno? Buscar(int turnoId)
        {
            return _contexto.Turnos
                .Where(turno => turno.TurnoId == turnoId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<Turno> Listar()
        {
            return _contexto.Turnos
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(Turno turno)
        {
            _contexto.Add(turno);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(Turno turno)
        {
            _contexto.Entry(turno).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(Turno turno)
        {
            return (!Existe(turno.TurnoId)) ? Insertar(turno) : Modificar(turno);
        }
        public bool Eliminar(Turno turno)
        {
            _contexto.Entry(turno).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(turno).State = EntityState.Detached;
            return guardo;
        }
    }
}
