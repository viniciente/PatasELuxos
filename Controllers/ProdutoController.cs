﻿using System;
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
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,NomeProduto,Descricao,Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.ProdutoId = Guid.NewGuid();
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProdutoId,NomeProduto,Descricao,Preco")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
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
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
