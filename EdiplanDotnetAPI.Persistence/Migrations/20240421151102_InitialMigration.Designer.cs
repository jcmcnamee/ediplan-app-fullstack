﻿// <auto-generated />
using System;
using EdiplanDotnetAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EdiplanDotnetAPI.Persistence.Migrations
{
    [DbContext(typeof(EdiplanDbContext))]
    [Migration("20240421151102_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetAssetGroup", b =>
                {
                    b.Property<Guid>("AssetGroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssetsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AssetGroupsId", "AssetsId");

                    b.HasIndex("AssetsId");

                    b.ToTable("AssetAssetGroup");
                });

            modelBuilder.Entity("BookingBookingGroup", b =>
                {
                    b.Property<Guid>("BookingGroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingGroupsId", "BookingsId");

                    b.HasIndex("BookingsId");

                    b.ToTable("BookingBookingGroup");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Common.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RateUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Asset");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Asset");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.AssetGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AssetGroup");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductionId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c1d42095-7bf9-4f29-9f35-c47065efe1ff"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 5, 14, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9696),
                            IsConfirmed = false,
                            LocationId = new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"),
                            Name = "",
                            Notes = "High-speed internet required for remote editing.",
                            ProductionId = new Guid("4050a623-5308-4640-8c36-493729f6f884"),
                            StartDate = new DateTime(2024, 4, 23, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9623)
                        },
                        new
                        {
                            Id = new Guid("ac2e516a-90d9-4a96-9004-1f39863c9a51"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 5, 28, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9744),
                            IsConfirmed = false,
                            Name = "",
                            Notes = "Need access to soundproof dubbing studio.",
                            ProductionId = new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"),
                            StartDate = new DateTime(2024, 5, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9737)
                        },
                        new
                        {
                            Id = new Guid("0c69b80a-c5e5-416b-8a68-cb81dc5d9ba6"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 4, 11, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9849),
                            IsConfirmed = true,
                            LocationId = new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"),
                            Name = "",
                            Notes = "Final editing phase.",
                            ProductionId = new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"),
                            StartDate = new DateTime(2024, 4, 6, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9846)
                        },
                        new
                        {
                            Id = new Guid("5a5458ef-8cf3-4c91-89c7-d7a13d93d2a3"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 4, 16, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9883),
                            IsConfirmed = true,
                            LocationId = new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"),
                            Name = "",
                            Notes = "Location scouting.",
                            ProductionId = new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"),
                            StartDate = new DateTime(2024, 2, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9880)
                        },
                        new
                        {
                            Id = new Guid("e005a01c-114c-4b8e-a654-68635714d9a6"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 2, 11, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9916),
                            IsConfirmed = true,
                            LocationId = new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"),
                            Name = "",
                            Notes = "Principal photography.",
                            ProductionId = new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"),
                            StartDate = new DateTime(2024, 1, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9913)
                        },
                        new
                        {
                            Id = new Guid("b2f4ed23-5099-495e-9310-87338caaae77"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndDate = new DateTime(2024, 6, 26, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9970),
                            IsConfirmed = false,
                            LocationId = new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"),
                            Name = "",
                            Notes = "Pre-production meetings.",
                            ProductionId = new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"),
                            StartDate = new DateTime(2024, 6, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9967)
                        });
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.BookingGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookingGroups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5b7a148-0452-4e0e-8298-7c37fd4caa64"),
                            Name = "Offline"
                        },
                        new
                        {
                            Id = new Guid("4658345f-4454-4128-a2f8-c673e73fa846"),
                            Name = "Online"
                        },
                        new
                        {
                            Id = new Guid("146023a2-4255-4cfd-893a-d04b0839e616"),
                            Name = "Dub - In House"
                        },
                        new
                        {
                            Id = new Guid("1cd16d2e-a35b-48ef-ab93-debd26d445f0"),
                            Name = "Grade"
                        });
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"),
                            Name = "True North Productions, Leeds",
                            Notes = ""
                        },
                        new
                        {
                            Id = new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"),
                            Name = "Picture Shop, Manchester",
                            Notes = ""
                        },
                        new
                        {
                            Id = new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"),
                            Name = "The Crown Production Office, London",
                            Notes = ""
                        },
                        new
                        {
                            Id = new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"),
                            Name = "Top Gear Production Office, London",
                            Notes = ""
                        });
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Production", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Productions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4050a623-5308-4640-8c36-493729f6f884"),
                            Name = "Teen Mom UK Series 10"
                        },
                        new
                        {
                            Id = new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"),
                            Name = "The Great British Bake Off!"
                        },
                        new
                        {
                            Id = new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"),
                            Name = "Top Gear Special"
                        },
                        new
                        {
                            Id = new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"),
                            Name = "The Crown Season 5"
                        });
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Person", b =>
                {
                    b.HasBaseType("EdiplanDotnetAPI.Domain.Common.Asset");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("ProductionId");

                    b.HasDiscriminator().HasValue("Person");
                });

            modelBuilder.Entity("AssetAssetGroup", b =>
                {
                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.AssetGroup", null)
                        .WithMany()
                        .HasForeignKey("AssetGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EdiplanDotnetAPI.Domain.Common.Asset", null)
                        .WithMany()
                        .HasForeignKey("AssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingBookingGroup", b =>
                {
                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.BookingGroup", null)
                        .WithMany()
                        .HasForeignKey("BookingGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Common.Asset", b =>
                {
                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.Booking", null)
                        .WithMany("Asset")
                        .HasForeignKey("BookingId");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Booking", b =>
                {
                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.Location", "Location")
                        .WithMany("Bookings")
                        .HasForeignKey("LocationId");

                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.Production", "Production")
                        .WithMany("Bookings")
                        .HasForeignKey("ProductionId");

                    b.Navigation("Location");

                    b.Navigation("Production");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Person", b =>
                {
                    b.HasOne("EdiplanDotnetAPI.Domain.Entities.Production", null)
                        .WithMany("People")
                        .HasForeignKey("ProductionId");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Booking", b =>
                {
                    b.Navigation("Asset");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Location", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("EdiplanDotnetAPI.Domain.Entities.Production", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("People");
                });
#pragma warning restore 612, 618
        }
    }
}
