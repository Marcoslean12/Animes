using CineAnimeWeb.Data;
using CineAnimeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Controllers;

public class GenerosController : Controller
{
    private readonly ApplicationDbContext _context;

    public GenerosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Generos.Include(g => g.ObraGeneros).OrderBy(g => g.Nome).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var genero = await _context.Generos
            .Include(g => g.ObraGeneros)
            .ThenInclude(og => og.Obra)
            .FirstOrDefaultAsync(m => m.Id == id);

        return genero == null ? NotFound() : View(genero);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genero genero)
    {
        if (!ModelState.IsValid) return View(genero);

        _context.Add(genero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var genero = await _context.Generos.FindAsync(id);
        return genero == null ? NotFound() : View(genero);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genero genero)
    {
        if (id != genero.Id) return NotFound();
        if (!ModelState.IsValid) return View(genero);

        _context.Update(genero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var genero = await _context.Generos
            .Include(g => g.ObraGeneros)
            .FirstOrDefaultAsync(m => m.Id == id);

        return genero == null ? NotFound() : View(genero);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genero = await _context.Generos.Include(g => g.ObraGeneros).FirstOrDefaultAsync(g => g.Id == id);
        if (genero == null) return RedirectToAction(nameof(Index));

        if (genero.ObraGeneros.Any())
        {
            ModelState.AddModelError(string.Empty, "Nao e possivel excluir um genero relacionado a obras.");
            return View(genero);
        }

        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
