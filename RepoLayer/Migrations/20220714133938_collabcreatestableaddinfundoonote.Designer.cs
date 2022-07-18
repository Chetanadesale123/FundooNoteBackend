﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepoLayer.Context;

namespace RepoLayer.Migrations
{
    [DbContext(typeof(FundooContext))]
    [Migration("20220714133938_collabcreatestableaddinfundoonote")]
    partial class collabcreatestableaddinfundoonote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepoLayer.Entities.CollabeEntity", b =>
                {
                    b.Property<long>("CollabId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CollabeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("noteid")
                        .HasColumnType("bigint");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("CollabId");

                    b.HasIndex("noteid");

                    b.HasIndex("userid");

                    b.ToTable("CollabeTable");
                });

            modelBuilder.Entity("RepoLayer.Entities.LabelEntity", b =>
                {
                    b.Property<long>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LabelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("noteid")
                        .HasColumnType("bigint");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("LabelId");

                    b.HasIndex("noteid");

                    b.HasIndex("userid");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("RepoLayer.Entities.NoteEntity", b =>
                {
                    b.Property<long>("noteid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("editAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("isPinned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("reminder")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("noteid");

                    b.HasIndex("userid");

                    b.ToTable("NotesTable");
                });

            modelBuilder.Entity("RepoLayer.Entities.UserEntity", b =>
                {
                    b.Property<long>("userid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepoLayer.Entities.CollabeEntity", b =>
                {
                    b.HasOne("RepoLayer.Entities.NoteEntity", "Note")
                        .WithMany()
                        .HasForeignKey("noteid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepoLayer.Entities.LabelEntity", b =>
                {
                    b.HasOne("RepoLayer.Entities.NoteEntity", "Note")
                        .WithMany()
                        .HasForeignKey("noteid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepoLayer.Entities.NoteEntity", b =>
                {
                    b.HasOne("RepoLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
