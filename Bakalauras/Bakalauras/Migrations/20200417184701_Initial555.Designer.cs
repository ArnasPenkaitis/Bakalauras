﻿// <auto-generated />
using System;
using Bakalauras.Shared.DataManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bakalauras.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200417184701_Initial555")]
    partial class Initial555
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bakalauras.Modeling.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation");

                    b.Property<string>("Name");

                    b.Property<int?>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Visualization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("FileUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Visualizations");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.VisualizationClasses", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("VisualizationId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ClassId", "VisualizationId", "Id");

                    b.HasAlternateKey("Id");

                    b.HasIndex("VisualizationId");

                    b.ToTable("VisualizationClasses");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Class", b =>
                {
                    b.HasOne("Bakalauras.Modeling.Models.Subject", "Subject")
                        .WithMany("Classes")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.Subject", b =>
                {
                    b.HasOne("Bakalauras.Modeling.Models.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Bakalauras.Modeling.Models.VisualizationClasses", b =>
                {
                    b.HasOne("Bakalauras.Modeling.Models.Class", "Classes")
                        .WithMany("VisualizationClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bakalauras.Modeling.Models.Visualization", "Visualization")
                        .WithMany("VisualizationClasses")
                        .HasForeignKey("VisualizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
