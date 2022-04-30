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
    [Migration("20220430173426_adicionSedesClientes")]
    partial class adicionSedesClientes
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
                    b.Property<long>("Area_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Area_Id"), 1L, 1);

                    b.Property<string>("Area_Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Area_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Area_Id");

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

                    b.ToTable("Cajas_Compensaciones");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Clientes", b =>
                {
                    b.Property<long>("Cli_Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Cli_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cli_Codigo"), 1L, 1);

                    b.Property<string>("Cli_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Cli_Email")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Cli_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<long>("Cli_Telefono")
                        .HasColumnType("bigint");

                    b.Property<int>("TPCli_Id")
                        .HasColumnType("int");

                    b.Property<string>("TipoIdentificacion_Id")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Cli_Id");

                    b.HasIndex("TPCli_Id");

                    b.HasIndex("TipoIdentificacion_Id");

                    b.ToTable("Clientes");
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

                    b.HasKey("Empresa_Id");

                    b.HasIndex("TipoIdentificacion_Id");

                    b.ToTable("Empresas");
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

            modelBuilder.Entity("PlasticaribeAPI.Models.Estado", b =>
                {
                    b.Property<int>("Estado_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Estado_Id"), 1L, 1);

                    b.Property<string>("Estado_Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Estado_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Estado_Id");

                    b.ToTable("Estados");
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

                    b.ToTable("FondosPensiones");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Rol_Usuario", b =>
                {
                    b.Property<int>("RolUsu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolUsu_Id"), 1L, 1);

                    b.Property<string>("RolUsu_Descripcion")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RolUsu_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("RolUsu_Id");

                    b.ToTable("Roles_Usuarios");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.SedesClientes", b =>
                {
                    b.Property<long>("SedeCli_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Cli_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("SedeCli_CodPostal")
                        .HasColumnType("bigint");

                    b.Property<int>("SedeCli_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SedeCli_Codigo"), 1L, 1);

                    b.Property<string>("SedeCliente_Ciudad")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("SedeCli_Id");

                    b.HasIndex("Cli_Id");

                    b.ToTable("Sedes_Clientes");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Bodega", b =>
                {
                    b.Property<int>("TpBod_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TpBod_Id"), 1L, 1);

                    b.Property<long>("Area_Id")
                        .HasColumnType("bigint");

                    b.Property<string>("TpBod_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TpBod_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TpBod_Id");

                    b.HasIndex("Area_Id");

                    b.ToTable("Tipos_Bodegas");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Producto", b =>
                {
                    b.Property<int>("TpProd_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TpProd_Id"), 1L, 1);

                    b.Property<string>("TpProd_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TpProd_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("TpProd_Id");

                    b.ToTable("Tipos_Productos");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Usuario", b =>
                {
                    b.Property<int>("tpUsu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tpUsu_Id"), 1L, 1);

                    b.Property<string>("tpUsu_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tpUsu_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("tpUsu_Id");

                    b.ToTable("Tipos_Usuarios");
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

            modelBuilder.Entity("PlasticaribeAPI.Models.TiposClientes", b =>
                {
                    b.Property<int>("TPCli_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TPCli_Id"), 1L, 1);

                    b.Property<string>("TPCli_Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("TPCli_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TPCli_Id");

                    b.ToTable("Tipos_Clientes");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Usuario", b =>
                {
                    b.Property<long>("Usua_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Area_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Empresa_Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Estado_Id")
                        .HasColumnType("int");

                    b.Property<int>("RolUsu_Id")
                        .HasColumnType("int");

                    b.Property<string>("TipoIdentificacion_Id")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Usua_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Usua_Codigo"), 1L, 1);

                    b.Property<string>("Usua_Contrasena")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Usua_Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Usua_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Usua_Telefono")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<long>("cajComp_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("eps_Id")
                        .HasColumnType("bigint");

                    b.Property<long>("fPen_Id")
                        .HasColumnType("bigint");

                    b.Property<int>("tpUsu_Id")
                        .HasColumnType("int");

                    b.HasKey("Usua_Id");

                    b.HasIndex("Area_Id");

                    b.HasIndex("Empresa_Id");

                    b.HasIndex("Estado_Id");

                    b.HasIndex("RolUsu_Id");

                    b.HasIndex("TipoIdentificacion_Id");

                    b.HasIndex("cajComp_Id");

                    b.HasIndex("eps_Id");

                    b.HasIndex("fPen_Id");

                    b.HasIndex("tpUsu_Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Clientes", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.TiposClientes", "TPCli")
                        .WithMany()
                        .HasForeignKey("TPCli_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany()
                        .HasForeignKey("TipoIdentificacion_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TPCli");

                    b.Navigation("TipoIdentificacion");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Empresa", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany()
                        .HasForeignKey("TipoIdentificacion_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoIdentificacion");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.SedesClientes", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Clientes", "Cli")
                        .WithMany()
                        .HasForeignKey("Cli_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cli");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Bodega", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Area", "area")
                        .WithMany()
                        .HasForeignKey("Area_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("area");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Usuario", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("Area_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("Empresa_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("Estado_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Rol_Usuario", "RolUsu")
                        .WithMany()
                        .HasForeignKey("RolUsu_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany()
                        .HasForeignKey("TipoIdentificacion_Id");

                    b.HasOne("PlasticaribeAPI.Models.cajaCompensacion", "cajComp")
                        .WithMany()
                        .HasForeignKey("cajComp_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.EPS", "EPS")
                        .WithMany()
                        .HasForeignKey("eps_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.fondoPension", "fPen")
                        .WithMany()
                        .HasForeignKey("fPen_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Tipo_Usuario", "tpUsu")
                        .WithMany()
                        .HasForeignKey("tpUsu_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("EPS");

                    b.Navigation("Empresa");

                    b.Navigation("Estado");

                    b.Navigation("RolUsu");

                    b.Navigation("TipoIdentificacion");

                    b.Navigation("cajComp");

                    b.Navigation("fPen");

                    b.Navigation("tpUsu");
                });
#pragma warning restore 612, 618
        }
    }
}
