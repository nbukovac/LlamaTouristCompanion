﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Migrations
{
    [DbContext(typeof(TouristDbContext))]
    [Migration("20170408115818_migracija")]
    partial class migracija
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LlamasTouristCompanion.Models.Apartment", b =>
                {
                    b.Property<Guid>("ApartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CheckIn");

                    b.Property<DateTime>("CheckOut");

                    b.Property<string>("Images")
                        .IsRequired();

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Utilities")
                        .IsRequired();

                    b.HasKey("ApartmentId");

                    b.HasIndex("LocationId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("LocationId");

                    b.HasKey("EventId");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Guest", b =>
                {
                    b.Property<Guid>("GuestId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("IdType");

                    b.Property<string>("IdentificationNumber");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<Guid>("UserId");

                    b.HasKey("GuestId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Info", b =>
                {
                    b.Property<Guid>("InfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("InfoId");

                    b.HasIndex("LocationId");

                    b.ToTable("Infos");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Owner", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FacebookUrl");

                    b.Property<string>("InstagramUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("TwitterUrl");

                    b.Property<Guid>("UserId");

                    b.Property<string>("YoutubeUrl");

                    b.HasKey("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApartmentId");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("Price");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ReservationId");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Apartment", b =>
                {
                    b.HasOne("LlamasTouristCompanion.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LlamasTouristCompanion.Models.Owner", "Owner")
                        .WithMany("Apartments")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Event", b =>
                {
                    b.HasOne("LlamasTouristCompanion.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Info", b =>
                {
                    b.HasOne("LlamasTouristCompanion.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Reservation", b =>
                {
                    b.HasOne("LlamasTouristCompanion.Models.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
