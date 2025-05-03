using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TabelasBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TipoUsuario = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Upvote = table.Column<int>(type: "integer", nullable: true),
                    Especialidade = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    ProjetoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratanteId = table.Column<long>(type: "bigint", nullable: false),
                    IdPropostaAceita = table.Column<long>(type: "bigint", nullable: true),
                    DataRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Escopo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.ProjetoId);
                    table.ForeignKey(
                        name: "FK_Projetos_Usuarios_ContratanteId",
                        column: x => x.ContratanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Propostas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjetoId = table.Column<long>(type: "bigint", nullable: true),
                    FreelancerId = table.Column<long>(type: "bigint", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataAceite = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DiasUteisDuracao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Propostas_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "ProjetoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propostas_Usuarios_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_ContratanteId",
                table: "Projetos",
                column: "ContratanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_IdPropostaAceita",
                table: "Projetos",
                column: "IdPropostaAceita");

            migrationBuilder.CreateIndex(
                name: "IX_Propostas_FreelancerId",
                table: "Propostas",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Propostas_ProjetoId",
                table: "Propostas",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Propostas_IdPropostaAceita",
                table: "Projetos",
                column: "IdPropostaAceita",
                principalTable: "Propostas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Propostas_IdPropostaAceita",
                table: "Projetos");

            migrationBuilder.DropTable(
                name: "Propostas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
