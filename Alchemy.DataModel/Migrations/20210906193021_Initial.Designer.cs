﻿// <auto-generated />
using System;
using Alchemy.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alchemy.DataModel.Migrations
{
    [DbContext(typeof(AlchemyContext))]
    [Migration("20210906193021_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alchemy.DataModel.Entities.Dlc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Dlcs");
                });

            modelBuilder.Entity("Alchemy.DataModel.Entities.Effect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Effects");
                });

            modelBuilder.Entity("Alchemy.DataModel.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseValue")
                        .HasColumnType("int");

                    b.Property<int?>("DlcId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(23)
                        .HasColumnType("nvarchar(23)");

                    b.Property<string>("Obtaining")
                        .HasMaxLength(92)
                        .HasColumnType("nvarchar(92)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DlcId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("EffectIngredient", b =>
                {
                    b.Property<int>("EffectsId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.HasKey("EffectsId", "IngredientsId");

                    b.HasIndex("IngredientsId");

                    b.ToTable("EffectIngredient");
                });

            modelBuilder.Entity("Alchemy.DataModel.Entities.Ingredient", b =>
                {
                    b.HasOne("Alchemy.DataModel.Entities.Dlc", "Dlc")
                        .WithMany("Ingredients")
                        .HasForeignKey("DlcId");

                    b.Navigation("Dlc");
                });

            modelBuilder.Entity("EffectIngredient", b =>
                {
                    b.HasOne("Alchemy.DataModel.Entities.Effect", null)
                        .WithMany()
                        .HasForeignKey("EffectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alchemy.DataModel.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Alchemy.DataModel.Entities.Dlc", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
