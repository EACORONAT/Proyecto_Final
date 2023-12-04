using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcCongresoTIC.Migrations
{
    /// <inheritdoc />
    public partial class PF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conferencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreConferencista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmacionAsistencia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UTwitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocupacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    TYC = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosConferencias",
                columns: table => new
                {
                    ConferenciaId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                        //.Annotation("SqlServer:Identity", "1, 1"),  
                    ConfirmacionAsistencia = table.Column<bool>(type: "bit", nullable: false),
                    ConferenciaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    // Elimina la definición actual de la clave primaria
                    // table.PrimaryKey("PK_RegistrosConferencias", x => new { x.ConferenciaId, x.ParticipanteId });

                    // Opcional: Puedes mantener una clave primaria solo en la columna Id si lo deseas
                    table.PrimaryKey("PK_RegistrosConferencias", x => x.Id);

                    // ... Resto del código de tu migración ...

                    table.ForeignKey(
                        name: "FK_RegistrosConferencias_Conferencia_ConferenciaId",
                        column: x => x.ConferenciaId,
                        principalTable: "Conferencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_RegistrosConferencias_Conferencia_ConferenciaId1",
                        column: x => x.ConferenciaId1,
                        principalTable: "Conferencia",
                        principalColumn: "Id");

                    table.ForeignKey(
                        name: "FK_RegistrosConferencias_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosConferencias_ConferenciaId1",
                table: "RegistrosConferencias",
                column: "ConferenciaId1");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosConferencias_ParticipanteId",
                table: "RegistrosConferencias",
                column: "ParticipanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosConferencias");

            migrationBuilder.DropTable(
                name: "Conferencia");

            migrationBuilder.DropTable(
                name: "Participante");
        }
    }
}
