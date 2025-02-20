using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GymClub.Database.DbModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblExercise> TblExercises { get; set; }

    public virtual DbSet<TblLogin> TblLogins { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWorkout> TblWorkouts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblExercise>(entity =>
        {
            entity.HasKey(e => e.Eid);

            entity.ToTable("Tbl_Exercise");

            entity.Property(e => e.Eid).HasColumnName("EId");
            entity.Property(e => e.EName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("E_Name");
            entity.Property(e => e.Wid).HasColumnName("WId");
        });

        modelBuilder.Entity<TblLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Login");

            entity.Property(e => e.LoginId).ValueGeneratedOnAdd();
            entity.Property(e => e.SessionExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.SessionId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("Tbl_User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblWorkout>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Workout");

            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Place)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Wid)
                .ValueGeneratedOnAdd()
                .HasColumnName("WId");
            entity.Property(e => e.WorkoutName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
