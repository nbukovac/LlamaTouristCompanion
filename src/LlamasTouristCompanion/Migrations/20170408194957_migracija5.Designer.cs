using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Migrations
{
    [DbContext(typeof(TouristDbContext))]
    [Migration("20170408194957_migracija5")]
    partial class migracija5
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

            modelBuilder.Entity("LlamasTouristCompanion.Models.BotCache", b =>
                {
                    b.Property<Guid>("BotCacheId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer")
                        .IsRequired();

                    b.Property<Guid>("ApartmentId");

                    b.Property<string>("Keyword")
                        .IsRequired();

                    b.HasKey("BotCacheId");

                    b.HasIndex("ApartmentId");

                    b.ToTable("BotCaches");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

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

                    b.Property<string>("Address")
                        .IsRequired();

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

            modelBuilder.Entity("LlamasTouristCompanion.Models.Price", b =>
                {
                    b.Property<Guid>("PriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<Guid>("ApartmentId");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("PriceId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("LlamasTouristCompanion.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApartmentId");

                    b.Property<bool>("CacheAdvance");

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("GuestId");

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

            modelBuilder.Entity("LlamasTouristCompanion.Models.BotCache", b =>
                {
                    b.HasOne("LlamasTouristCompanion.Models.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId")
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
