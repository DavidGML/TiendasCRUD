using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WA_CRUD_Tienda.Models;

public partial class TiendasContext : DbContext
{
    public TiendasContext()
    {
    }

    public TiendasContext(DbContextOptions<TiendasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<ArticuloTiendum> ArticuloTienda { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteArticulo> ClienteArticulos { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-J18FQV1T\\SQLEXPRESS; database=tiendas; Trusted_connection=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.CodigoArticulo).HasName("PK__articulo__01435263520B95C5");

            entity.ToTable("articulos");

            entity.Property(e => e.CodigoArticulo).HasColumnName("codigo_articulo");
            entity.Property(e => e.ArticuloOculto)
                .HasDefaultValueSql("((0))")
                .HasColumnName("articulo_oculto");
            entity.Property(e => e.DescripcionArticulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion_articulo");
            entity.Property(e => e.ImagenArticulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen_articulo");
            entity.Property(e => e.PrecioArticulo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_articulo");
            entity.Property(e => e.StockArticulo).HasColumnName("stock_articulo");
        });

        modelBuilder.Entity<ArticuloTiendum>(entity =>
        {
            entity.HasKey(e => new { e.CodigoArticuloAt, e.CodigoSucursalAt }).HasName("PK__articulo__3C9BFCC63F66D57A");

            entity.ToTable("articulo_tienda");

            entity.Property(e => e.CodigoArticuloAt).HasColumnName("codigo_articulo_at");
            entity.Property(e => e.CodigoSucursalAt).HasColumnName("codigo_sucursal_at");
            entity.Property(e => e.FechaAs)
                .HasColumnType("date")
                .HasColumnName("fecha_as");

            entity.HasOne(d => d.CodigoArticuloAtNavigation).WithMany(p => p.ArticuloTienda)
                .HasForeignKey(d => d.CodigoArticuloAt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__articulo___codig__52593CB8");

            entity.HasOne(d => d.CodigoSucursalAtNavigation).WithMany(p => p.ArticuloTienda)
                .HasForeignKey(d => d.CodigoSucursalAt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__articulo___codig__534D60F1");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__clientes__677F38F50D32D2E4");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.ApelllidosCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apelllidos_cliente");
            entity.Property(e => e.ClienteOculto)
                .HasDefaultValueSql("((0))")
                .HasColumnName("cliente_oculto");
            entity.Property(e => e.DireccionCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion_cliente");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
        });

        modelBuilder.Entity<ClienteArticulo>(entity =>
        {
            entity.HasKey(e => new { e.IdClienteCa, e.CodigoArticuloCa }).HasName("PK__cliente___59C25AB06AAAB9F9");

            entity.ToTable("cliente_articulo");

            entity.Property(e => e.IdClienteCa).HasColumnName("id_cliente_ca");
            entity.Property(e => e.CodigoArticuloCa).HasColumnName("codigo_articulo_ca");
            entity.Property(e => e.FechaAc)
                .HasColumnType("date")
                .HasColumnName("fecha_ac");

            entity.HasOne(d => d.CodigoArticuloCaNavigation).WithMany(p => p.ClienteArticulos)
                .HasForeignKey(d => d.CodigoArticuloCa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cliente_a__codig__571DF1D5");

            entity.HasOne(d => d.IdClienteCaNavigation).WithMany(p => p.ClienteArticulos)
                .HasForeignKey(d => d.IdClienteCa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cliente_a__id_cl__5629CD9C");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.CodigoSucursal).HasName("PK__sucursal__2F19A22CC1C1E047");

            entity.ToTable("sucursales");

            entity.Property(e => e.CodigoSucursal).HasColumnName("codigo_sucursal");
            entity.Property(e => e.DireccionSucursal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion_sucursal");
            entity.Property(e => e.SucursalOculta)
                .HasDefaultValueSql("((0))")
                .HasColumnName("sucursal_oculta");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__4E3E04ADEFDE3029");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.EmailUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_usuario");
            entity.Property(e => e.PassUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pass_usuario");
            entity.Property(e => e.RolUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
