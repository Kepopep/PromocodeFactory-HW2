﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeFactory.EntityFramework;

#nullable disable

namespace PromoCodeFactory.EntityFramework.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3");

            modelBuilder.Entity("CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomersId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferencesId")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomersId", "PreferencesId");

                    b.HasIndex("PreferencesId");

                    b.ToTable("CustomerPreference");
                });

            modelBuilder.Entity("EmployeeRole", b =>
                {
                    b.Property<Guid>("EmployeesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeesId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("EmployeeRole");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManager.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManager.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManager.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PartnerManagerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartnerName")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceInfo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PartnerManagerId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("CustomerPreference", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManager.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManager.Preference", null)
                        .WithMany()
                        .HasForeignKey("PreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeRole", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManager.PromoCode", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManager.Customer", "Customer")
                        .WithMany("PromoCodes")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId");

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManager.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId");

                    b.Navigation("Customer");

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManager.Customer", b =>
                {
                    b.Navigation("PromoCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
