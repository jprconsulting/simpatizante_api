using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpatizantesapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActiveTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TokenId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveTokens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Casillas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casillas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProgramasSociales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramasSociales", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Promotores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoPaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoMaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreacionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioEdicionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposAgrupacionesPoliticas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAgrupacionesPoliticas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposElecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposElecciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TiposIncidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIncidencias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Distritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distritos_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoAgrupacionPoliticaId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Acronimo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Partidos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidaturas_TiposAgrupacionesPoliticas_TipoAgrupacionPoliti~",
                        column: x => x.TipoAgrupacionPoliticaId,
                        principalTable: "TiposAgrupacionesPoliticas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Municipios_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Municipios_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Combinaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CandidaturaId = table.Column<int>(type: "int", nullable: true),
                    Logo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Partidos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combinaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combinaciones_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comunidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunidades_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secciones_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoPaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoMaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Sobrenombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Emblema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsuarioCreacionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioEdicionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    CargoId = table.Column<int>(type: "int", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    ComunidadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "Comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidatos_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DistribucionesCandidaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoEleccionId = table.Column<int>(type: "int", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    ComunidadId = table.Column<int>(type: "int", nullable: true),
                    Partidos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Coalicion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comun = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Independiente = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribucionesCandidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistribucionesCandidaturas_Comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "Comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesCandidaturas_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesCandidaturas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesCandidaturas_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesCandidaturas_TiposElecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "TiposElecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActasEscrutinios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    CasillaId = table.Column<int>(type: "int", nullable: true),
                    TipoEleccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActasEscrutinios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActasEscrutinios_Casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "Casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActasEscrutinios_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActasEscrutinios_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActasEscrutinios_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActasEscrutinios_TiposElecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "TiposElecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResultadosPreEliminares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoEleccionId = table.Column<int>(type: "int", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    DistritoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    ComunidadId = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    CasillaId = table.Column<int>(type: "int", nullable: true),
                    BoletasSobrantes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonasVotaron = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VotosRepresentantes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Suma = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Partidos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosPreEliminares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "Casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "Comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosPreEliminares_TiposElecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "TiposElecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Retroalimentacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TipoIncidenciaId = table.Column<int>(type: "int", nullable: true),
                    CasillaId = table.Column<int>(type: "int", nullable: true),
                    CandidatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidencias_Casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "Casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidencias_TiposIncidencias_TipoIncidenciaId",
                        column: x => x.TipoIncidenciaId,
                        principalTable: "TiposIncidencias",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoPaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoMaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UsuarioCreacionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioEdicionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CandidatoId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operadores_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operadores_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PropagandasElectorales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comentarios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Ubicacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dimensiones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    CandidatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropagandasElectorales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropagandasElectorales_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropagandasElectorales_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropagandasElectorales_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DistribucionesOrdenadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InputId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Orden = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DistribucionCandidaturaId = table.Column<int>(type: "int", nullable: true),
                    TipoAgrupacionPoliticaId = table.Column<int>(type: "int", nullable: true),
                    CandidaturaId = table.Column<int>(type: "int", nullable: true),
                    CombinacionId = table.Column<int>(type: "int", nullable: true),
                    PadreId = table.Column<int>(type: "int", nullable: false),
                    NombreCandidatura = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistribucionesOrdenadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistribucionesOrdenadas_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesOrdenadas_Combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "Combinaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesOrdenadas_DistribucionesCandidaturas_Distribuc~",
                        column: x => x.DistribucionCandidaturaId,
                        principalTable: "DistribucionesCandidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistribucionesOrdenadas_TiposAgrupacionesPoliticas_TipoAgrup~",
                        column: x => x.TipoAgrupacionPoliticaId,
                        principalTable: "TiposAgrupacionesPoliticas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResultadosCandidaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActaEscrutinioId = table.Column<int>(type: "int", nullable: true),
                    DistribucionCandidaturaId = table.Column<int>(type: "int", nullable: true),
                    CandidaturaId = table.Column<int>(type: "int", nullable: true),
                    CombinacionId = table.Column<int>(type: "int", nullable: true),
                    PadreId = table.Column<int>(type: "int", nullable: false),
                    VotoPreliminar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadosCandidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultadosCandidaturas_ActasEscrutinios_ActaEscrutinioId",
                        column: x => x.ActaEscrutinioId,
                        principalTable: "ActasEscrutinios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosCandidaturas_Candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "Candidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosCandidaturas_Combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "Combinaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultadosCandidaturas_DistribucionesCandidaturas_Distribuci~",
                        column: x => x.DistribucionCandidaturaId,
                        principalTable: "DistribucionesCandidaturas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OperadoresSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OperadorId = table.Column<int>(type: "int", nullable: false),
                    SeccionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperadoresSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperadoresSecciones_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperadoresSecciones_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PromotoresOperadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PromotorId = table.Column<int>(type: "int", nullable: false),
                    OperadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotoresOperadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotoresOperadores_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotoresOperadores_Promotores_PromotorId",
                        column: x => x.PromotorId,
                        principalTable: "Promotores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Simpatizantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoPaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoMaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Domicilio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CURP = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numerotel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClaveElector = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TercerNivelContacto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    UsuarioCreacionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioEdicionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProgramaSocialId = table.Column<int>(type: "int", nullable: true),
                    PromotorId = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    OperadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simpatizantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Simpatizantes_ProgramasSociales_ProgramaSocialId",
                        column: x => x.ProgramaSocialId,
                        principalTable: "ProgramasSociales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Promotores_PromotorId",
                        column: x => x.PromotorId,
                        principalTable: "Promotores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Simpatizantes_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoPaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApellidoMaterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    CandidatoId = table.Column<int>(type: "int", nullable: true),
                    OperadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraVot = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstatusVoto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SimpatizanteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Simpatizantes_SimpatizanteId",
                        column: x => x.SimpatizanteId,
                        principalTable: "Simpatizantes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Servicio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Simpatiza = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaHoraVisita = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SimpatizanteId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_Simpatizantes_SimpatizanteId",
                        column: x => x.SimpatizanteId,
                        principalTable: "Simpatizantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActasEscrutinios_CasillaId",
                table: "ActasEscrutinios",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasEscrutinios_DistritoId",
                table: "ActasEscrutinios",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasEscrutinios_MunicipioId",
                table: "ActasEscrutinios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasEscrutinios_SeccionId",
                table: "ActasEscrutinios",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasEscrutinios_TipoEleccionId",
                table: "ActasEscrutinios",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_CargoId",
                table: "Candidatos",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_ComunidadId",
                table: "Candidatos",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_DistritoId",
                table: "Candidatos",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_EstadoId",
                table: "Candidatos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_GeneroId",
                table: "Candidatos",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_MunicipioId",
                table: "Candidatos",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_TipoAgrupacionPoliticaId",
                table: "Candidaturas",
                column: "TipoAgrupacionPoliticaId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_RolId",
                table: "Claims",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Combinaciones_CandidaturaId",
                table: "Combinaciones",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunidades_MunicipioId",
                table: "Comunidades",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesCandidaturas_ComunidadId",
                table: "DistribucionesCandidaturas",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesCandidaturas_DistritoId",
                table: "DistribucionesCandidaturas",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesCandidaturas_EstadoId",
                table: "DistribucionesCandidaturas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesCandidaturas_MunicipioId",
                table: "DistribucionesCandidaturas",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesCandidaturas_TipoEleccionId",
                table: "DistribucionesCandidaturas",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesOrdenadas_CandidaturaId",
                table: "DistribucionesOrdenadas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesOrdenadas_CombinacionId",
                table: "DistribucionesOrdenadas",
                column: "CombinacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesOrdenadas_DistribucionCandidaturaId",
                table: "DistribucionesOrdenadas",
                column: "DistribucionCandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DistribucionesOrdenadas_TipoAgrupacionPoliticaId",
                table: "DistribucionesOrdenadas",
                column: "TipoAgrupacionPoliticaId");

            migrationBuilder.CreateIndex(
                name: "IX_Distritos_EstadoId",
                table: "Distritos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_CandidatoId",
                table: "Incidencias",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_CasillaId",
                table: "Incidencias",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_TipoIncidenciaId",
                table: "Incidencias",
                column: "TipoIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_DistritoId",
                table: "Municipios",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_EstadoId",
                table: "Municipios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_MunicipioId",
                table: "Municipios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_CandidatoId",
                table: "Operadores",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_MunicipioId",
                table: "Operadores",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_OperadoresSecciones_OperadorId",
                table: "OperadoresSecciones",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_OperadoresSecciones_SeccionId",
                table: "OperadoresSecciones",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotoresOperadores_OperadorId",
                table: "PromotoresOperadores",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotoresOperadores_PromotorId",
                table: "PromotoresOperadores",
                column: "PromotorId");

            migrationBuilder.CreateIndex(
                name: "IX_PropagandasElectorales_CandidatoId",
                table: "PropagandasElectorales",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropagandasElectorales_MunicipioId",
                table: "PropagandasElectorales",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_PropagandasElectorales_SeccionId",
                table: "PropagandasElectorales",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCandidaturas_ActaEscrutinioId",
                table: "ResultadosCandidaturas",
                column: "ActaEscrutinioId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCandidaturas_CandidaturaId",
                table: "ResultadosCandidaturas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCandidaturas_CombinacionId",
                table: "ResultadosCandidaturas",
                column: "CombinacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosCandidaturas_DistribucionCandidaturaId",
                table: "ResultadosCandidaturas",
                column: "DistribucionCandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_CasillaId",
                table: "ResultadosPreEliminares",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_ComunidadId",
                table: "ResultadosPreEliminares",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_DistritoId",
                table: "ResultadosPreEliminares",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_EstadoId",
                table: "ResultadosPreEliminares",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_MunicipioId",
                table: "ResultadosPreEliminares",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_SeccionId",
                table: "ResultadosPreEliminares",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadosPreEliminares_TipoEleccionId",
                table: "ResultadosPreEliminares",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_MunicipioId",
                table: "Secciones",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_EstadoId",
                table: "Simpatizantes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_GeneroId",
                table: "Simpatizantes",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_MunicipioId",
                table: "Simpatizantes",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_OperadorId",
                table: "Simpatizantes",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_ProgramaSocialId",
                table: "Simpatizantes",
                column: "ProgramaSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_PromotorId",
                table: "Simpatizantes",
                column: "PromotorId");

            migrationBuilder.CreateIndex(
                name: "IX_Simpatizantes_SeccionId",
                table: "Simpatizantes",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CandidatoId",
                table: "Usuarios",
                column: "CandidatoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_OperadorId",
                table: "Usuarios",
                column: "OperadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_SimpatizanteId",
                table: "Visitas",
                column: "SimpatizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_UsuarioId",
                table: "Visitas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_SimpatizanteId",
                table: "Votos",
                column: "SimpatizanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveTokens");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "DistribucionesOrdenadas");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "OperadoresSecciones");

            migrationBuilder.DropTable(
                name: "PromotoresOperadores");

            migrationBuilder.DropTable(
                name: "PropagandasElectorales");

            migrationBuilder.DropTable(
                name: "ResultadosCandidaturas");

            migrationBuilder.DropTable(
                name: "ResultadosPreEliminares");

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "TiposIncidencias");

            migrationBuilder.DropTable(
                name: "ActasEscrutinios");

            migrationBuilder.DropTable(
                name: "Combinaciones");

            migrationBuilder.DropTable(
                name: "DistribucionesCandidaturas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Simpatizantes");

            migrationBuilder.DropTable(
                name: "Casillas");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "TiposElecciones");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "Operadores");

            migrationBuilder.DropTable(
                name: "ProgramasSociales");

            migrationBuilder.DropTable(
                name: "Promotores");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "TiposAgrupacionesPoliticas");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Comunidades");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Distritos");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
