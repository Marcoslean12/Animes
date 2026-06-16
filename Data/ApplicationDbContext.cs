using CineAnimeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Obra> Obras => Set<Obra>();
    public DbSet<DiretorEstudio> DiretorEstudios => Set<DiretorEstudio>();
    public DbSet<Genero> Generos => Set<Genero>();
    public DbSet<ObraGenero> ObraGeneros => Set<ObraGenero>();
    public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ObraGenero>()
            .HasKey(og => new { og.ObraId, og.GeneroId });

        modelBuilder.Entity<ObraGenero>()
            .HasOne(og => og.Obra)
            .WithMany(o => o.ObraGeneros)
            .HasForeignKey(og => og.ObraId);

        modelBuilder.Entity<ObraGenero>()
            .HasOne(og => og.Genero)
            .WithMany(g => g.ObraGeneros)
            .HasForeignKey(og => og.GeneroId);

        modelBuilder.Entity<DiretorEstudio>()
            .HasMany(de => de.Obras)
            .WithOne(o => o.DiretorEstudio)
            .HasForeignKey(o => o.DiretorEstudioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Obra>()
            .HasMany(o => o.Avaliacoes)
            .WithOne(a => a.Obra)
            .HasForeignKey(a => a.ObraId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Avaliacao>()
            .Property(a => a.Nota)
            .HasPrecision(4, 1);
    }
}
