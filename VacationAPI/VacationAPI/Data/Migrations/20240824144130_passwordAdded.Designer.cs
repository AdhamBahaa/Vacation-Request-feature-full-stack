﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VacationData;

#nullable disable

namespace VacationAPI.Data.Migrations
{
    [DbContext(typeof(VacationContext))]
    [Migration("20240824144130_passwordAdded")]
    partial class passwordAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VacationAPI.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("FromDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ToDate")
                        .HasColumnType("date");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");

                    b.HasData(
                        new
                        {
                            Id = 201,
                            FromDate = new DateOnly(2024, 8, 22),
                            StatusId = 1,
                            ToDate = new DateOnly(2024, 8, 23),
                            TypeId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 202,
                            FromDate = new DateOnly(2024, 8, 13),
                            StatusId = 1,
                            ToDate = new DateOnly(2024, 8, 23),
                            TypeId = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 203,
                            FromDate = new DateOnly(2024, 8, 3),
                            StatusId = 1,
                            ToDate = new DateOnly(2024, 8, 4),
                            TypeId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 204,
                            FromDate = new DateOnly(2024, 8, 2),
                            StatusId = 1,
                            ToDate = new DateOnly(2024, 8, 12),
                            TypeId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 205,
                            FromDate = new DateOnly(2024, 9, 2),
                            StatusId = 1,
                            ToDate = new DateOnly(2024, 9, 12),
                            TypeId = 2,
                            UserId = 4
                        });
                });

            modelBuilder.Entity("VacationAPI.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusType = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            StatusType = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            StatusType = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            StatusType = "Completed"
                        });
                });

            modelBuilder.Entity("VacationAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnnualDays")
                        .HasColumnType("int");

                    b.Property<int>("CasualDays")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnnualDays = 24,
                            CasualDays = 3,
                            Email = "adham@gmail.com",
                            ManagerId = 2,
                            Name = "Adham Bahaa",
                            Password = "0000"
                        },
                        new
                        {
                            Id = 2,
                            AnnualDays = 24,
                            CasualDays = 3,
                            Email = "Rabie@gmail.com",
                            Name = "Mohamed Rabie",
                            Password = "0000"
                        },
                        new
                        {
                            Id = 3,
                            AnnualDays = 20,
                            CasualDays = 5,
                            Email = "Doe@gmail.com",
                            Name = "John Doe",
                            Password = "0000"
                        },
                        new
                        {
                            Id = 4,
                            AnnualDays = 13,
                            CasualDays = 2,
                            Email = "Ismail@gmail.com",
                            ManagerId = 2,
                            Name = "Khaled Ismail",
                            Password = "0000"
                        },
                        new
                        {
                            Id = 5,
                            AnnualDays = 17,
                            CasualDays = 2,
                            Email = "Alcaraz@gmail.com",
                            ManagerId = 3,
                            Name = "Carlos Alcaraz",
                            Password = "0000"
                        });
                });

            modelBuilder.Entity("VacationAPI.Models.VacationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("VacType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            VacType = "Casual"
                        },
                        new
                        {
                            Id = 2,
                            VacType = "Annual"
                        });
                });

            modelBuilder.Entity("VacationAPI.Models.Request", b =>
                {
                    b.HasOne("VacationAPI.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacationAPI.Models.VacationType", "VacationType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacationAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");

                    b.Navigation("VacationType");
                });
#pragma warning restore 612, 618
        }
    }
}
