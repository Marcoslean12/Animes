using CineAnimeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Obras.Any())
        {
            return;
        }

        var diretoresEstudios = new List<DiretorEstudio>
        {
            new() { Nome = "Christopher Nolan", Pais = "Reino Unido" },
            new() { Nome = "Peter Jackson", Pais = "Nova Zelandia" },
            new() { Nome = "Studio Pierrot", Pais = "Japao" },
            new() { Nome = "MAPPA", Pais = "Japao" },
            new() { Nome = "Madhouse", Pais = "Japao" }
        };

        var generos = new List<Genero>
        {
            new() { Nome = "Acao" },
            new() { Nome = "Aventura" },
            new() { Nome = "Drama" },
            new() { Nome = "Ficcao Cientifica" },
            new() { Nome = "Fantasia" },
            new() { Nome = "Shounen" },
            new() { Nome = "Suspense" }
        };

        context.DiretorEstudios.AddRange(diretoresEstudios);
        context.Generos.AddRange(generos);
        context.SaveChanges();

        var obras = new List<Obra>
        {
            new()
            {
                Titulo = "O Senhor dos Aneis",
                Tipo = TipoObra.Filme,
                AnoLancamento = 2001,
                DuracaoMinutos = 178,
                Sinopse = "Uma jornada epica para destruir um anel poderoso.",
                DiretorEstudioId = diretoresEstudios[1].Id
            },
            new()
            {
                Titulo = "Interestelar",
                Tipo = TipoObra.Filme,
                AnoLancamento = 2014,
                DuracaoMinutos = 169,
                Sinopse = "Exploradores viajam pelo espaco em busca de um novo lar para a humanidade.",
                DiretorEstudioId = diretoresEstudios[0].Id
            },
            new()
            {
                Titulo = "Naruto",
                Tipo = TipoObra.Anime,
                AnoLancamento = 2002,
                DuracaoMinutos = 23,
                Sinopse = "Um jovem ninja sonha em se tornar Hokage.",
                DiretorEstudioId = diretoresEstudios[2].Id
            },
            new()
            {
                Titulo = "Attack on Titan",
                Tipo = TipoObra.Anime,
                AnoLancamento = 2013,
                DuracaoMinutos = 24,
                Sinopse = "Humanos lutam pela sobrevivencia contra titas gigantes.",
                DiretorEstudioId = diretoresEstudios[3].Id
            },
            new()
            {
                Titulo = "Death Note",
                Tipo = TipoObra.Anime,
                AnoLancamento = 2006,
                DuracaoMinutos = 23,
                Sinopse = "Um estudante encontra um caderno capaz de matar pessoas.",
                DiretorEstudioId = diretoresEstudios[4].Id
            },
            new()
            {
                Titulo = "Jujutsu Kaisen",
                Tipo = TipoObra.Anime,
                AnoLancamento = 2020,
                DuracaoMinutos = 24,
                Sinopse = "Estudantes enfrentam maldicoes usando energia amaldicoada.",
                DiretorEstudioId = diretoresEstudios[3].Id
            }
        };

        context.Obras.AddRange(obras);
        context.SaveChanges();

        context.ObraGeneros.AddRange(
            new ObraGenero { ObraId = obras[0].Id, GeneroId = generos[1].Id },
            new ObraGenero { ObraId = obras[0].Id, GeneroId = generos[4].Id },
            new ObraGenero { ObraId = obras[1].Id, GeneroId = generos[2].Id },
            new ObraGenero { ObraId = obras[1].Id, GeneroId = generos[3].Id },
            new ObraGenero { ObraId = obras[2].Id, GeneroId = generos[0].Id },
            new ObraGenero { ObraId = obras[2].Id, GeneroId = generos[5].Id },
            new ObraGenero { ObraId = obras[3].Id, GeneroId = generos[0].Id },
            new ObraGenero { ObraId = obras[3].Id, GeneroId = generos[2].Id },
            new ObraGenero { ObraId = obras[4].Id, GeneroId = generos[6].Id },
            new ObraGenero { ObraId = obras[5].Id, GeneroId = generos[0].Id },
            new ObraGenero { ObraId = obras[5].Id, GeneroId = generos[5].Id }
        );

        context.Avaliacoes.AddRange(
            new Avaliacao { ObraId = obras[0].Id, NomeUsuario = "Ana", Nota = 9, Comentario = "Fantasia muito marcante.", DataAvaliacao = DateTime.Today.AddDays(-10) },
            new Avaliacao { ObraId = obras[1].Id, NomeUsuario = "Bruno", Nota = 10, Comentario = "Ficcao cientifica emocionante.", DataAvaliacao = DateTime.Today.AddDays(-8) },
            new Avaliacao { ObraId = obras[2].Id, NomeUsuario = "Carla", Nota = 8, Comentario = "Classico dos animes shounen.", DataAvaliacao = DateTime.Today.AddDays(-6) },
            new Avaliacao { ObraId = obras[3].Id, NomeUsuario = "Diego", Nota = 9, Comentario = "Historia intensa e cheia de reviravoltas.", DataAvaliacao = DateTime.Today.AddDays(-4) },
            new Avaliacao { ObraId = obras[4].Id, NomeUsuario = "Eva", Nota = 10, Comentario = "Suspense excelente.", DataAvaliacao = DateTime.Today.AddDays(-2) },
            new Avaliacao { ObraId = obras[5].Id, NomeUsuario = "Felipe", Nota = 8, Comentario = "Boas lutas e visual moderno.", DataAvaliacao = DateTime.Today.AddDays(-1) }
        );

        context.SaveChanges();
    }
}
