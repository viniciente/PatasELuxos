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
    public class FormaPagamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormaPagamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FormaPagamentos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FormarPagamentos.Include(f => f.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FormaPagamentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormarPagamentos
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FormaPagamentoId == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: FormaPagamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormaPagamentoId,UsuarioId,Cartao,Validade,CVV,Senha")] FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                formaPagamento.FormaPagamentoId = Guid.NewGuid();
                _context.Add(formaPagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", formaPagamento.UsuarioId);
            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormarPagamentos.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", formaPagamento.UsuarioId);
            return View(formaPagamento);
        }

        // POST: FormaPagamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FormaPagamentoId,UsuarioId,Cartao,Validade,CVV,Senha")] FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.FormaPagamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formaPagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormaPagamentoExists(formaPagamento.FormaPagamentoId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "UsuarioId", formaPagamento.UsuarioId);
            return View(formaPagamento);
        }

        // GET: FormaPagamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPagamento = await _context.FormarPagamentos
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FormaPagamentoId == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return View(formaPagamento);
        }

        // POST: FormaPagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var formaPagamento = await _context.FormarPagamentos.FindAsync(id);
            if (formaPagamento != null)
            {
                _context.FormarPagamentos.Remove(formaPagamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormaPagamentoExists(Guid id)
        {
            return _context.FormarPagamentos.Any(e => e.FormaPagamentoId == id);
        }
    }
}
