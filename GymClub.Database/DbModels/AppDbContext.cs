﻿using System;
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

    public virtual DbSet<TblLogEvent> TblLogEvents { get; set; }

    public virtual DbSet<TblLogin> TblLogins { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWorkout> TblWorkouts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblExercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId);

            entity.ToTable("Tbl_Exercise");

            entity.Property(e => e.ExerciseCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Url).IsUnicode(false);
            entity.Property(e => e.WorkoutCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblLogEvent>(entity =>
        {
            entity.ToTable("Tbl_LogEvents");

            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
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

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("Tbl_Payment");

            entity.Property(e => e.PaymentId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PayDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkoutCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkoutName)
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
            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(180)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
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
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.WorkoutCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WorkoutId).ValueGeneratedOnAdd();
            entity.Property(e => e.WorkoutName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
