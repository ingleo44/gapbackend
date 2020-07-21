using System;
using DAL.model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCommon.Model
{
    public partial class MedAppointmentsContext : DbContext
    {


        public MedAppointmentsContext()
        {

        }

        public MedAppointmentsContext(DbContextOptions<MedAppointmentsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Patient> patients { get; set; }
        public virtual DbSet<Appointment> appointments { get; set; }
        public virtual DbSet<AppointmentType> appointmentTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appointment>(entity =>
            {

                entity.HasOne(d => d.patient)
                    .WithMany(p => p.appointments)
                    .HasForeignKey(d => d.patientId);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {

                entity.HasOne(d => d.appointmentType)
                    .WithMany(p => p.appointments)
                    .HasForeignKey(d => d.appointmentTypeId);
            });

            modelBuilder.Entity<Patient>().HasQueryFilter(p => p.active);
            modelBuilder.Entity<Appointment>().HasQueryFilter(p => p.active);
            modelBuilder.Entity<AppointmentType>().HasQueryFilter(p => p.active);




        }

   
    }
}
