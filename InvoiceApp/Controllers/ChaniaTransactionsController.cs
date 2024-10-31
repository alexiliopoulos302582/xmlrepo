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
    public class ChaniaTransactionsController : Controller
    {
        private readonly ChaniaContext _context;

        public ChaniaTransactionsController(ChaniaContext context)
        {
            _context = context;
        }

        // GET: ChaniaTransactions
        public async Task<IActionResult> Index()
        {
            var chaniaContext = _context.ChaniaTransactions.Include(c => c.ChaniaInvoice);
            return View(await chaniaContext.ToListAsync());
        }

        // GET: ChaniaTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaTransaction = await _context.ChaniaTransactions
                .Include(c => c.ChaniaInvoice)
                .FirstOrDefaultAsync(m => m.ChaniaTransactionId == id);
            if (chaniaTransaction == null)
            {
                return NotFound();
            }

            return View(chaniaTransaction);
        }

        // GET: ChaniaTransactions/Create
        public IActionResult Create()
        {
            ViewData["ChaniaDocID"] = new SelectList(_context.ChaniaInvoices, "ChaniaDocID", "ChaniaDocID");
            return View();
        }

        // POST: ChaniaTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChaniaTransactionId,ChaniaDocID,ChaniaAmount,ChaniaCurrency,ChaniaDescription,ChaniaGLAccount,ChaniaTranType")] ChaniaTransaction chaniaTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chaniaTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChaniaDocID"] = new SelectList(_context.ChaniaInvoices, "ChaniaDocID", "ChaniaDocID", chaniaTransaction.ChaniaDocID);
            return View(chaniaTransaction);
        }

        // GET: ChaniaTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaTransaction = await _context.ChaniaTransactions.FindAsync(id);
            if (chaniaTransaction == null)
            {
                return NotFound();
            }
            ViewData["ChaniaDocID"] = new SelectList(_context.ChaniaInvoices, "ChaniaDocID", "ChaniaDocID", chaniaTransaction.ChaniaDocID);
            return View(chaniaTransaction);
        }

        // POST: ChaniaTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChaniaTransactionId,ChaniaDocID,ChaniaAmount,ChaniaCurrency,ChaniaDescription,ChaniaGLAccount,ChaniaTranType")] ChaniaTransaction chaniaTransaction)
        {
            if (id != chaniaTransaction.ChaniaTransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chaniaTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChaniaTransactionExists(chaniaTransaction.ChaniaTransactionId))
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
            ViewData["ChaniaDocID"] = new SelectList(_context.ChaniaInvoices, "ChaniaDocID", "ChaniaDocID", chaniaTransaction.ChaniaDocID);
            return View(chaniaTransaction);
        }

        // GET: ChaniaTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaTransaction = await _context.ChaniaTransactions
                .Include(c => c.ChaniaInvoice)
                .FirstOrDefaultAsync(m => m.ChaniaTransactionId == id);
            if (chaniaTransaction == null)
            {
                return NotFound();
            }

            return View(chaniaTransaction);
        }

        // POST: ChaniaTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chaniaTransaction = await _context.ChaniaTransactions.FindAsync(id);
            if (chaniaTransaction != null)
            {
                _context.ChaniaTransactions.Remove(chaniaTransaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChaniaTransactionExists(int id)
        {
            return _context.ChaniaTransactions.Any(e => e.ChaniaTransactionId == id);
        }
    }
}
