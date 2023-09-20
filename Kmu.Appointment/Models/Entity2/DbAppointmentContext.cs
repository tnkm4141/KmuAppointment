using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Kmu.Appointment.Models.Entity2
{
    public partial class DbAppointmentContext : DbContext
    {
        public DbAppointmentContext()
        {
        }

        public DbAppointmentContext(DbContextOptions<DbAppointmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientDiagnosis> PatientDiagnoses { get; set; }
        public virtual DbSet<Polyclinic> Polyclinics { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Xappointment> Xappointments { get; set; }
        public virtual DbSet<Xdoctor> Xdoctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-GML3BQ0N\\SQLEXPRESS2;Database=DbAppointment;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.PatientNavigation)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Patient)
                    .HasConstraintName("FK_Appointment_Patient");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.NameSurname).HasMaxLength(20);

                entity.Property(e => e.Telephone).HasMaxLength(20);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.NameSurname).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.Tc)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("TC")
                    .IsFixedLength(true);

                entity.Property(e => e.Telephone).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.HasOne(d => d.PolyclinicNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Polyclinic)
                    .HasConstraintName("FK_Doctor_Polyclinic");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.NameSurname).HasMaxLength(50);

                entity.Property(e => e.Tc)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("TC")
                    .IsFixedLength(true);

                entity.Property(e => e.Telephone).HasMaxLength(20);

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.Doctor)
                    .HasConstraintName("FK_Patient_Doctor");
            });

            modelBuilder.Entity<PatientDiagnosis>(entity =>
            {
                entity.ToTable("PatientDiagnosis");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Gsm).HasMaxLength(50);
            });

            modelBuilder.Entity<Polyclinic>(entity =>
            {
                entity.ToTable("Polyclinic");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("SysUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gsm).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(10);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Xappointment>(entity =>
            {
                entity.ToTable("XAppointment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Gsm)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserIdentifer).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Xappointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_XAppointment_Doctor");
            });

            modelBuilder.Entity<Xdoctor>(entity =>
            {
                entity.ToTable("XDoctor");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Img).HasMaxLength(50);

                entity.HasOne(d => d.Polyclinic)
                    .WithMany(p => p.Xdoctors)
                    .HasForeignKey(d => d.PolyclinicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_XDoctor_Polyclinic");

                entity.HasOne(d => d.SysUser)
                    .WithMany(p => p.Xdoctors)
                    .HasForeignKey(d => d.SysUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_XDoctor_SysUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
