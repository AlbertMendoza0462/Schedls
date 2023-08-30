using Microsoft.EntityFrameworkCore;
using Schedls.Models;

namespace Schedls.DAL
{
    public class Contexto: DbContext
    {
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EstadoSolicitud> EstadosSolicitudes { get; set; }
        public DbSet<SolicitudCambio> SolicitudesCambios { get; set; }
        public DbSet<TipoTurno> TiposTurnos { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SolicitudCambio>()
                .HasOne(e => e.TurnoActual)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SolicitudCambio>()
                .HasOne(e => e.TurnoSolicitado)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TipoTurno>().HasData(
                new TipoTurno
                {
                    TipoTurnoId = 1,
                    Descripcion = "Mañana",
                    Abreviatura = "M"
                },
                new TipoTurno
                {
                    TipoTurnoId = 2,
                    Descripcion = "Tarde",
                    Abreviatura = "T"
                },
                new TipoTurno
                {
                    TipoTurnoId = 3,
                    Descripcion = "Noche",
                    Abreviatura = "N"
                }
                );

            modelBuilder.Entity<EstadoSolicitud>().HasData(
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 1,
                    Descripcion = "Aprobada"
                },
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 2,
                    Descripcion = "Pendiente"
                },
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 3,
                    Descripcion = "En Proceso"
                },
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 4,
                    Descripcion = "Cancelada por el Empleado"
                },
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 5,
                    Descripcion = "Rechazada"
                },
                new EstadoSolicitud
                {
                    EstadoSolicitudId = 6,
                    Descripcion = "Solicitud Inválida"
                }
                );
        }
    }
}
