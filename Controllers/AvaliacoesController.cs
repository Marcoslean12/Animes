using CineAnimeWeb.Data;
using CineAnimeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Controllers;

public class AvaliacoesController : Controller
{
    private readonly ApplicationDbContext _context;

    public AvaliacoesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Avaliacoes.Include(a => a.Obra).OrderByDescending(a => a.DataAvaliacao).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var avaliacao = await _context.Avaliacoes.Include(a => a.Obra).FirstOrDefaultAsync(m => m.Id == id);
        return avaliacao == null ? NotFound() : View(avaliacao);
    }

    public async Task<IActionResult> Create(int? obraId)
    {
        var avaliacao = new Avaliacao { DataAvaliacao = DateTime.Today };
        if (obraId.HasValue) avaliacao.ObraId = obraId.Value;
        await CarregarObrasAsync(avaliacao.ObraId);
        return View(avaliacao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Avaliacao avaliacao)
    {
        if (!ModelState.IsValid)
        {
            await CarregarObrasAsync(avaliacao.ObraId);
            return View(avaliacao);
        }

        _context.Add(avaliacao);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "Obras", new { id = avaliacao.ObraId });
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var avaliacao = await _context.Avaliacoes.FindAsync(id);
        if (avaliacao == null) return NotFound();

        await CarregarObrasAsync(avaliacao.ObraId);
        return View(avaliacao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Avaliacao avaliacao)
    {
        if (id != avaliacao.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            await CarregarObrasAsync(avaliacao.ObraId);
            return View(avaliacao);
        }

        _context.Update(avaliacao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var avaliacao = await _context.Avaliacoes.Include(a => a.Obra).FirstOrDefaultAsync(m => m.Id == id);
        return avaliacao == null ? NotFound() : View(avaliacao);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var avaliacao = await _context.Avaliacoes.FindAsync(id);
        var obraId = avaliacao?.ObraId;

        if (avaliacao != null)
        {
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
        }

        return obraId.HasValue ? RedirectToAction("Details", "Obras", new { id = obraId.Value }) : RedirectToAction(nameof(Index));
    }

    private async Task CarregarObrasAsync(int? obraId = null)
    {
        var obras = await _context.Obras.OrderBy(o => o.Titulo).ToListAsync();
        ViewData["ObraId"] = new SelectList(obras, "Id", "Titulo", obraId);
    }
}
