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
    public class CarrinhoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carrinhoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carrinhos.Include(c => c.FormaPagamento).Include(c => c.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carrinhoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos
                .Include(c => c.FormaPagamento)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CarrinhoId == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        // GET: Carrinhoes/Create
        public IActionResult Create()
        {
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormarPagamentos, "FormaPagamentoId", "FormaPagamentoId");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "NomeProduto");
            return View();
        }

        // POST: Carrinhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarrinhoId,ProdutoId,FormaPagamentoId")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                carrinho.CarrinhoId = Guid.NewGuid();
                _context.Add(carrinho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormarPagamentos, "FormaPagamentoId", "FormaPagamentoId", carrinho.FormaPagamentoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "NomeProduto", carrinho.ProdutoId);
            return View(carrinho);
        }

        // GET: Carrinhoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormarPagamentos, "FormaPagamentoId", "FormaPagamentoId", carrinho.FormaPagamentoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "NomeProduto", carrinho.ProdutoId);
            return View(carrinho);
        }

        // POST: Carrinhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarrinhoId,ProdutoId,FormaPagamentoId")] Carrinho carrinho)
        {
            if (id != carrinho.CarrinhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrinho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrinhoExists(carrinho.CarrinhoId))
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
            ViewData["FormaPagamentoId"] = new SelectList(_context.FormarPagamentos, "FormaPagamentoId", "FormaPagamentoId", carrinho.FormaPagamentoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "NomeProduto", carrinho.ProdutoId);
            return View(carrinho);
        }

        // GET: Carrinhoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos
                .Include(c => c.FormaPagamento)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CarrinhoId == id);
            if (carrinho == null)
            {
                return NotFound();
            }

            return View(carrinho);
        }

        // POST: Carrinhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);
            if (carrinho != null)
            {
                _context.Carrinhos.Remove(carrinho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrinhoExists(Guid id)
        {
            return _context.Carrinhos.Any(e => e.CarrinhoId == id);
        }
    }
}
