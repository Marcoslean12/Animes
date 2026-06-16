using CineAnimeWeb.Data;
using CineAnimeWeb.Models;
using CineAnimeWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Controllers;

public class ObrasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ObrasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var obras = await _context.Obras
            .Include(o => o.DiretorEstudio)
            .Include(o => o.ObraGeneros)
            .ThenInclude(og => og.Genero)
            .Include(o => o.Avaliacoes)
            .OrderBy(o => o.Titulo)
            .ToListAsync();

        return View(obras);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var obra = await _context.Obras
            .Include(o => o.DiretorEstudio)
            .Include(o => o.ObraGeneros)
            .ThenInclude(og => og.Genero)
            .Include(o => o.Avaliacoes)
            .FirstOrDefaultAsync(m => m.Id == id);

        return obra == null ? NotFound() : View(obra);
    }

    public async Task<IActionResult> Create()
    {
        return View(await CriarViewModelAsync(new ObraFormViewModel { AnoLancamento = DateTime.Today.Year, DuracaoMinutos = 90 }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ObraFormViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(await CriarViewModelAsync(viewModel));

        var obra = new Obra
        {
            Titulo = viewModel.Titulo,
            Tipo = viewModel.Tipo,
            AnoLancamento = viewModel.AnoLancamento,
            DuracaoMinutos = viewModel.DuracaoMinutos,
            Sinopse = viewModel.Sinopse,
            DiretorEstudioId = viewModel.DiretorEstudioId,
            ObraGeneros = viewModel.GenerosSelecionados.Select(generoId => new ObraGenero { GeneroId = generoId }).ToList()
        };

        _context.Add(obra);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var obra = await _context.Obras.Include(o => o.ObraGeneros).FirstOrDefaultAsync(o => o.Id == id);
        if (obra == null) return NotFound();

        var viewModel = new ObraFormViewModel
        {
            Id = obra.Id,
            Titulo = obra.Titulo,
            Tipo = obra.Tipo,
            AnoLancamento = obra.AnoLancamento,
            DuracaoMinutos = obra.DuracaoMinutos,
            Sinopse = obra.Sinopse,
            DiretorEstudioId = obra.DiretorEstudioId,
            GenerosSelecionados = obra.ObraGeneros.Select(og => og.GeneroId).ToList()
        };

        return View(await CriarViewModelAsync(viewModel));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ObraFormViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();
        if (!ModelState.IsValid) return View(await CriarViewModelAsync(viewModel));

        var obra = await _context.Obras.Include(o => o.ObraGeneros).FirstOrDefaultAsync(o => o.Id == id);
        if (obra == null) return NotFound();

        obra.Titulo = viewModel.Titulo;
        obra.Tipo = viewModel.Tipo;
        obra.AnoLancamento = viewModel.AnoLancamento;
        obra.DuracaoMinutos = viewModel.DuracaoMinutos;
        obra.Sinopse = viewModel.Sinopse;
        obra.DiretorEstudioId = viewModel.DiretorEstudioId;

        _context.ObraGeneros.RemoveRange(obra.ObraGeneros);
        obra.ObraGeneros = viewModel.GenerosSelecionados.Select(generoId => new ObraGenero
        {
            ObraId = obra.Id,
            GeneroId = generoId
        }).ToList();

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var obra = await _context.Obras
            .Include(o => o.DiretorEstudio)
            .Include(o => o.ObraGeneros)
            .ThenInclude(og => og.Genero)
            .FirstOrDefaultAsync(m => m.Id == id);

        return obra == null ? NotFound() : View(obra);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var obra = await _context.Obras.FindAsync(id);
        if (obra != null)
        {
            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<ObraFormViewModel> CriarViewModelAsync(ObraFormViewModel viewModel)
    {
        var diretores = await _context.DiretorEstudios.OrderBy(d => d.Nome).ToListAsync();
        var generos = await _context.Generos.OrderBy(g => g.Nome).ToListAsync();

        viewModel.DiretoresEstudios = new SelectList(diretores, "Id", "Nome", viewModel.DiretorEstudioId);
        viewModel.Generos = new MultiSelectList(generos, "Id", "Nome", viewModel.GenerosSelecionados);

        return viewModel;
    }
}
