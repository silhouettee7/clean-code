﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250125232110_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Persistence.Entities.DocumentAccessLevelEntity", b =>
                {
                    b.Property<int>("DocumentAccessLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DocumentAccessLevelId"));

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DocumentAccessLevelId");

                    b.ToTable("AccessLevels");

                    b.HasData(
                        new
                        {
                            DocumentAccessLevelId = 1,
                            LevelName = "Read"
                        },
                        new
                        {
                            DocumentAccessLevelId = 2,
                            LevelName = "Edit"
                        });
                });

            modelBuilder.Entity("Persistence.Entities.DocumentAccessTypeEntity", b =>
                {
                    b.Property<int>("DocumentAccessTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DocumentAccessTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DocumentAccessTypeId");

                    b.ToTable("AccessTypes");

                    b.HasData(
                        new
                        {
                            DocumentAccessTypeId = 1,
                            TypeName = "Private"
                        },
                        new
                        {
                            DocumentAccessTypeId = 2,
                            TypeName = "PublicRead"
                        },
                        new
                        {
                            DocumentAccessTypeId = 3,
                            TypeName = "PublicEdit"
                        });
                });

            modelBuilder.Entity("Persistence.Entities.DocumentEntity", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("DocumentId");

                    b.HasIndex("AccessTypeId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentPermissionEntity", b =>
                {
                    b.Property<Guid>("DocumentPermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessLevelId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("DocumentPermissionId");

                    b.HasIndex("AccessLevelId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Persistence.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentEntity", b =>
                {
                    b.HasOne("Persistence.Entities.DocumentAccessTypeEntity", "AccessType")
                        .WithMany("Documents")
                        .HasForeignKey("AccessTypeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Persistence.Entities.UserEntity", "Author")
                        .WithMany("Documents")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessType");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentPermissionEntity", b =>
                {
                    b.HasOne("Persistence.Entities.DocumentAccessLevelEntity", "AccessLevel")
                        .WithMany("DocumentPermissions")
                        .HasForeignKey("AccessLevelId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Persistence.Entities.DocumentEntity", "Document")
                        .WithMany("Permissions")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Entities.UserEntity", "User")
                        .WithMany("DocumentsPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessLevel");

                    b.Navigation("Document");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentAccessLevelEntity", b =>
                {
                    b.Navigation("DocumentPermissions");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentAccessTypeEntity", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("Persistence.Entities.DocumentEntity", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("DocumentsPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
