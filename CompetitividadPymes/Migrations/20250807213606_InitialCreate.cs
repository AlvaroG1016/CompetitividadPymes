using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetitividadPymes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    clasificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__4A0B7E2CA0ED448C", x => x.id_empresa);
                });

            migrationBuilder.CreateTable(
                name: "Factor",
                columns: table => new
                {
                    id_factor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    peso = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    disponible_en_planes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factor__FEAFF024B3079CD3", x => x.id_factor);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permiso__228F224F7310FC7C", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    id_plan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    duracion_meses = table.Column<int>(type: "int", nullable: false),
                    caracteristicas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plane__3901EAE39522BD96", x => x.id_plan);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__6ABCB5E02E36850F", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Caracterizacion_Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TiempoMercado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClasificacionEmpresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracterizacion_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caracterizacion_Empresa_Empresa",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa");
                });

            migrationBuilder.CreateTable(
                name: "Caracterizacion_Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Cargo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Antiguedad = table.Column<int>(type: "int", nullable: false),
                    EmailInstitucional = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EmailPersonal = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracterizacion_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caracterizacion_Usuario_Empresa",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa");
                });

            migrationBuilder.CreateTable(
                name: "Variable",
                columns: table => new
                {
                    id_variable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_factor = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    peso = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cantidadpreguntas = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Variable__99DC6B6499877C10", x => x.id_variable);
                    table.ForeignKey(
                        name: "FK__Variable__id_fac__5CD6CB2B",
                        column: x => x.id_factor,
                        principalTable: "Factor",
                        principalColumn: "id_factor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orden_Pago",
                columns: table => new
                {
                    id_orden_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: true),
                    id_plan = table.Column<int>(type: "int", nullable: false),
                    monto_total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    metodo_pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    fecha_pago = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden_Pa__83B3D784E7853E03", x => x.id_orden_pago);
                    table.ForeignKey(
                        name: "FK_Orden_Pago_Plane",
                        column: x => x.id_plan,
                        principalTable: "Plane",
                        principalColumn: "id_plan");
                    table.ForeignKey(
                        name: "FK__Orden_Pag__id_em__49C3F6B7",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permiso_Factor",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    id_factor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permiso___35564AE277C415D4", x => new { x.id_rol, x.id_factor });
                    table.ForeignKey(
                        name: "FK__Permiso_F__id_fa__6D0D32F4",
                        column: x => x.id_factor,
                        principalTable: "Factor",
                        principalColumn: "id_factor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Permiso_F__id_ro__6C190EBB",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permiso_Rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    id_permiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permiso___889447C493BE42B2", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK__Permiso_R__id_pe__46E78A0C",
                        column: x => x.id_permiso,
                        principalTable: "Permiso",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Permiso_R__id_ro__45F365D3",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: true),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultimo_acceso = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__4E3E04ADC528991F", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK__Usuario__id_empr__3E52440B",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Usuario__id_rol__412EB0B6",
                        column: x => x.id_rol,
                        principalTable: "Rol",
                        principalColumn: "id_rol");
                });

            migrationBuilder.CreateTable(
                name: "Encuesta",
                columns: table => new
                {
                    id_encuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: true),
                    fecha_aplicacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    puntaje_total = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    id_carUsuario = table.Column<int>(type: "int", nullable: false),
                    id_carEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Encuesta__013535C336F2E942", x => x.id_encuesta);
                    table.ForeignKey(
                        name: "FK_Encuesta_Caracterizacion_Empresa",
                        column: x => x.id_carEmpresa,
                        principalTable: "Caracterizacion_Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Encuesta_Caracterizacion_Usuario",
                        column: x => x.id_carUsuario,
                        principalTable: "Caracterizacion_Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Encuesta__id_emp__5629CD9C",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    id_variable = table.Column<int>(type: "int", nullable: true),
                    enunciado = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    peso = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Pregunta__id_var__5FB337D6",
                        column: x => x.id_variable,
                        principalTable: "Variable",
                        principalColumn: "id_variable",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suscripcion",
                columns: table => new
                {
                    id_suscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empresa = table.Column<int>(type: "int", nullable: true),
                    id_plan = table.Column<int>(type: "int", nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_orden_pago = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Suscripc__4E8926BB4EE5C716", x => x.id_suscripcion);
                    table.ForeignKey(
                        name: "FK__Suscripci__id_em__5070F446",
                        column: x => x.id_empresa,
                        principalTable: "Empresa",
                        principalColumn: "id_empresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Suscripci__id_or__534D60F1",
                        column: x => x.id_orden_pago,
                        principalTable: "Orden_Pago",
                        principalColumn: "id_orden_pago");
                    table.ForeignKey(
                        name: "FK__Suscripci__id_pl__5165187F",
                        column: x => x.id_plan,
                        principalTable: "Plane",
                        principalColumn: "id_plan");
                });

            migrationBuilder.CreateTable(
                name: "Documento_Evidencia",
                columns: table => new
                {
                    id_documento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_encuesta = table.Column<int>(type: "int", nullable: true),
                    id_factor = table.Column<int>(type: "int", nullable: true),
                    tipo_documento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    archivo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__5D2EE7E503F5FFDF", x => x.id_documento);
                    table.ForeignKey(
                        name: "FK__Documento__id_en__6754599E",
                        column: x => x.id_encuesta,
                        principalTable: "Encuesta",
                        principalColumn: "id_encuesta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Documento__id_fa__68487DD7",
                        column: x => x.id_factor,
                        principalTable: "Factor",
                        principalColumn: "id_factor");
                });

            migrationBuilder.CreateTable(
                name: "ResultadoFactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(type: "int", nullable: false),
                    IdFactor = table.Column<int>(type: "int", nullable: false),
                    PuntajeObtenido = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PuntajeMaximo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PorcentajeFactor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PesoFactor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ContribucionFinal = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CantidadVariables = table.Column<int>(type: "int", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__3214EC077A6D99C6", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Resultado__IdEnc__3587F3E0",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuesta",
                        principalColumn: "id_encuesta");
                    table.ForeignKey(
                        name: "FK__Resultado__IdFac__367C1819",
                        column: x => x.IdFactor,
                        principalTable: "Factor",
                        principalColumn: "id_factor");
                });

            migrationBuilder.CreateTable(
                name: "ResultadoVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(type: "int", nullable: false),
                    IdVariable = table.Column<int>(type: "int", nullable: false),
                    PuntajeObtenido = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PuntajeMaximo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PorcentajeVariable = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PesoVariable = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ContribucionFinal = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaCalculo = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultad__3214EC07B10F3B9D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Resultado__IdEnc__2DE6D218",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuesta",
                        principalColumn: "id_encuesta");
                    table.ForeignKey(
                        name: "FK__Resultado__IdVar__2EDAF651",
                        column: x => x.IdVariable,
                        principalTable: "Variable",
                        principalColumn: "id_variable");
                });

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    id_respuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_encuesta = table.Column<int>(type: "int", nullable: true),
                    id_pregunta = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    valor_respuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Respuest__14E55589160BC79F", x => x.id_respuesta);
                    table.ForeignKey(
                        name: "FK_Respuesta_Pregunta1",
                        column: x => x.id_pregunta,
                        principalTable: "Pregunta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Respuesta__id_en__628FA481",
                        column: x => x.id_encuesta,
                        principalTable: "Encuesta",
                        principalColumn: "id_encuesta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_CaracterizacionEmpresa_IdEmpresa",
                table: "Caracterizacion_Empresa",
                column: "id_empresa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CaracterizacionUsuario_IdEmpresa",
                table: "Caracterizacion_Usuario",
                column: "id_empresa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Evidencia_id_encuesta",
                table: "Documento_Evidencia",
                column: "id_encuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Evidencia_id_factor",
                table: "Documento_Evidencia",
                column: "id_factor");

            migrationBuilder.CreateIndex(
                name: "IX_Encuesta_id_carEmpresa",
                table: "Encuesta",
                column: "id_carEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Encuesta_id_carUsuario",
                table: "Encuesta",
                column: "id_carUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Encuesta_id_empresa",
                table: "Encuesta",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Pago_id_empresa",
                table: "Orden_Pago",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Pago_id_plan",
                table: "Orden_Pago",
                column: "id_plan");

            migrationBuilder.CreateIndex(
                name: "IX_Permiso_Factor_id_factor",
                table: "Permiso_Factor",
                column: "id_factor");

            migrationBuilder.CreateIndex(
                name: "IX_Permiso_Rol_id_permiso",
                table: "Permiso_Rol",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_id_variable",
                table: "Pregunta",
                column: "id_variable");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_id_encuesta",
                table: "Respuesta",
                column: "id_encuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_id_pregunta",
                table: "Respuesta",
                column: "id_pregunta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoFactor_IdEncuesta",
                table: "ResultadoFactor",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoFactor_IdFactor",
                table: "ResultadoFactor",
                column: "IdFactor");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoVariable_IdEncuesta",
                table: "ResultadoVariable",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoVariable_IdVariable",
                table: "ResultadoVariable",
                column: "IdVariable");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcion_id_empresa",
                table: "Suscripcion",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcion_id_orden_pago",
                table: "Suscripcion",
                column: "id_orden_pago");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripcion_id_plan",
                table: "Suscripcion",
                column: "id_plan");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_id_empresa",
                table: "Usuario",
                column: "id_empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_id_rol",
                table: "Usuario",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__2A586E0BB6EC9A03",
                table: "Usuario",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variable_id_factor",
                table: "Variable",
                column: "id_factor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento_Evidencia");

            migrationBuilder.DropTable(
                name: "Permiso_Factor");

            migrationBuilder.DropTable(
                name: "Permiso_Rol");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "ResultadoFactor");

            migrationBuilder.DropTable(
                name: "ResultadoVariable");

            migrationBuilder.DropTable(
                name: "Suscripcion");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Encuesta");

            migrationBuilder.DropTable(
                name: "Orden_Pago");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Variable");

            migrationBuilder.DropTable(
                name: "Caracterizacion_Empresa");

            migrationBuilder.DropTable(
                name: "Caracterizacion_Usuario");

            migrationBuilder.DropTable(
                name: "Plane");

            migrationBuilder.DropTable(
                name: "Factor");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
