﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlasticaribeAPI.Data;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    [DbContext(typeof(dataContext))]
    [Migration("20220429133959_migracionArea1")]
    partial class migracionArea1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlasticaribeAPI.Models.Area", b =>
                {
                    b.Property<long>("area_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("area_Id"), 1L, 1);

                    b.Property<string>("area_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("area_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("area_Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.cajaCompensacion", b =>
                {
                    b.Property<long>("cajComp_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("cajComp_Ciudad")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("cajComp_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cajComp_Codigo"), 1L, 1);

                    b.Property<long>("cajComp_CuentaBancaria")
                        .HasColumnType("bigint");

                    b.Property<string>("cajComp_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("cajComp_Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("cajComp_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("cajComp_Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("cajComp_Id");

                    b.ToTable("cajas_Compensaciones");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.EPS", b =>
                {
                    b.Property<long>("eps_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("eps_Ciudad")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("eps_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("eps_Codigo"), 1L, 1);

                    b.Property<long>("eps_CuentaBancaria")
                        .HasColumnType("bigint");

                    b.Property<string>("eps_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("eps_Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("eps_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("eps_Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("eps_Id");

                    b.ToTable("EPS");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.fondoPension", b =>
                {
                    b.Property<long>("fPen_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("fPen_Ciudad")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("fPen_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("fPen_Codigo"), 1L, 1);

                    b.Property<long>("fPen_CuentaBancaria")
                        .HasColumnType("bigint");

                    b.Property<string>("fPen_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("fPen_Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("fPen_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("fPen_Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("fPen_Id");

                    b.ToTable("fondosPensiones");
                });
#pragma warning restore 612, 618
        }
    }
}
