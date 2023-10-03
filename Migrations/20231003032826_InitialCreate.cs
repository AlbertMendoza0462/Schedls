using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedls.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosSolicitudes",
                columns: table => new
                {
                    EstadoSolicitudId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosSolicitudes", x => x.EstadoSolicitudId);
                });

            migrationBuilder.CreateTable(
                name: "TiposTurnos",
                columns: table => new
                {
                    TipoTurnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTurnos", x => x.TipoTurnoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UltimoTokenValido = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    TurnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoTurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantHorasEnDiaDeSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantHorasEnFinDeSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntervaloDeDias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.TurnoId);
                    table.ForeignKey(
                        name: "FK_Turnos_TiposTurnos_TipoTurnoId",
                        column: x => x.TipoTurnoId,
                        principalTable: "TiposTurnos",
                        principalColumn: "TipoTurnoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesCambios",
                columns: table => new
                {
                    SolicitudCambioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TurnoActualId = table.Column<int>(type: "int", nullable: false),
                    TurnoSolicitadoId = table.Column<int>(type: "int", nullable: false),
                    FechaTurnoActual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTurnoSolicitado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoSolicitudId = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesCambios", x => x.SolicitudCambioId);
                    table.ForeignKey(
                        name: "FK_SolicitudesCambios_EstadosSolicitudes_EstadoSolicitudId",
                        column: x => x.EstadoSolicitudId,
                        principalTable: "EstadosSolicitudes",
                        principalColumn: "EstadoSolicitudId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesCambios_Turnos_TurnoActualId",
                        column: x => x.TurnoActualId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId");
                    table.ForeignKey(
                        name: "FK_SolicitudesCambios_Turnos_TurnoSolicitadoId",
                        column: x => x.TurnoSolicitadoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId");
                    table.ForeignKey(
                        name: "FK_SolicitudesCambios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EstadosSolicitudes",
                columns: new[] { "EstadoSolicitudId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "En Proceso" },
                    { 3, "Aprobada" },
                    { 4, "Cancelada por el Empleado" },
                    { 5, "Rechazada" },
                    { 6, "Solicitud Inválida" }
                });

            migrationBuilder.InsertData(
                table: "TiposTurnos",
                columns: new[] { "TipoTurnoId", "Abreviatura", "Descripcion" },
                values: new object[,]
                {
                    { 1, "M", "Mañana" },
                    { 2, "T", "Tarde" },
                    { 3, "N", "Noche" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellido", "Clave", "Correo", "IsAdmin", "Nombre", "UltimoTokenValido" },
                values: new object[,]
                {
                    { 1, "Mendoza", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "albert@gmail.com", false, "Albert", "" },
                    { 2, "Liriano", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "deninson@gmail.com", false, "Deninson", "" },
                    { 3, "Mendez", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "domingo@gmail.com", false, "Domingo", "" },
                    { 4, "Goris", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "frank@gmail.com", false, "Frank", "" },
                    { 5, "Bonifacio", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "danilo@gmail.com", false, "Danilo", "" },
                    { 6, "Castillo", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "eliot@gmail.com", true, "Eliot", "" }
                });

            migrationBuilder.InsertData(
                table: "Turnos",
                columns: new[] { "TurnoId", "CantHorasEnDiaDeSemana", "CantHorasEnFinDeSemana", "FechaInicio", "IntervaloDeDias", "TipoTurnoId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "09:00:00", "08:00:00", new DateTime(2023, 9, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 1 },
                    { 2, "08:00:00", "08:00:00", new DateTime(2023, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 1 },
                    { 3, "08:00:00", "08:00:00", new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 1 },
                    { 4, "08:00:00", "08:00:00", new DateTime(2023, 9, 14, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 5 },
                    { 5, "08:00:00", "08:00:00", new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 5 },
                    { 6, "09:00:00", "08:00:00", new DateTime(2023, 9, 18, 8, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 5 },
                    { 7, "08:00:00", "08:00:00", new DateTime(2023, 9, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 4 },
                    { 8, "09:00:00", "08:00:00", new DateTime(2023, 9, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 4 },
                    { 9, "08:00:00", "08:00:00", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 4 },
                    { 10, "08:00:00", "08:00:00", new DateTime(2023, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 3 },
                    { 11, "09:00:00", "08:00:00", new DateTime(2023, 9, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 3 },
                    { 12, "08:00:00", "08:00:00", new DateTime(2023, 9, 17, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 3 },
                    { 13, "09:00:00", "08:00:00", new DateTime(2023, 9, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 2 },
                    { 14, "08:00:00", "08:00:00", new DateTime(2023, 9, 16, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 2 },
                    { 15, "08:00:00", "08:00:00", new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "SolicitudesCambios",
                columns: new[] { "SolicitudCambioId", "Comentario", "EstadoSolicitudId", "FechaSolicitud", "FechaTurnoActual", "FechaTurnoSolicitado", "Motivo", "TurnoActualId", "TurnoSolicitadoId", "UsuarioId" },
                values: new object[] { 1, "", 1, new DateTime(2023, 9, 11, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 14, 16, 0, 0, 0, DateTimeKind.Unspecified), "Para hacer el curso de inglés.", 1, 4, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCambios_EstadoSolicitudId",
                table: "SolicitudesCambios",
                column: "EstadoSolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCambios_TurnoActualId",
                table: "SolicitudesCambios",
                column: "TurnoActualId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCambios_TurnoSolicitadoId",
                table: "SolicitudesCambios",
                column: "TurnoSolicitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCambios_UsuarioId",
                table: "SolicitudesCambios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_TipoTurnoId",
                table: "Turnos",
                column: "TipoTurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_UsuarioId",
                table: "Turnos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesCambios");

            migrationBuilder.DropTable(
                name: "EstadosSolicitudes");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "TiposTurnos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
