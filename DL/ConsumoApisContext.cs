using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ConsumoApisContext : DbContext
{
    public ConsumoApisContext()
    {
    }

    public ConsumoApisContext(DbContextOptions<ConsumoApisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Vuelo> Vuelos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= ConsumoApis; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("PK__Aeroline__FE89E8DFAED79257");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.AerolineaNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.Idvuelo).HasName("PK__Vuelo__CC080D33EB98A1E8");

            entity.ToTable("Vuelo");

            entity.Property(e => e.Destino)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraLlegada)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HoraLLegada");
            entity.Property(e => e.HoraSalida)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.IdAerolinea)
                .HasConstraintName("FK__Vuelo__IdAerolin__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
