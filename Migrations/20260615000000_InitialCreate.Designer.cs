using System;
using CineAnimeWeb.Data;
using CineAnimeWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineAnimeWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260615000000_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CineAnimeWeb.Models.Avaliacao", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Comentario").IsRequired().HasMaxLength(500).HasColumnType("nvarchar(500)");
                b.Property<DateTime>("DataAvaliacao").HasColumnType("datetime2");
                b.Property<string>("NomeUsuario").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<decimal>("Nota").HasPrecision(4, 1).HasColumnType("decimal(4,1)");
                b.Property<int>("ObraId").HasColumnType("int");
                b.HasKey("Id");
                b.HasIndex("ObraId");
                b.ToTable("Avaliacoes");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.DiretorEstudio", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Nome").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<string>("Pais").IsRequired().HasMaxLength(80).HasColumnType("nvarchar(80)");
                b.HasKey("Id");
                b.ToTable("DiretorEstudios");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.Genero", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("Nome").IsRequired().HasMaxLength(80).HasColumnType("nvarchar(80)");
                b.HasKey("Id");
                b.ToTable("Generos");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.Obra", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<int>("AnoLancamento").HasColumnType("int");
                b.Property<int>("DiretorEstudioId").HasColumnType("int");
                b.Property<int>("DuracaoMinutos").HasColumnType("int");
                b.Property<string>("Sinopse").IsRequired().HasMaxLength(1000).HasColumnType("nvarchar(1000)");
                b.Property<int>("Tipo").HasColumnType("int");
                b.Property<string>("Titulo").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
                b.HasKey("Id");
                b.HasIndex("DiretorEstudioId");
                b.ToTable("Obras");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.ObraGenero", b =>
            {
                b.Property<int>("ObraId").HasColumnType("int");
                b.Property<int>("GeneroId").HasColumnType("int");
                b.HasKey("ObraId", "GeneroId");
                b.HasIndex("GeneroId");
                b.ToTable("ObraGeneros");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.Avaliacao", b =>
            {
                b.HasOne("CineAnimeWeb.Models.Obra", "Obra")
                    .WithMany("Avaliacoes")
                    .HasForeignKey("ObraId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                b.Navigation("Obra");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.Obra", b =>
            {
                b.HasOne("CineAnimeWeb.Models.DiretorEstudio", "DiretorEstudio")
                    .WithMany("Obras")
                    .HasForeignKey("DiretorEstudioId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                b.Navigation("DiretorEstudio");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.ObraGenero", b =>
            {
                b.HasOne("CineAnimeWeb.Models.Genero", "Genero")
                    .WithMany("ObraGeneros")
                    .HasForeignKey("GeneroId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                b.HasOne("CineAnimeWeb.Models.Obra", "Obra")
                    .WithMany("ObraGeneros")
                    .HasForeignKey("ObraId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                b.Navigation("Genero");
                b.Navigation("Obra");
            });

            modelBuilder.Entity("CineAnimeWeb.Models.DiretorEstudio", b => b.Navigation("Obras"));
            modelBuilder.Entity("CineAnimeWeb.Models.Genero", b => b.Navigation("ObraGeneros"));
            modelBuilder.Entity("CineAnimeWeb.Models.Obra", b =>
            {
                b.Navigation("Avaliacoes");
                b.Navigation("ObraGeneros");
            });
        }
    }
}
