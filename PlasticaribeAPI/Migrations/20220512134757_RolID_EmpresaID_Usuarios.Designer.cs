﻿// <auto-generated />
using System;
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
    [Migration("20220512134757_RolID_EmpresaID_Usuarios")]
    partial class RolID_EmpresaID_Usuarios
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
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

            modelBuilder.Entity("PlasticaribeAPI.Models.Categoria_Insumo", b =>
                {
                    b.Property<int>("CatInsu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatInsu_Id"), 1L, 1);

                    b.Property<string>("CatInsu_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CatInsu_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("CatInsu_Id");

                    b.ToTable("Categorias_Insumos");
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

                    b.Property<long>("Usua_Id")
                        .HasColumnType("bigint");

                    b.HasKey("Cli_Id");

                    b.HasIndex("TPCli_Id");

                    b.HasIndex("TipoIdentificacion_Id");

                    b.HasIndex("Usua_Id");

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

                    b.Property<int>("TpEstado_Id")
                        .HasColumnType("int");

                    b.HasKey("Estado_Id");

                    b.HasIndex("TpEstado_Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Existencia_Producto", b =>
                {
                    b.Property<long>("ExProd_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExProd_Id"), 1L, 1);

                    b.Property<decimal>("ExProd_Cantidad")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExProd_Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExProd_PrecioExistencia")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ExProd_PrecioSinInflacion")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ExProd_PrecioTotalFinal")
                        .HasPrecision(18, 2)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prod_Id")
                        .HasColumnType("int");

                    b.Property<int>("TpBod_Id")
                        .HasColumnType("int");

                    b.Property<string>("UndMed_Id")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("ExProd_Id");

                    b.HasIndex("Prod_Id");

                    b.HasIndex("TpBod_Id");

                    b.HasIndex("UndMed_Id");

                    b.ToTable("Existencia_Producto");
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

            modelBuilder.Entity("PlasticaribeAPI.Models.Insumo", b =>
                {
                    b.Property<int>("Insu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Insu_Id"), 1L, 1);

                    b.Property<int>("CatInsu_Id")
                        .HasColumnType("int");

                    b.Property<string>("Insu_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Insu_Stock")
                        .IsRequired()
                        .HasPrecision(18, 2)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UndMed_Id")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Insu_Id");

                    b.HasIndex("CatInsu_Id");

                    b.HasIndex("UndMed_Id");

                    b.ToTable("Insumos");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.PedidoExterno", b =>
                {
                    b.Property<long>("PedExt_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PedExt_Id"), 1L, 1);

                    b.Property<long>("Empresa_Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Estado_Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("PedExt_Archivo")
                        .IsRequired()
                        .HasColumnType("binary(4)");

                    b.Property<long>("PedExt_Codigo")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PedExt_FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PedExt_FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("PedExt_Observacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PedExt_PrecioTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("SedeCliente")
                        .HasColumnType("bigint");

                    b.HasKey("PedExt_Id");

                    b.HasIndex("Empresa_Id");

                    b.HasIndex("Estado_Id");

                    b.HasIndex("SedeCliente");

                    b.ToTable("Pedidos_Externos");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Producto", b =>
                {
                    b.Property<int>("Prod_Id")
                        .HasColumnType("int");

                    b.Property<decimal?>("Prod_Ancho")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<decimal?>("Prod_Calibre")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("Prod_Cod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Prod_Cod"), 1L, 1);

                    b.Property<string>("Prod_Descripcion")
                        .HasColumnType("text");

                    b.Property<decimal?>("Prod_Fuelle")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<string>("Prod_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Prod_Peso_Bruto")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<decimal>("Prod_Peso_Neto")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("TpProd_Id")
                        .HasColumnType("int");

                    b.Property<string>("UndMedACF")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("UndMedPeso")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Prod_Id");

                    b.HasIndex("TpProd_Id");

                    b.HasIndex("UndMedACF");

                    b.HasIndex("UndMedPeso");

                    b.ToTable("Productos");
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

                    b.Property<long>("Cli_Id1")
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

                    b.Property<string>("SedeCliente_Direccion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("SedeCli_Id");

                    b.HasIndex("Cli_Id1");

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

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Estado", b =>
                {
                    b.Property<int>("TpEstado_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TpEstado_Id"), 1L, 1);

                    b.Property<string>("TpEstado_Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("TpEstado_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TpEstado_Id");

                    b.ToTable("Tipos_Estados");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Tipo_Moneda", b =>
                {
                    b.Property<string>("TpMoneda_Id")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TpMoneda_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TpMoneda_Codigo"), 1L, 1);

                    b.Property<string>("TpMoneda_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("TpMoneda_Id");

                    b.ToTable("Tipos_Monedas");
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

            modelBuilder.Entity("PlasticaribeAPI.Models.Unidad_Medida", b =>
                {
                    b.Property<string>("UndMed_Id")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("UndMed_Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UndMed_Codigo"), 1L, 1);

                    b.Property<string>("UndMed_Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UndMed_Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("UndMed_Id");

                    b.ToTable("Unidades_Medidas");
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
                        .IsRequired()
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

                    b.HasOne("PlasticaribeAPI.Models.Usuario", "Usua")
                        .WithMany()
                        .HasForeignKey("Usua_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TPCli");

                    b.Navigation("TipoIdentificacion");

                    b.Navigation("Usua");
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

            modelBuilder.Entity("PlasticaribeAPI.Models.Estado", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Tipo_Estado", null)
                        .WithMany()
                        .HasForeignKey("TpEstado_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Existencia_Producto", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Producto", "Prod")
                        .WithMany()
                        .HasForeignKey("Prod_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Tipo_Bodega", "TpBod")
                        .WithMany()
                        .HasForeignKey("TpBod_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Unidad_Medida", "UndMed")
                        .WithMany()
                        .HasForeignKey("UndMed_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prod");

                    b.Navigation("TpBod");

                    b.Navigation("UndMed");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Insumo", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Categoria_Insumo", null)
                        .WithMany()
                        .HasForeignKey("CatInsu_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Unidad_Medida", null)
                        .WithMany()
                        .HasForeignKey("UndMed_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.PedidoExterno", b =>
                {
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

                    b.HasOne("PlasticaribeAPI.Models.SedesClientes", "SedeCli")
                        .WithMany()
                        .HasForeignKey("SedeCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Estado");

                    b.Navigation("SedeCli");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.Producto", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Tipo_Producto", "TpProd")
                        .WithMany()
                        .HasForeignKey("TpProd_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Unidad_Medida", "UndMed2")
                        .WithMany()
                        .HasForeignKey("UndMedACF")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Unidad_Medida", "UndMed1")
                        .WithMany()
                        .HasForeignKey("UndMedPeso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TpProd");

                    b.Navigation("UndMed1");

                    b.Navigation("UndMed2");
                });

            modelBuilder.Entity("PlasticaribeAPI.Models.SedesClientes", b =>
                {
                    b.HasOne("PlasticaribeAPI.Models.Clientes", "Cli_Id")
                        .WithMany()
                        .HasForeignKey("Cli_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cli_Id");
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
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("Empresa_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("Estado_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.Rol_Usuario", "RolUsu")
                        .WithMany()
                        .HasForeignKey("RolUsu_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PlasticaribeAPI.Models.TipoIdentificacion", "TipoIdentificacion")
                        .WithMany()
                        .HasForeignKey("TipoIdentificacion_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
                        .OnDelete(DeleteBehavior.Restrict)
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
