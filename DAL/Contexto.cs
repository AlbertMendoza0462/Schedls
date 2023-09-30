using Microsoft.EntityFrameworkCore;
using Schedls.Models;
using Schedls.Utils;

namespace Schedls.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EstadoSolicitud> EstadosSolicitudes { get; set; }
        public DbSet<SolicitudCambio> SolicitudesCambios { get; set; }
        public DbSet<TipoTurno> TiposTurnos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId = 1, Nombre = "Albert", Apellido = "Mendoza", Correo = "albert@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" },
                new Usuario { UsuarioId = 2, Nombre = "Deninson", Apellido = "Liriano", Correo = "deninson@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" },
                new Usuario { UsuarioId = 3, Nombre = "Domingo", Apellido = "Mendez", Correo = "domingo@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" },
                new Usuario { UsuarioId = 4, Nombre = "Frank", Apellido = "Goris", Correo = "frank@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" },
                new Usuario { UsuarioId = 5, Nombre = "Danilo", Apellido = "Bonifacio", Correo = "danilo@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" },
                new Usuario { UsuarioId = 6, Nombre = "Eliot", Apellido = "Castillo", Correo = "eliot@gmail.com", Clave = EncryptSHA256.GetSHA256("1234"), UltimoTokenValido = "" }
                );

            modelBuilder.Entity<Turno>().HasData(
                new Turno { TurnoId = 1, UsuarioId = 1, TipoTurnoId = 1, FechaInicio = new DateTime(2023, 09, 14, 08, 00, 00), CantHorasEnDiaDeSemana = "09:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 2, UsuarioId = 1, TipoTurnoId = 2, FechaInicio = new DateTime(2023, 09, 15, 16, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 3, UsuarioId = 1, TipoTurnoId = 3, FechaInicio = new DateTime(2023, 09, 17, 00, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 4, UsuarioId = 5, TipoTurnoId = 2, FechaInicio = new DateTime(2023, 09, 14, 16, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 5, UsuarioId = 5, TipoTurnoId = 3, FechaInicio = new DateTime(2023, 09, 16, 00, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 6, UsuarioId = 5, TipoTurnoId = 1, FechaInicio = new DateTime(2023, 09, 18, 08, 00, 00), CantHorasEnDiaDeSemana = "09:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 7, UsuarioId = 4, TipoTurnoId = 2, FechaInicio = new DateTime(2023, 09, 18, 16, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 8, UsuarioId = 4, TipoTurnoId = 1, FechaInicio = new DateTime(2023, 09, 17, 08, 00, 00), CantHorasEnDiaDeSemana = "09:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 9, UsuarioId = 4, TipoTurnoId = 3, FechaInicio = new DateTime(2023, 09, 15, 00, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 10, UsuarioId = 3, TipoTurnoId = 3, FechaInicio = new DateTime(2023, 09, 14, 00, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 11, UsuarioId = 3, TipoTurnoId = 1, FechaInicio = new DateTime(2023, 09, 16, 08, 00, 00), CantHorasEnDiaDeSemana = "09:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 12, UsuarioId = 3, TipoTurnoId = 2, FechaInicio = new DateTime(2023, 09, 17, 16, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 13, UsuarioId = 2, TipoTurnoId = 1, FechaInicio = new DateTime(2023, 09, 15, 08, 00, 00), CantHorasEnDiaDeSemana = "09:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 14, UsuarioId = 2, TipoTurnoId = 2, FechaInicio = new DateTime(2023, 09, 16, 16, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 },
                new Turno { TurnoId = 15, UsuarioId = 2, TipoTurnoId = 3, FechaInicio = new DateTime(2023, 09, 18, 00, 00, 00), CantHorasEnDiaDeSemana = "08:00:00", CantHorasEnFinDeSemana = "08:00:00", IntervaloDeDias = 5 }
                );

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

            modelBuilder.Entity<SolicitudCambio>().HasData(
                new SolicitudCambio
                {
                    SolicitudCambioId = 1,
                    UsuarioId = 1,
                    TurnoActualId = 1,
                    TurnoSolicitadoId = 4,
                    FechaTurnoActual = new DateTime(2023, 09, 14, 08, 00, 00),
                    FechaTurnoSolicitado = new DateTime(2023, 09, 14, 16, 00, 00),
                    FechaSolicitud = new DateTime(2023, 09, 11, 16, 00, 00),
                    Motivo = "Para hacer el curso de inglés.",
                    EstadoSolicitudId = 1,
                    Comentario = ""
                });
        }
    }
}
