using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Models;

public partial class FUMiniHotelContext : DbContext
{
    public FUMiniHotelContext()
    {
    }

    public FUMiniHotelContext(DbContextOptions<FUMiniHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<BookingReservation> BookingReservations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<RoomInformation> RoomInformations { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=MiniHotelManagement;User Id=sa;Password=V@ndiep0210;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasOne(d => d.BookingReservation).WithMany(p => p.BookingDetails).HasConstraintName("FK_BookingDetail_BookingReservation");

            entity.HasOne(d => d.Room).WithMany(p => p.BookingDetails).HasConstraintName("FK_BookingDetail_RoomInformation");
        });

        modelBuilder.Entity<BookingReservation>(entity =>
        {
            entity.Property(e => e.BookingReservationId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingReservations).HasConstraintName("FK_BookingReservation_Customer");
        });

        modelBuilder.Entity<RoomInformation>(entity =>
        {
            entity.HasOne(d => d.RoomType).WithMany(p => p.RoomInformations).HasConstraintName("FK_RoomInformation_RoomType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
