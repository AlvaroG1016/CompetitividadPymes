using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompetitividadPymes.Models.Domain;

public partial class PymesCompetitividadContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PymesCompetitividadContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PymesCompetitividadContext(DbContextOptions<PymesCompetitividadContext> options, IConfiguration configuration)
             : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<DocumentoEvidencium> DocumentoEvidencia { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Encuestum> Encuesta { get; set; }

    public virtual DbSet<Factor> Factors { get; set; }

    public virtual DbSet<OrdenPago> OrdenPagos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Suscripcion> Suscripcions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Variable> Variables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("CompetitividadPymes");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentoEvidencium>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PK__Document__5D2EE7E503F5FFDF");

            entity.ToTable("Documento_Evidencia");

            entity.Property(e => e.IdDocumento).HasColumnName("id_documento");
            entity.Property(e => e.Archivo).HasColumnName("archivo");
            entity.Property(e => e.IdEncuesta).HasColumnName("id_encuesta");
            entity.Property(e => e.IdFactor).HasColumnName("id_factor");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(100)
                .HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.DocumentoEvidencia)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Documento__id_en__6754599E");

            entity.HasOne(d => d.IdFactorNavigation).WithMany(p => p.DocumentoEvidencia)
                .HasForeignKey(d => d.IdFactor)
                .HasConstraintName("FK__Documento__id_fa__68487DD7");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__4A0B7E2CA0ED448C");

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.Clasificacion)
                .HasMaxLength(50)
                .HasColumnName("clasificacion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Sector)
                .HasMaxLength(100)
                .HasColumnName("sector");
        });

        modelBuilder.Entity<Encuestum>(entity =>
        {
            entity.HasKey(e => e.IdEncuesta).HasName("PK__Encuesta__013535C336F2E942");

            entity.Property(e => e.IdEncuesta).HasColumnName("id_encuesta");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaAplicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_aplicacion");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.PuntajeTotal)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("puntaje_total");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Encuesta)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Encuesta__id_emp__5629CD9C");
        });

        modelBuilder.Entity<Factor>(entity =>
        {
            entity.HasKey(e => e.IdFactor).HasName("PK__Factor__FEAFF024B3079CD3");

            entity.ToTable("Factor");

            entity.Property(e => e.IdFactor).HasColumnName("id_factor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.DisponibleEnPlanes).HasColumnName("disponible_en_planes");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso");
        });

        modelBuilder.Entity<OrdenPago>(entity =>
        {
            entity.HasKey(e => e.IdOrdenPago).HasName("PK__Orden_Pa__83B3D784E7853E03");

            entity.ToTable("Orden_Pago");

            entity.Property(e => e.IdOrdenPago).HasColumnName("id_orden_pago");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdPlan).HasColumnName("id_plan");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto_total");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.OrdenPagos)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orden_Pag__id_em__49C3F6B7");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.OrdenPagos)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orden_Pago_Plane");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permiso__228F224F7310FC7C");

            entity.ToTable("Permiso");

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.IdPlan).HasName("PK__Plane__3901EAE39522BD96");

            entity.ToTable("Plane");

            entity.Property(e => e.IdPlan).HasColumnName("id_plan");
            entity.Property(e => e.Caracteristicas).HasColumnName("caracteristicas");
            entity.Property(e => e.DuracionMeses).HasColumnName("duracion_meses");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK__Pregunta__6867FFA4A5509A21");

            entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
            entity.Property(e => e.Enunciado)
                .HasMaxLength(500)
                .HasColumnName("enunciado");
            entity.Property(e => e.IdVariable).HasColumnName("id_variable");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso");

            entity.HasOne(d => d.IdVariableNavigation).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.IdVariable)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pregunta__id_var__5FB337D6");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.IdRespuesta).HasName("PK__Respuest__14E55589160BC79F");

            entity.Property(e => e.IdRespuesta).HasColumnName("id_respuesta");
            entity.Property(e => e.IdEncuesta).HasColumnName("id_encuesta");
            entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
            entity.Property(e => e.ValorRespuesta).HasColumnName("valor_respuesta");

            entity.HasOne(d => d.IdEncuestaNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.IdEncuesta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Respuesta__id_en__628FA481");

            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.IdPregunta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Respuesta__id_pr__6383C8BA");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__6ABCB5E02E36850F");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasMany(d => d.IdFactors).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoFactor",
                    r => r.HasOne<Factor>().WithMany()
                        .HasForeignKey("IdFactor")
                        .HasConstraintName("FK__Permiso_F__id_fa__6D0D32F4"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__Permiso_F__id_ro__6C190EBB"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdFactor").HasName("PK__Permiso___35564AE277C415D4");
                        j.ToTable("Permiso_Factor");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                        j.IndexerProperty<int>("IdFactor").HasColumnName("id_factor");
                    });

            entity.HasMany(d => d.IdPermisos).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoRol",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("FK__Permiso_R__id_pe__46E78A0C"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__Permiso_R__id_ro__45F365D3"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("PK__Permiso___889447C493BE42B2");
                        j.ToTable("Permiso_Rol");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                        j.IndexerProperty<int>("IdPermiso").HasColumnName("id_permiso");
                    });
        });

        modelBuilder.Entity<Suscripcion>(entity =>
        {
            entity.HasKey(e => e.IdSuscripcion).HasName("PK__Suscripc__4E8926BB4EE5C716");

            entity.ToTable("Suscripcion");

            entity.Property(e => e.IdSuscripcion).HasColumnName("id_suscripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdOrdenPago).HasColumnName("id_orden_pago");
            entity.Property(e => e.IdPlan).HasColumnName("id_plan");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Suscripcions)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Suscripci__id_em__5070F446");

            entity.HasOne(d => d.IdOrdenPagoNavigation).WithMany(p => p.Suscripcions)
                .HasForeignKey(d => d.IdOrdenPago)
                .HasConstraintName("FK__Suscripci__id_or__534D60F1");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Suscripcions)
                .HasForeignKey(d => d.IdPlan)
                .HasConstraintName("FK__Suscripci__id_pl__5165187F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04ADC528991F");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__2A586E0BB6EC9A03").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UltimoAcceso)
                .HasColumnType("datetime")
                .HasColumnName("ultimo_acceso");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Usuario__id_empr__3E52440B");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__id_rol__412EB0B6");
        });

        modelBuilder.Entity<Variable>(entity =>
        {
            entity.HasKey(e => e.IdVariable).HasName("PK__Variable__99DC6B6499877C10");

            entity.ToTable("Variable");

            entity.Property(e => e.IdVariable).HasColumnName("id_variable");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdFactor).HasColumnName("id_factor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Peso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("peso");

            entity.HasOne(d => d.IdFactorNavigation).WithMany(p => p.Variables)
                .HasForeignKey(d => d.IdFactor)
                .HasConstraintName("FK__Variable__id_fac__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
