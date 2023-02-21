﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NonogramPuzzle.Models;

#nullable disable

namespace NonogramPuzzle.Migrations
{
    [DbContext(typeof(NonogramPuzzleContext))]
    [Migration("20230220064339_AddNonogramValidation")]
    partial class AddNonogramValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NonogramPuzzle.Models.Cell", b =>
                {
                    b.Property<int>("CellId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CellState")
                        .HasColumnType("int");

                    b.Property<int>("NonogramId")
                        .HasColumnType("int");

                    b.HasKey("CellId");

                    b.HasIndex("NonogramId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("NonogramPuzzle.Models.Nonogram", b =>
                {
                    b.Property<int>("NonogramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NonogramHeight")
                        .HasColumnType("int");

                    b.Property<int>("NonogramWidth")
                        .HasColumnType("int");

                    b.HasKey("NonogramId");

                    b.ToTable("Nonograms");
                });

            modelBuilder.Entity("NonogramPuzzle.Models.Cell", b =>
                {
                    b.HasOne("NonogramPuzzle.Models.Nonogram", "Nonogram")
                        .WithMany("Cells")
                        .HasForeignKey("NonogramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nonogram");
                });

            modelBuilder.Entity("NonogramPuzzle.Models.Nonogram", b =>
                {
                    b.Navigation("Cells");
                });
#pragma warning restore 612, 618
        }
    }
}
