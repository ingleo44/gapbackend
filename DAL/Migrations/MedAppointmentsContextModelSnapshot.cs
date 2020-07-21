﻿// <auto-generated />
using System;
using EFCoreCommon.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(MedAppointmentsContext))]
    partial class MedAppointmentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.model.Appointment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("appointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("appointmentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("patientId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("appointmentTypeId");

                    b.HasIndex("patientId");

                    b.ToTable("appointments");
                });

            modelBuilder.Entity("DAL.model.AppointmentType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("appointmentTypes");

                    b.HasData(
                        new
                        {
                            id = 1,
                            active = true,
                            name = "Consulta General"
                        },
                        new
                        {
                            id = 2,
                            active = true,
                            name = "Odontologia"
                        },
                        new
                        {
                            id = 3,
                            active = true,
                            name = "Pediatria"
                        },
                        new
                        {
                            id = 4,
                            active = true,
                            name = "Neurologia"
                        },
                        new
                        {
                            id = 5,
                            active = true,
                            name = "Ortopedia"
                        });
                });

            modelBuilder.Entity("DAL.model.Patient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("insuranceCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("DAL.model.Appointment", b =>
                {
                    b.HasOne("DAL.model.AppointmentType", "appointmentType")
                        .WithMany("appointments")
                        .HasForeignKey("appointmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.model.Patient", "patient")
                        .WithMany("appointments")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
