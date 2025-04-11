using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PataseLuxos.Data;
using PataseLuxos.Models;

namespace PataseLuxos.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veterinario.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.VeterinarioId == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeterinarioId,Nome,CPF,DataNascimento,Email,Celular,CEP")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                veterinario.VeterinarioId = Guid.NewGuid();
                _context.Add(veterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VeterinarioId,Nome,CPF,DataNascimento,Email,Celular,CEP")] Veterinario veterinario)
        {
            if (id != veterinario.VeterinarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.VeterinarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.VeterinarioId == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var veterinario = await _context.Veterinario.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinario.Remove(veterinario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(Guid id)
        {
            return _context.Veterinario.Any(e => e.VeterinarioId == id);
        }
    }
}
