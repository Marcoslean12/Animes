using CineAnimeWeb.Data;
using CineAnimeWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Controllers;

public class ConsultasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ConsultasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int anoFiltro = 2010)
    {
        var obrasComDiretores = await _context.Obras
            .Include(o => o.DiretorEstudio)
            .OrderBy(o => o.Titulo)
            .Select(o => new ObraDiretorConsultaViewModel
            {
                Titulo = o.Titulo,
                Tipo = o.Tipo,
                AnoLancamento = o.AnoLancamento,
                NomeDiretorEstudio = o.DiretorEstudio!.Nome,
                PaisDiretorEstudio = o.DiretorEstudio.Pais
            })
            .ToListAsync();

        var obrasPorTipo = await _context.Obras
            .GroupBy(o => o.Tipo)
            .Select(g => new GrupoTipoConsultaViewModel
            {
                Tipo = g.Key,
                QuantidadeObras = g.Count(),
                MediaNotas = g.SelectMany(o => o.Avaliacoes).Any()
                    ? g.SelectMany(o => o.Avaliacoes).Average(a => a.Nota)
                    : 0
            })
            .OrderBy(g => g.Tipo)
            .ToListAsync();

        var diretoresComMaisDeUmaObra = await _context.Obras
            .Where(o => o.AnoLancamento >= anoFiltro)
            .GroupBy(o => new { o.DiretorEstudioId, o.DiretorEstudio!.Nome })
            .Where(g => g.Count() > 1)
            .Select(g => new GrupoDiretorConsultaViewModel
            {
                NomeDiretorEstudio = g.Key.Nome,
                QuantidadeObras = g.Count(),
                MediaAvaliacoes = g.SelectMany(o => o.Avaliacoes).Any()
                    ? g.SelectMany(o => o.Avaliacoes).Average(a => a.Nota)
                    : 0
            })
            .OrderBy(g => g.NomeDiretorEstudio)
            .ToListAsync();

        return View(new ConsultasIndexViewModel
        {
            AnoFiltro = anoFiltro,
            ObrasComDiretores = obrasComDiretores,
            ObrasPorTipo = obrasPorTipo,
            DiretoresComMaisDeUmaObra = diretoresComMaisDeUmaObra
        });
    }
}
