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
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoId);
                });

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
                name: "Turnos",
                columns: table => new
                {
                    TurnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Asignaciones",
                columns: table => new
                {
                    AsignacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones", x => x.AsignacionId);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolcitudesCambios",
                columns: table => new
                {
                    SolicitudCambioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    TurnoActualId = table.Column<int>(type: "int", nullable: false),
                    TurnoSolicitadoId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoSolicitudId = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolcitudesCambios", x => x.SolicitudCambioId);
                    table.ForeignKey(
                        name: "FK_SolcitudesCambios_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolcitudesCambios_EstadosSolicitudes_EstadoSolicitudId",
                        column: x => x.EstadoSolicitudId,
                        principalTable: "EstadosSolicitudes",
                        principalColumn: "EstadoSolicitudId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolcitudesCambios_Turnos_TurnoActualId",
                        column: x => x.TurnoActualId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId");
                    table.ForeignKey(
                        name: "FK_SolcitudesCambios_Turnos_TurnoSolicitadoId",
                        column: x => x.TurnoSolicitadoId,
                        principalTable: "Turnos",
                        principalColumn: "TurnoId");
                });

            migrationBuilder.InsertData(
                table: "EstadosSolicitudes",
                columns: new[] { "EstadoSolicitudId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Aprobada" },
                    { 2, "Pendiente" },
                    { 3, "En Proceso" },
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

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_EmpleadoId",
                table: "Asignaciones",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_TurnoId",
                table: "Asignaciones",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolcitudesCambios_EmpleadoId",
                table: "SolcitudesCambios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolcitudesCambios_EstadoSolicitudId",
                table: "SolcitudesCambios",
                column: "EstadoSolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolcitudesCambios_TurnoActualId",
                table: "SolcitudesCambios",
                column: "TurnoActualId");

            migrationBuilder.CreateIndex(
                name: "IX_SolcitudesCambios_TurnoSolicitadoId",
                table: "SolcitudesCambios",
                column: "TurnoSolicitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_TipoTurnoId",
                table: "Turnos",
                column: "TipoTurnoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones");

            migrationBuilder.DropTable(
                name: "SolcitudesCambios");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "EstadosSolicitudes");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "TiposTurnos");
        }
    }
}
