using CineAnimeWeb.Data;
using CineAnimeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineAnimeWeb.Controllers;

public class DiretorEstudiosController : Controller
{
    private readonly ApplicationDbContext _context;

    public DiretorEstudiosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.DiretorEstudios.Include(d => d.Obras).OrderBy(d => d.Nome).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var diretorEstudio = await _context.DiretorEstudios
            .Include(d => d.Obras)
            .FirstOrDefaultAsync(m => m.Id == id);

        return diretorEstudio == null ? NotFound() : View(diretorEstudio);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DiretorEstudio diretorEstudio)
    {
        if (!ModelState.IsValid) return View(diretorEstudio);

        _context.Add(diretorEstudio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var diretorEstudio = await _context.DiretorEstudios.FindAsync(id);
        return diretorEstudio == null ? NotFound() : View(diretorEstudio);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DiretorEstudio diretorEstudio)
    {
        if (id != diretorEstudio.Id) return NotFound();
        if (!ModelState.IsValid) return View(diretorEstudio);

        _context.Update(diretorEstudio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var diretorEstudio = await _context.DiretorEstudios
            .Include(d => d.Obras)
            .FirstOrDefaultAsync(m => m.Id == id);

        return diretorEstudio == null ? NotFound() : View(diretorEstudio);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var diretorEstudio = await _context.DiretorEstudios.Include(d => d.Obras).FirstOrDefaultAsync(d => d.Id == id);
        if (diretorEstudio == null) return RedirectToAction(nameof(Index));

        if (diretorEstudio.Obras.Any())
        {
            ModelState.AddModelError(string.Empty, "Nao e possivel excluir um diretor/estudio com obras cadastradas.");
            return View(diretorEstudio);
        }

        _context.DiretorEstudios.Remove(diretorEstudio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
