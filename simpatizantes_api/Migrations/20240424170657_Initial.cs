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
                name: "activetokens",
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
                    table.PrimaryKey("PK_activetokens", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "casillas",
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
                    table.PrimaryKey("PK_casillas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "programassociales",
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
                    table.PrimaryKey("PK_programassociales", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "promotores",
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
                    table.PrimaryKey("PK_promotores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiposagrupacionespoliticas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposagrupacionespoliticas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiposelecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposelecciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiposincidencias",
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
                    table.PrimaryKey("PK_tiposincidencias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "distritos",
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
                    table.PrimaryKey("PK_distritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distritos_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "claims",
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
                    table.PrimaryKey("PK_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_claims_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "candidaturas",
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
                    table.PrimaryKey("PK_candidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidaturas_tiposagrupacionespoliticas_TipoAgrupacionPoliti~",
                        column: x => x.TipoAgrupacionPoliticaId,
                        principalTable: "tiposagrupacionespoliticas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "municipios",
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
                    table.PrimaryKey("PK_municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_municipios_distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_municipios_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_municipios_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "combinaciones",
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
                    table.PrimaryKey("PK_combinaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_combinaciones_candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "candidaturas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comunidades",
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
                    table.PrimaryKey("PK_comunidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comunidades_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "secciones",
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
                    table.PrimaryKey("PK_secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secciones_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "candidatos",
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
                    table.PrimaryKey("PK_candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidatos_cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "cargos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidatos_comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidatos_distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidatos_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidatos_generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidatos_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "distribucionescandidaturas",
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
                    table.PrimaryKey("PK_distribucionescandidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distribucionescandidaturas_comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionescandidaturas_distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionescandidaturas_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionescandidaturas_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionescandidaturas_tiposelecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "tiposelecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "actasescrutinios",
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
                    table.PrimaryKey("PK_actasescrutinios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actasescrutinios_casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_actasescrutinios_distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_actasescrutinios_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_actasescrutinios_secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_actasescrutinios_tiposelecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "tiposelecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "resultadospreeliminares",
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoRegistrado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VotosNulos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultadospreeliminares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_comunidades_ComunidadId",
                        column: x => x.ComunidadId,
                        principalTable: "comunidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "distritos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadospreeliminares_tiposelecciones_TipoEleccionId",
                        column: x => x.TipoEleccionId,
                        principalTable: "tiposelecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "incidencias",
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
                    table.PrimaryKey("PK_incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_incidencias_candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_incidencias_casillas_CasillaId",
                        column: x => x.CasillaId,
                        principalTable: "casillas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_incidencias_tiposincidencias_TipoIncidenciaId",
                        column: x => x.TipoIncidenciaId,
                        principalTable: "tiposincidencias",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operadores",
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
                    table.PrimaryKey("PK_operadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operadores_candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operadores_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "propagandaselectorales",
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
                    table.PrimaryKey("PK_propagandaselectorales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_propagandaselectorales_candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_propagandaselectorales_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_propagandaselectorales_secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "secciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "distribucionesordenadas",
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
                    table.PrimaryKey("PK_distribucionesordenadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distribucionesordenadas_candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "candidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionesordenadas_combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "combinaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionesordenadas_distribucionescandidaturas_Distribuc~",
                        column: x => x.DistribucionCandidaturaId,
                        principalTable: "distribucionescandidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distribucionesordenadas_tiposagrupacionespoliticas_TipoAgrup~",
                        column: x => x.TipoAgrupacionPoliticaId,
                        principalTable: "tiposagrupacionespoliticas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "resultadoscandidaturas",
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
                    table.PrimaryKey("PK_resultadoscandidaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resultadoscandidaturas_actasescrutinios_ActaEscrutinioId",
                        column: x => x.ActaEscrutinioId,
                        principalTable: "actasescrutinios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadoscandidaturas_candidaturas_CandidaturaId",
                        column: x => x.CandidaturaId,
                        principalTable: "candidaturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadoscandidaturas_combinaciones_CombinacionId",
                        column: x => x.CombinacionId,
                        principalTable: "combinaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_resultadoscandidaturas_distribucionescandidaturas_Distribuci~",
                        column: x => x.DistribucionCandidaturaId,
                        principalTable: "distribucionescandidaturas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operadoressecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OperadorId = table.Column<int>(type: "int", nullable: false),
                    SeccionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operadoressecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operadoressecciones_operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_operadoressecciones_secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "promotoresoperadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PromotorId = table.Column<int>(type: "int", nullable: false),
                    OperadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotoresoperadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_promotoresoperadores_operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promotoresoperadores_promotores_PromotorId",
                        column: x => x.PromotorId,
                        principalTable: "promotores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "simpatizantes",
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
                    Latitud = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Longitud = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Estatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClaveElector = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TercerNivelContacto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreacionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioEdicionNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHoraEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    ProgramaSocialId = table.Column<int>(type: "int", nullable: true),
                    PromotorId = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    OperadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simpatizantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simpatizantes_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "operadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_programassociales_ProgramaSocialId",
                        column: x => x.ProgramaSocialId,
                        principalTable: "programassociales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_promotores_PromotorId",
                        column: x => x.PromotorId,
                        principalTable: "promotores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simpatizantes_secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "secciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
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
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuarios_operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "operadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_usuarios_rols_RolId",
                        column: x => x.RolId,
                        principalTable: "rols",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "votos",
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
                    table.PrimaryKey("PK_votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_votos_simpatizantes_SimpatizanteId",
                        column: x => x.SimpatizanteId,
                        principalTable: "simpatizantes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "visitas",
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
                    table.PrimaryKey("PK_visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visitas_simpatizantes_SimpatizanteId",
                        column: x => x.SimpatizanteId,
                        principalTable: "simpatizantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_visitas_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_actasescrutinios_CasillaId",
                table: "actasescrutinios",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_actasescrutinios_DistritoId",
                table: "actasescrutinios",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_actasescrutinios_MunicipioId",
                table: "actasescrutinios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_actasescrutinios_SeccionId",
                table: "actasescrutinios",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_actasescrutinios_TipoEleccionId",
                table: "actasescrutinios",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_CargoId",
                table: "candidatos",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_ComunidadId",
                table: "candidatos",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_DistritoId",
                table: "candidatos",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_EstadoId",
                table: "candidatos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_GeneroId",
                table: "candidatos",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_candidatos_MunicipioId",
                table: "candidatos",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_candidaturas_TipoAgrupacionPoliticaId",
                table: "candidaturas",
                column: "TipoAgrupacionPoliticaId");

            migrationBuilder.CreateIndex(
                name: "IX_claims_RolId",
                table: "claims",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_combinaciones_CandidaturaId",
                table: "combinaciones",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_comunidades_MunicipioId",
                table: "comunidades",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionescandidaturas_ComunidadId",
                table: "distribucionescandidaturas",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionescandidaturas_DistritoId",
                table: "distribucionescandidaturas",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionescandidaturas_EstadoId",
                table: "distribucionescandidaturas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionescandidaturas_MunicipioId",
                table: "distribucionescandidaturas",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionescandidaturas_TipoEleccionId",
                table: "distribucionescandidaturas",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionesordenadas_CandidaturaId",
                table: "distribucionesordenadas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionesordenadas_CombinacionId",
                table: "distribucionesordenadas",
                column: "CombinacionId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionesordenadas_DistribucionCandidaturaId",
                table: "distribucionesordenadas",
                column: "DistribucionCandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_distribucionesordenadas_TipoAgrupacionPoliticaId",
                table: "distribucionesordenadas",
                column: "TipoAgrupacionPoliticaId");

            migrationBuilder.CreateIndex(
                name: "IX_distritos_EstadoId",
                table: "distritos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_incidencias_CandidatoId",
                table: "incidencias",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_incidencias_CasillaId",
                table: "incidencias",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_incidencias_TipoIncidenciaId",
                table: "incidencias",
                column: "TipoIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_DistritoId",
                table: "municipios",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_EstadoId",
                table: "municipios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_MunicipioId",
                table: "municipios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_operadores_CandidatoId",
                table: "operadores",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_operadores_MunicipioId",
                table: "operadores",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_operadoressecciones_OperadorId",
                table: "operadoressecciones",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_operadoressecciones_SeccionId",
                table: "operadoressecciones",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_promotoresoperadores_OperadorId",
                table: "promotoresoperadores",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_promotoresoperadores_PromotorId",
                table: "promotoresoperadores",
                column: "PromotorId");

            migrationBuilder.CreateIndex(
                name: "IX_propagandaselectorales_CandidatoId",
                table: "propagandaselectorales",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_propagandaselectorales_MunicipioId",
                table: "propagandaselectorales",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_propagandaselectorales_SeccionId",
                table: "propagandaselectorales",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadoscandidaturas_ActaEscrutinioId",
                table: "resultadoscandidaturas",
                column: "ActaEscrutinioId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadoscandidaturas_CandidaturaId",
                table: "resultadoscandidaturas",
                column: "CandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadoscandidaturas_CombinacionId",
                table: "resultadoscandidaturas",
                column: "CombinacionId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadoscandidaturas_DistribucionCandidaturaId",
                table: "resultadoscandidaturas",
                column: "DistribucionCandidaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_CasillaId",
                table: "resultadospreeliminares",
                column: "CasillaId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_ComunidadId",
                table: "resultadospreeliminares",
                column: "ComunidadId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_DistritoId",
                table: "resultadospreeliminares",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_EstadoId",
                table: "resultadospreeliminares",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_MunicipioId",
                table: "resultadospreeliminares",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_SeccionId",
                table: "resultadospreeliminares",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_resultadospreeliminares_TipoEleccionId",
                table: "resultadospreeliminares",
                column: "TipoEleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_secciones_MunicipioId",
                table: "secciones",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_EstadoId",
                table: "simpatizantes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_GeneroId",
                table: "simpatizantes",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_MunicipioId",
                table: "simpatizantes",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_OperadorId",
                table: "simpatizantes",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_ProgramaSocialId",
                table: "simpatizantes",
                column: "ProgramaSocialId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_PromotorId",
                table: "simpatizantes",
                column: "PromotorId");

            migrationBuilder.CreateIndex(
                name: "IX_simpatizantes_SeccionId",
                table: "simpatizantes",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_CandidatoId",
                table: "usuarios",
                column: "CandidatoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_OperadorId",
                table: "usuarios",
                column: "OperadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_RolId",
                table: "usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_visitas_SimpatizanteId",
                table: "visitas",
                column: "SimpatizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_visitas_UsuarioId",
                table: "visitas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_votos_SimpatizanteId",
                table: "votos",
                column: "SimpatizanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activetokens");

            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "distribucionesordenadas");

            migrationBuilder.DropTable(
                name: "incidencias");

            migrationBuilder.DropTable(
                name: "operadoressecciones");

            migrationBuilder.DropTable(
                name: "promotoresoperadores");

            migrationBuilder.DropTable(
                name: "propagandaselectorales");

            migrationBuilder.DropTable(
                name: "resultadoscandidaturas");

            migrationBuilder.DropTable(
                name: "resultadospreeliminares");

            migrationBuilder.DropTable(
                name: "visitas");

            migrationBuilder.DropTable(
                name: "votos");

            migrationBuilder.DropTable(
                name: "tiposincidencias");

            migrationBuilder.DropTable(
                name: "actasescrutinios");

            migrationBuilder.DropTable(
                name: "combinaciones");

            migrationBuilder.DropTable(
                name: "distribucionescandidaturas");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "simpatizantes");

            migrationBuilder.DropTable(
                name: "casillas");

            migrationBuilder.DropTable(
                name: "candidaturas");

            migrationBuilder.DropTable(
                name: "tiposelecciones");

            migrationBuilder.DropTable(
                name: "rols");

            migrationBuilder.DropTable(
                name: "operadores");

            migrationBuilder.DropTable(
                name: "programassociales");

            migrationBuilder.DropTable(
                name: "promotores");

            migrationBuilder.DropTable(
                name: "secciones");

            migrationBuilder.DropTable(
                name: "tiposagrupacionespoliticas");

            migrationBuilder.DropTable(
                name: "candidatos");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "comunidades");

            migrationBuilder.DropTable(
                name: "generos");

            migrationBuilder.DropTable(
                name: "municipios");

            migrationBuilder.DropTable(
                name: "distritos");

            migrationBuilder.DropTable(
                name: "estados");
        }
    }
}
