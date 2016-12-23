using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ticket.Data.Migrations
{
    public partial class T : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    DepartamentoId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(nullable: true),
                    DataHoraCad = table.Column<DateTime>(nullable: false),
                    DepartamentoMasterId = table.Column<Guid>(nullable: true),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Sigla = table.Column<string>(nullable: true),
                    UsuarioAlteracao = table.Column<Guid>(nullable: true),
                    UsuarioCad = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.DepartamentoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}
