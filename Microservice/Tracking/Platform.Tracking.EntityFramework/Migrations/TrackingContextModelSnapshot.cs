﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Platform.Tracking.EntityFramework;

#nullable disable

namespace Platform.Tracking.EntityFramework.Migrations
{
    [DbContext(typeof(TrackingContext))]
    partial class TrackingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("Platform.Tracking.DataModel.BrandAction.BrandActionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BrandModelId")
                        .HasColumnType("uuid");

                    b.Property<string>("BrandName")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeOfAction")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TypeOfAction")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("BrandAction");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.BrandAction.BrandActionSummaryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BrandModelId")
                        .HasColumnType("uuid");

                    b.Property<long>("GoToAppCount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<long>("ViewDetailsCount")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BrandActionSummary");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.BrandAction.BrandActionSummaryView", b =>
                {
                    b.Property<Guid>("BrandModelId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeOfAction")
                        .HasColumnType("integer");

                    b.Property<long>("TypeOfActionCount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.ToTable((string)null);

                    b.ToView("brandactionsummaryviews", (string)null);
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.GetDeals.AggregatorItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AggregatorItemId")
                        .HasColumnType("uuid");

                    b.Property<int?>("DealActionEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DealActionEntityId");

                    b.ToTable("AggregatorItem");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.GetDeals.DealActionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TimeOfAction")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("DealAction");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.Offre.OfferActionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<Guid>("OffreID")
                        .HasColumnType("uuid");

                    b.Property<string>("SocialMedia")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeOfAction")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("OfferAction");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.UserSearch.UserSearchEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("HasResults")
                        .HasColumnType("boolean");

                    b.Property<string>("SearchText")
                        .HasColumnType("text");

                    b.Property<DateTime>("SearchTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserSearch");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.UserStatus.UserStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateLogin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateLogout")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.GetDeals.AggregatorItemEntity", b =>
                {
                    b.HasOne("Platform.Tracking.DataModel.GetDeals.DealActionEntity", null)
                        .WithMany("Aggregator")
                        .HasForeignKey("DealActionEntityId");
                });

            modelBuilder.Entity("Platform.Tracking.DataModel.GetDeals.DealActionEntity", b =>
                {
                    b.Navigation("Aggregator");
                });
#pragma warning restore 612, 618
        }
    }
}
