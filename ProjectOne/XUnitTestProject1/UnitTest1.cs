using System;
using Xunit;
using ProjectOne.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectOne.Models;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CheckCusts()
        {
            using (var db = new ProjectOneContext())
            {
                var custs = db.Customers
                    .FromSqlRaw("SELECT * FROM Customers")
                    .ToList();
                int val = -1;
                if (custs.Count > 0)
                    val = 1;
                Assert.Equal(1, val);
            }
        }
        [Fact]
        public void ValidProductBad()
        {
            Product p = new Product();
            bool check = p.IsValidProduct("Candy");
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }
        [Fact]
        public void ValidProductGood()
        {
            Product p = new Product();
            bool check = p.IsValidProduct("Donut");
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1, val);
        }
        [Fact]
        public void ValidLocationGood()
        {
            Location l = new Location();
            bool check = l.IsValidLocation(1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1, val);
        }
        [Fact]
        public void ValidLocationBad()
        {
            Location l = new Location();
            bool check = l.IsValidLocation(100);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }
        [Fact]
        public void CheckLocations()
        {
            using (var db = new ProjectOneContext())
            {
                var locs = db.Locations
                    .FromSqlRaw("SELECT * FROM Locations")
                    .ToList();
                int val = -1;
                if (locs.Count > 0)
                    val = 1;
                Assert.Equal(1, val);
            }
        }
        [Fact]
        public void CheckProducts()
        {
            using (var db = new ProjectOneContext())
            {
                var prods = db.Products
                    .FromSqlRaw("SELECT * FROM Products")
                    .ToList();
                int val = -1;
                if (prods.Count > 0)
                    val = 1;
                Assert.Equal(1, val);
            }
        }
        [Fact]
        public void ValidCustomerBad()
        {
            Customer c = new Customer();
            bool check = c.IsValidCustomer(300);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }
        [Fact]
        public void ValidCustomerGood()
        {
            Customer c = new Customer();
            bool check = c.IsValidCustomer(1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1, val);
        }
        [Fact]
        public void ValidLocationQuantityGood()
        {
            Location l = new Location();
            bool check = l.IsValidQuantity(1,"Cookies",1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1,val);
        }
        [Fact]
        public void ValidLocationQuantityBad()
        {
            Location l = new Location();
            bool check = l.IsValidQuantity(10,"Candy",1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1,val);
        }
        [Fact]
        public void ValidProductCookiesIDGood()
        {
            Product p = new Product();
            bool check = p.IsValidProduct(1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1,val);
        }
        [Fact]
        public void ValidProductDonutIDGood()
        {
            Product p = new Product();
            bool check = p.IsValidProduct(2);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1, val);
        }
        [Fact]
        public void ValidProductCakeIDGood()
        {
            Product p = new Product();
            bool check = p.IsValidProduct(3);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(1, val);
        }
        [Fact]
        public void ValidProductIDBad()
        {
            Product p = new Product();
            bool check = p.IsValidProduct(100);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }
        [Fact]
        public void CheckOrders()
        {
            using (var db = new ProjectOneContext())
            {
                var ords = db.Orders
                    .FromSqlRaw("SELECT * FROM Orders")
                    .ToList();
                int val = -1;
                if (ords.Count > 0)
                    val = 1;
                Assert.Equal(1, val);
            }
        }
        [Fact]
        public void CheckCustsXavier()
        {
            using (var db = new ProjectOneContext())
            {
                var custs = db.Customers
                    .FromSqlRaw("SELECT * FROM Customers WHERE FName = 'Xavier'")
                    .ToList();
                int val = -1;
                if (custs.Count > 0)
                    val = 1;
                Assert.Equal(1, val);
            }
        }
        [Fact]
        public void CheckCustsAlphanso()
        {
            using (var db = new ProjectOneContext())
            {
                var custs = db.Customers
                    .FromSqlRaw("SELECT * FROM Customers WHERE FName = 'Alphanso'")
                    .ToList();
                int val = -1;
                if (custs.Count > 0)
                    val = 1;
                Assert.Equal(-1, val);
            }
        }
        [Fact]
        public void ValidLocationQuantityBadStore()
        {
            Location l = new Location();
            bool check = l.IsValidQuantity(1, "Cookies", 20);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }
        [Fact]
        public void ValidLocationQuantityTooMany()
        {
            Location l = new Location();
            bool check = l.IsValidQuantity(1000, "Cookies", 1);
            int val = -1;
            if (check)
                val = 1;
            Assert.Equal(-1, val);
        }


    }
}
