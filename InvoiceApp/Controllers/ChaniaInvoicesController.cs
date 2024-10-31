using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceApp;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    public class ChaniaInvoicesController : Controller
    {
        private readonly ChaniaContext _context;

        public ChaniaInvoicesController(ChaniaContext context)
        {
            _context = context;
        }

        // GET: ChaniaInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChaniaInvoices.ToListAsync());
        }

        // GET: ChaniaInvoices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaInvoice = await _context.ChaniaInvoices
                .FirstOrDefaultAsync(m => m.ChaniaDocID == id);
            if (chaniaInvoice == null)
            {
                return NotFound();
            }

            return View(chaniaInvoice);
        }

        // GET: ChaniaInvoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChaniaInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChaniaDocID,ChaniaDocDate,ChaniaDocType,ChaniaDocNumber,ChaniaIsReversal,ChaniaDocAmount,ChaniaDocCurrency")] ChaniaInvoice chaniaInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chaniaInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chaniaInvoice);
        }

        // GET: ChaniaInvoices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaInvoice = await _context.ChaniaInvoices.FindAsync(id);
            if (chaniaInvoice == null)
            {
                return NotFound();
            }
            return View(chaniaInvoice);
        }

        // POST: ChaniaInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ChaniaDocID,ChaniaDocDate,ChaniaDocType,ChaniaDocNumber,ChaniaIsReversal,ChaniaDocAmount,ChaniaDocCurrency")] ChaniaInvoice chaniaInvoice)
        {
            if (id != chaniaInvoice.ChaniaDocID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chaniaInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChaniaInvoiceExists(chaniaInvoice.ChaniaDocID))
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
            return View(chaniaInvoice);
        }

        // GET: ChaniaInvoices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaInvoice = await _context.ChaniaInvoices
                .FirstOrDefaultAsync(m => m.ChaniaDocID == id);
            if (chaniaInvoice == null)
            {
                return NotFound();
            }

            return View(chaniaInvoice);
        }

        // POST: ChaniaInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chaniaInvoice = await _context.ChaniaInvoices.FindAsync(id);
            if (chaniaInvoice != null)
            {
                _context.ChaniaInvoices.Remove(chaniaInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChaniaInvoiceExists(string id)
        {
            return _context.ChaniaInvoices.Any(e => e.ChaniaDocID == id);
        }
    }
}
