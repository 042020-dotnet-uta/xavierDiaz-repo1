using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectOne.Data;
using ProjectOne.Models;

namespace ProjectOne.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ProjectOneContext _context;

        public CustomersController(ProjectOneContext context)
        {
            _context = context;
        }
        public string SuccessMessage
        {
            get { return TempData["SuccessMessage"] as string; }
            set { TempData["SuccessMessage"] = value; }
        }
        public string FailMessage
        {
            get { return TempData["FailMessage"] as string; }
            set { TempData["FailMessage"] = value; }
        }

        // GET: Customers
        public async Task<IActionResult> Index(string search)
        {
            var customers = from c in _context.Customers
                         select c;
            if (!String.IsNullOrEmpty(search))
            {
                customers = customers.Where(s => s.FName.Contains(search));
            }
            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,FName,LName,DefaultSto")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (CheckInp(customer.DefaultSto))
                {
                    TempData["SuccessMessage"] = "good";
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
                else if(!CheckInp(customer.DefaultSto))
                {
                    TempData["FailMessage"] = "bad";
                    return RedirectToAction("Create");
                }
            }
            return View(customer);
        }
        public bool CheckInp(int store) 
        {
            Location l = new Location();
            return l.IsValidLocation(store);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FName,LName,DefaultSto")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
