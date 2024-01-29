using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingAppv7.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Showing> Showings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1UHI2BJ\\SQLDB;Database=TestDB;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movies__3214EC07EF1D2E40");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Director).HasMaxLength(100);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Summary).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC07E5E0070F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Showing).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Showingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Showi__04E4BC85");
        });

        modelBuilder.Entity<Showing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Showings__3214EC07DBABC8E3");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Datet).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showings)
                .HasForeignKey(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Showings__Moviei__02084FDA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Users__C9F28457D0A2D823");

            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserPassword).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
