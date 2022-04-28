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
    [Migration("20220428230504_AdiciosEmpresa")]
    partial class AdiciosEmpresa
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
                    b.Property<int>("area_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("area_Id"), 1L, 1);

                    b.Property<int>("area_Codigo")
                        .HasColumnType("int");

                    b.Property<string>("area_Descripcion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("area_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("area_Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Empresa", b =>
                {
                    b.Property<long>("Empresa_Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Empresa_COdigoPostal")
                        .HasColumnType("int");

                    b.Property<string>("Empresa_Ciudad")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Empresa_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Empresa_Codigo"), 1L, 1);

                    b.Property<string>("Empresa_Correo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Empresa_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Empresa_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Empresa_Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TipoIdentificacion_Id")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TipoIdentificacion_Id1")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Empresa_Id");

                    b.HasIndex("TipoIdentificacion_Id1");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Rol", b =>
                {
                    b.Property<int>("Rol_Id")
                        .HasColumnType("int");

                    b.Property<int>("Rol_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Rol_Codigo"), 1L, 1);

                    b.Property<string>("Rol_Descripcion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Rol_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Rol_Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.TipoIdentificacion", b =>
                {
                    b.Property<string>("TipoIdentificacion_Id")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("TipoIdentificacion_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoIdentificacion_Codigo"), 1L, 1);

                    b.Property<string>("TipoIdentificacion_Descripcion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TipoIdentificacion_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TipoIdentificacion_Id");

                    b.ToTable("TipoIdentificaciones");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.TipoUsuario", b =>
                {
                    b.Property<string>("TipoUsuario_Id")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("TipoUsuario_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoUsuario_Codigo"), 1L, 1);

                    b.Property<string>("TipoUsuario_Descripcion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TipoUsuario_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TipoUsuario_Id");

                    b.ToTable("TipoUsuarios");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Empresa", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany()
                        .HasForeignKey("TipoIdentificacion_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoIdentificacion");
                });
#pragma warning restore 612, 618
        }
    }
}
