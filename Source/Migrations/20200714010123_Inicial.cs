using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Source.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ambiente",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ambiente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipolog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipolog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    senha = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arquivado = table.Column<bool>(nullable: false),
                    origem = table.Column<string>(maxLength: 50, nullable: false),
                    data = table.Column<DateTime>(nullable: false),
                    titulo = table.Column<string>(maxLength: 100, nullable: false),
                    detalhes = table.Column<string>(type: "varchar(max)", nullable: true),
                    eventos = table.Column<int>(nullable: false),
                    usuario_id = table.Column<int>(nullable: false),
                    ambiente_id = table.Column<int>(nullable: false),
                    tipo_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_log_ambiente_ambiente_id",
                        column: x => x.ambiente_id,
                        principalTable: "ambiente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_log_tipolog_tipo_id",
                        column: x => x.tipo_id,
                        principalTable: "tipolog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_log_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_log_ambiente_id",
                table: "log",
                column: "ambiente_id");

            migrationBuilder.CreateIndex(
                name: "IX_log_tipo_id",
                table: "log",
                column: "tipo_id");

            migrationBuilder.CreateIndex(
                name: "IX_log_usuario_id",
                table: "log",
                column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "ambiente");

            migrationBuilder.DropTable(
                name: "tipolog");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
