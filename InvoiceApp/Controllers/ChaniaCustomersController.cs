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
    public class ChaniaCustomersController : Controller
    {
        private readonly ChaniaContext _context;

        public ChaniaCustomersController(ChaniaContext context)
        {
            _context = context;
        }

        // GET: ChaniaCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChaniaCustomers.ToListAsync());
        }

        // GET: ChaniaCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaCustomer = await _context.ChaniaCustomers
                .FirstOrDefaultAsync(m => m.ChaniaCustomerID == id);
            if (chaniaCustomer == null)
            {
                return NotFound();
            }

            return View(chaniaCustomer);
        }

        // GET: ChaniaCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChaniaCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChaniaCustomerID,ChaniaFullName,ChaniaTaxID,ChaniaCountryCode,ChaniaPostCode,ChaniaCityName,ChaniaAddress,ChaniaEmail")] ChaniaCustomer chaniaCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chaniaCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chaniaCustomer);
        }

        // GET: ChaniaCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaCustomer = await _context.ChaniaCustomers.FindAsync(id);
            if (chaniaCustomer == null)
            {
                return NotFound();
            }
            return View(chaniaCustomer);
        }

        // POST: ChaniaCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChaniaCustomerID,ChaniaFullName,ChaniaTaxID,ChaniaCountryCode,ChaniaPostCode,ChaniaCityName,ChaniaAddress,ChaniaEmail")] ChaniaCustomer chaniaCustomer)
        {
            if (id != chaniaCustomer.ChaniaCustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chaniaCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChaniaCustomerExists(chaniaCustomer.ChaniaCustomerID))
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
            return View(chaniaCustomer);
        }

        // GET: ChaniaCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chaniaCustomer = await _context.ChaniaCustomers
                .FirstOrDefaultAsync(m => m.ChaniaCustomerID == id);
            if (chaniaCustomer == null)
            {
                return NotFound();
            }

            return View(chaniaCustomer);
        }

        // POST: ChaniaCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chaniaCustomer = await _context.ChaniaCustomers.FindAsync(id);
            if (chaniaCustomer != null)
            {
                _context.ChaniaCustomers.Remove(chaniaCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChaniaCustomerExists(int id)
        {
            return _context.ChaniaCustomers.Any(e => e.ChaniaCustomerID == id);
        }
    }
}
