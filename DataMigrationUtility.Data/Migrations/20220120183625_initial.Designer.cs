﻿// <auto-generated />
using System;
using DataMigrationUtility.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataMigrationUtility.Data.Migrations
{
    [DbContext(typeof(DataMigrationUtilityDbContext))]
    [Migration("20220120183625_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataMigrationUtility.Domain.DestinationTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SourceTableID")
                        .HasColumnType("int");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SourceTableID");

                    b.ToTable("DestinationTable");
                });

            modelBuilder.Entity("DataMigrationUtility.Domain.SourceTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FirstNumber")
                        .HasColumnType("int");

                    b.Property<int>("SecondNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("SourceTable");
                });

            modelBuilder.Entity("DataMigrationUtility.Domain.DestinationTable", b =>
                {
                    b.HasOne("DataMigrationUtility.Domain.SourceTable", "SourceTable")
                        .WithMany()
                        .HasForeignKey("SourceTableID");

                    b.Navigation("SourceTable");
                });
#pragma warning restore 612, 618
        }
    }
}
