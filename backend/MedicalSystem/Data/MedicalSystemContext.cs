﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MedicalSystem.Models;

namespace MedicalSystem.Data
{
    public partial class MedicalSystemContext : DbContext
    {
        public MedicalSystemContext()
        {
        }

        public MedicalSystemContext(DbContextOptions<MedicalSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorRating> DoctorRatings { get; set; }
        public virtual DbSet<Other> Others { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Works_in> Works_ins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=medical-system-server.database.windows.net;Initial Catalog=MedicalSystem;Persist Security Info=True;User ID=Team;Password=Password123");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorRating).HasComputedColumnSql("([dbo].[getDoctorRating]([ID]))", false);

                entity.Property(e => e.age).HasComputedColumnSql("(datediff(year,[birthDate],getdate()))", false);

                entity.Property(e => e.birthDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.city).HasDefaultValueSql("('')");

                entity.Property(e => e.gender).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<DoctorRating>(entity =>
            {
                entity.HasKey(e => new { e.PID, e.DID, e.VisitNumber });

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.DoctorRatings)
                    .HasForeignKey(d => d.DID)
                    .HasConstraintName("FK_DoctorRating_Doctor");

                entity.HasOne(d => d.PIDNavigation)
                    .WithMany(p => p.DoctorRatings)
                    .HasForeignKey(d => d.PID)
                    .HasConstraintName("FK_DoctorRating_Patient");
            });

            modelBuilder.Entity<Other>(entity =>
            {
                entity.Property(e => e.age).HasComputedColumnSql("(datediff(year,[birthDate],getdate()))", false);

                entity.Property(e => e.birthDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.city).HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.age).HasComputedColumnSql("(datediff(year,[birthDate],getdate()))", false);

                entity.Property(e => e.birthDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.city).HasDefaultValueSql("('')");

                entity.Property(e => e.gender).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => new { e.DID, e.PID, e.date, e.FNO });

                entity.Property(e => e.prescription).HasDefaultValueSql("('')");

                entity.Property(e => e.summary).HasDefaultValueSql("('')");

                entity.Property(e => e.done).HasDefaultValueSql("0");

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Doctor1");

                entity.HasOne(d => d.OIDNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.OID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Record_Others");

                entity.HasOne(d => d.PIDNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PID)
                    .HasConstraintName("FK_Record_Patient");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => new { e.PID, e.DID, e.appointment_time });

                entity.Property(e => e.AppointmentStatus).HasComputedColumnSql("([dbo].[Appointment_Status]([PID],[DID],[appointment_time]))", false);

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_Doctor");

                entity.HasOne(d => d.PIDNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PID)
                    .HasConstraintName("FK_Visit_Patient");
            });

            modelBuilder.Entity<Works_in>(entity =>
            {
                entity.HasKey(e => new { e.DID, e.start_time })
                    .HasName("PK_Works_in_1");

                entity.Property(e => e.start_time).HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.DIDNavigation)
                    .WithMany(p => p.Works_ins)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Works_in_Doctor1");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}