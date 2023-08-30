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

        public bool Existe(int tipoTurnoId)
        {
            return _contexto.TiposTurnos
                .Any(tipoTurno => tipoTurno.TipoTurnoId == tipoTurnoId);
        }

        public TipoTurno? Buscar(int tipoTurnoId)
        {
            return _contexto.TiposTurnos
                .Where(tipoTurno => tipoTurno.TipoTurnoId == tipoTurnoId)
                .AsNoTracking()
                .SingleOrDefault();
        }

        public List<TipoTurno> Listar()
        {
            return _contexto.TiposTurnos
                .AsNoTracking()
                .ToList();
        }

        public bool Insertar(TipoTurno tipoTurno)
        {
            _contexto.Add(tipoTurno);
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }

        public bool Modificar(TipoTurno tipoTurno)
        {
            _contexto.Entry(tipoTurno).State = EntityState.Modified;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }

        public bool Guardar(TipoTurno tipoTurno)
        {
            return (!Existe(tipoTurno.TipoTurnoId)) ? Insertar(tipoTurno) : Modificar(tipoTurno);
        }
        public bool Eliminar(TipoTurno tipoTurno)
        {
            _contexto.Entry(tipoTurno).State = EntityState.Deleted;
            var guardo = _contexto.SaveChanges() > 0;
            _contexto.Entry(tipoTurno).State = EntityState.Detached;
            return guardo;
        }
    }
}
