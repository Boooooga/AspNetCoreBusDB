using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.DbModels;

public partial class DefaultDbContext : DbContext
{
    public DefaultDbContext()
    {
    }

    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArrivalTime> ArrivalTimes { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusRoute> BusRoutes { get; set; }

    public virtual DbSet<BusRoutesStop> BusRoutesStops { get; set; }

    public virtual DbSet<Checkup> Checkups { get; set; }

    public virtual DbSet<Conductor> Conductors { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Mechanic> Mechanics { get; set; }

    public virtual DbSet<RouteList> RouteLists { get; set; }

    public virtual DbSet<Stop> Stops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<View2> View2s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BOOOOOGA\\MSSQLSERVER1;Initial Catalog=BusDatabase;Trusted_Connection=True;TrustServerCertificate=True;Integrated security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArrivalTime>(entity =>
        {
            entity.ToTable("ArrivalTime");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdBusRouteStop).HasColumnName("ID_BusRoute_Stop");

            entity.HasOne(d => d.IdBusRouteStopNavigation).WithMany(p => p.ArrivalTimes)
                .HasForeignKey(d => d.IdBusRouteStop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArrivalTime_BusRoutes_Stops");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Bus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BusRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Route");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<BusRoutesStop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BusRoute_Stop");

            entity.ToTable("BusRoutes_Stops");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdRoute).HasColumnName("ID_Route");
            entity.Property(e => e.IdStop).HasColumnName("ID_Stop");

            entity.HasOne(d => d.IdRouteNavigation).WithMany(p => p.BusRoutesStops)
                .HasForeignKey(d => d.IdRoute)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusRoutes_Stops_BusRoute");

            entity.HasOne(d => d.IdStopNavigation).WithMany(p => p.BusRoutesStops)
                .HasForeignKey(d => d.IdStop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusRoutes_Stops_Stop");
        });

        modelBuilder.Entity<Checkup>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CheckupDate).HasColumnType("date");
            entity.Property(e => e.IdBus).HasColumnName("ID_Bus");
            entity.Property(e => e.IdMechanic).HasColumnName("ID_Mechanic");

            entity.HasOne(d => d.IdBusNavigation).WithMany(p => p.Checkups)
                .HasForeignKey(d => d.IdBus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Checkups_Bus");

            entity.HasOne(d => d.IdMechanicNavigation).WithMany(p => p.Checkups)
                .HasForeignKey(d => d.IdMechanic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Checkups_Mechanic");
        });

        modelBuilder.Entity<Conductor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Conductor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Driver");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Mechanic");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RouteList>(entity =>
        {
            entity.ToTable("RouteList");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdBus).HasColumnName("ID_Bus");
            entity.Property(e => e.IdConductor).HasColumnName("ID_Conductor");
            entity.Property(e => e.IdDriver).HasColumnName("ID_Driver");
            entity.Property(e => e.IdRoute).HasColumnName("ID_Route");
            entity.Property(e => e.RouteDate).HasColumnType("date");

            entity.HasOne(d => d.IdBusNavigation).WithMany(p => p.RouteLists)
                .HasForeignKey(d => d.IdBus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteList_Bus");

            entity.HasOne(d => d.IdConductorNavigation).WithMany(p => p.RouteLists)
                .HasForeignKey(d => d.IdConductor)
                .HasConstraintName("FK_RouteList_Conductor");

            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.RouteLists)
                .HasForeignKey(d => d.IdDriver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteList_Driver");

            entity.HasOne(d => d.IdRouteNavigation).WithMany(p => p.RouteLists)
                .HasForeignKey(d => d.IdRoute)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteList_Route");
        });

        modelBuilder.Entity<Stop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Stop");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adress)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Login, "Unique_Users_Login").IsUnique();

            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.Автобус)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Водитель)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ВозрастВодителя).HasColumnName("Возраст водителя");
            entity.Property(e => e.Дата).HasColumnType("date");
            entity.Property(e => e.Кондуктор)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<View2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_2");

            entity.Property(e => e.IdRoute).HasColumnName("ID_Route");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
