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
        public static List<Order> Orders = new List<Order>();
        //idea from stack overflow
        //How to open a dialog box after submit a form to show posted successfully - asp.net MVC
        public string SuccessMessage
        {
            get { return TempData["SuccessMessage"] as string; }
            set { TempData["SuccessMessage"] = value; }
        }
        public string OrderSize
        {
            get { return TempData["OrderSize"] as string; }
            set { TempData["SucessMessage"] = value; }
        }
        public string Error
        {
            get { return TempData["Error"] as string; }
            set { TempData["Error"] = value; }
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
        public async Task<IActionResult> Create([Bind("ID,StoreID,CustomerID,SellTime,SoldItems,Quantity")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.SellTime = DateTime.Now;
                int check = validateOrder(order);
                if (check == 0)
                {
                    Orders.Add(order);
                    TempData["OrderSize"] = Orders.Count.ToString();
                    //_context.Add(order);
                    //await _context.SaveChangesAsync();
                    SuccessMessage = "Order Placed";
                    return RedirectToAction("Create");
                }
                else if(check == 1)
                {
                    TempData["Error"] = "Invalid storeID, try again";
                    return RedirectToAction("Create");
                }
                else if (check == 2)
                {
                    TempData["Error"] = "Invalid CustomerID, try again";
                    return RedirectToAction("Create");
                }
                else if (check == 3)
                {
                    TempData["Error"] = "Invalid Item, try again";
                    return RedirectToAction("Create");
                }
                else if (check == 4)
                {
                    TempData["Error"] = "Either not enough of that item or quantity asked for was smaller than 1";
                    return RedirectToAction("Create");
                }

                //return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        /// <summary>
        /// checks all input and returns code for any errors
        /// </summary>
        private int validateOrder(Order order)
        {
            // 0-good | 1-invalid store | 2-invalid customer | 3-invalid Item |4-invalid Quantity
            Location l = new Location();
            if (!l.IsValidLocation(order.StoreID))
                return 1;
            Customer c = new Customer();
            if (!c.IsValidCustomer(order.CustomerID))
                return 2;
            Product p = new Product();
            if (!p.IsValidProduct(order.SoldItems))
                return 3;
            if (!l.IsValidQuantity(order.Quantity, order.SoldItems, order.StoreID))
                return 4;
            return 0;
        }
        /// <summary>
        /// called in Razor to place orders when ready
        /// </summary>
        public IActionResult addOrders()
        {
            foreach(var o in Orders)
            {
                _context.Add(o);
                Location l = new Location();
                l.UpdateInventory(o);
            }
            _context.SaveChanges();
            Orders.Clear();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,StoreID,CustomerID,SellTime,SoldItems,Quantity")] Order order)
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
