using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cafeteria_joy.Models;

public partial class JoyContext : DbContext
{
    public JoyContext()
    {
    }

    public JoyContext(DbContextOptions<JoyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Cafeterium> Cafeteria { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.ArticuloId).HasName("PRIMARY");

            entity.ToTable("articulos");

            entity.HasIndex(e => e.Marca, "marca");

            entity.HasIndex(e => e.Proveedor, "proveedor");

            entity.Property(e => e.ArticuloId).HasColumnName("articulo_id");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Existencia).HasColumnName("existencia");
            entity.Property(e => e.Marca).HasColumnName("marca");
            entity.Property(e => e.Proveedor).HasColumnName("proveedor");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.Marca)
                .HasConstraintName("articulos_ibfk_1");

            entity.HasOne(d => d.ProveedorNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.Proveedor)
                .HasConstraintName("articulos_ibfk_2");
        });

        modelBuilder.Entity<Cafeterium>(entity =>
        {
            entity.HasKey(e => e.CafeteriaId).HasName("PRIMARY");

            entity.ToTable("cafeteria");

            entity.HasIndex(e => e.Campus, "campus");

            entity.Property(e => e.CafeteriaId).HasColumnName("cafeteria_id");
            entity.Property(e => e.Campus).HasColumnName("campus");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Encargado).HasColumnName("encargado");
            entity.Property(e => e.Estado).HasColumnName("estado");

            entity.HasOne(d => d.CampusNavigation).WithMany(p => p.Cafeteria)
                .HasForeignKey(d => d.Campus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cafeteria_ibfk_1");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.CampusId).HasName("PRIMARY");

            entity.ToTable("campus");

            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadosId).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.Property(e => e.EmpleadosId).HasColumnName("empleados_id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.PorcientoComision)
                .HasPrecision(10, 2)
                .HasColumnName("porcientoComision");
            entity.Property(e => e.TandaLabor)
                .HasMaxLength(100)
                .HasColumnName("tandaLabor");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcaId).HasName("PRIMARY");

            entity.ToTable("marcas");

            entity.Property(e => e.MarcaId).HasColumnName("marca_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedoresId).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.Property(e => e.ProveedoresId).HasColumnName("proveedores_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(50)
                .HasColumnName("nombreComercial");
            entity.Property(e => e.Rnc)
                .HasMaxLength(11)
                .HasColumnName("rnc");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuariosId).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.TipoUsuario, "tipoUsuario");

            entity.Property(e => e.UsuariosId).HasColumnName("usuarios_id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(13)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro");
            entity.Property(e => e.LimiteCredito)
                .HasPrecision(10, 2)
                .HasColumnName("limiteCredito");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario).HasColumnName("tipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
