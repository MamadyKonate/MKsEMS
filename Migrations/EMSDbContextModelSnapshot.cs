﻿// <auto-generated />
using System;
using MKsEMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MKsEMS.Migrations
{
    [DbContext(typeof(EMSDbContext))]
    partial class EMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("MKsEMS.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsToBeDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogoURI")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("MKsEMS.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Eircode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("MKsEMS.Models.Credentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EncPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("MKsEMS.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Salary")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("MKsEMS.Models.Leave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Allowance")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateTo")
                        .HasColumnType("TEXT");

                    b.Property<string>("DenialReason")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LeaveStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeaveType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Taken")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("numberOfDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("MKsEMS.Models.LeaveAllowance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Allowance")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("LeaveAllowances");
                });

            modelBuilder.Entity("MKsEMS.Models.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("MKsEMS.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<bool>("FirstTimeLogin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCEO")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsManager")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUserLoggedIn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("LeaveEntitement")
                        .HasColumnType("REAL");

                    b.Property<double>("LeaveTaken")
                        .HasColumnType("REAL");

                    b.Property<string>("ManagerEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("SickLeaveTaken")
                        .HasColumnType("REAL");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MKsEMS.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("MKsEMS.ViewModels.ResetPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrentPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NewPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReEnterNewPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ResetPasses");
                });

            modelBuilder.Entity("MKsEMS.Models.Administraor", b =>
                {
                    b.HasBaseType("MKsEMS.Models.User");

                    b.HasDiscriminator().HasValue("Administraor");
                });

            modelBuilder.Entity("MKsEMS.Models.Employee", b =>
                {
                    b.HasBaseType("MKsEMS.Models.User");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("MKsEMS.Models.Manager", b =>
                {
                    b.HasBaseType("MKsEMS.Models.User");

                    b.HasDiscriminator().HasValue("Manager");
                });
#pragma warning restore 612, 618
        }
    }
}
