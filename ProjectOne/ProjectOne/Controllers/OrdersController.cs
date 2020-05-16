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
    public class OrdersController : Controller
    {
        private readonly ProjectOneContext _context;

        public OrdersController(ProjectOneContext context)
        {
            _context = context;
        }
        //idea from stack overflow
        //How to open a dialog box after submit a form to show posted successfully - asp.net MVC
        public string SuccessMessage
        {
            get { return TempData["SuccessMessage"] as string; }
            set { TempData["SuccessMessage"] = value; }
        }

        // GET: Orders
        public async Task<IActionResult> Index(string searchString)
        {
            var orders = from o in _context.Orders
                         select o;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.SoldItems.Contains(searchString));
            }

            return View(await orders.ToListAsync());
        }
        public async Task<IActionResult> SearchCustomer(int custID = -1)
        {
            var orders = from o in _context.Orders
                         select o;
            if (custID != -1)
            {
                orders = orders.Where(s => s.CustomerID == custID);
            }

            return View(await orders.ToListAsync());
        }
        public async Task<IActionResult> SearchStore(int storeID = -1)
        {
            var orders = from o in _context.Orders
                         select o;
            if (storeID != -1)
            {
                orders = orders.Where(s => s.StoreID == storeID);
            }

            return View(await orders.ToListAsync());
        }
        public async Task<IActionResult> SearchOrdID(int ordID = -1)
        {
            var orders = from o in _context.Orders
                         select o;
            if (ordID != -1)
            {
                orders = orders.Where(s => s.ID == ordID);
            }

            return View(await orders.ToListAsync());
        }
        //can overload this later for different searches?
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StoreID,CustomerID,SellTime,SoldItems")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                SuccessMessage = "Order Placed";
                return RedirectToAction("Create");
                //return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StoreID,CustomerID,SellTime,SoldItems")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
