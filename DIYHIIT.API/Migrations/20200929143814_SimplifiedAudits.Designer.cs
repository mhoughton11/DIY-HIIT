﻿// <auto-generated />
using System;
using DIYHIIT.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DIYHIIT.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200929143814_SimplifiedAudits")]
    partial class SimplifiedAudits
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DIYHIIT.Library.Models.AuditTrail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DOE")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuditTrails");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.Exercise", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<double?>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("WorkoutID");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.ViewComponents.FeedItem", b =>
                {
                    b.Property<int>("FeedItemKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FeedType")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkoutID")
                        .HasColumnType("int");

                    b.HasKey("FeedItemKey");

                    b.HasIndex("WorkoutID");

                    b.ToTable("FeedItems");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.Workout", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("ActiveInterval")
                        .HasColumnType("float");

                    b.Property<string>("BackgroundImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BodyFocus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUsed")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Duration")
                        .HasColumnType("float");

                    b.Property<double?>("Effort")
                        .HasColumnType("float");

                    b.Property<string>("ExerciseCount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("RestInterval")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.AuditTrail", b =>
                {
                    b.HasOne("DIYHIIT.Library.Models.User", "User")
                        .WithMany("WorkoutAuditTrails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.Exercise", b =>
                {
                    b.HasOne("DIYHIIT.Library.Models.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.ViewComponents.FeedItem", b =>
                {
                    b.HasOne("DIYHIIT.Library.Models.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutID");
                });

            modelBuilder.Entity("DIYHIIT.Library.Models.Workout", b =>
                {
                    b.HasOne("DIYHIIT.Library.Models.User", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
