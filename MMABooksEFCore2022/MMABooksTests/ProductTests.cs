using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        MMABooksContext dbContext;
        Product? p;
        List<Product>? products;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            products = dbContext.Products.OrderBy(p => p.Description).ToList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("DB1R", products[0].ProductCode);
            PrintAll(products);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            p = dbContext.Products.Find("DB1R");
            Assert.IsNotNull(p);
            Assert.AreEqual("DB2 for the COBOL Programmer, Part 1 (2nd Edition)", p.Description);
            Console.WriteLine(p);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the products that have a unit price of 56.50
            products = dbContext.Products.Where(p => p.UnitPrice.Equals(56.5000m)).OrderBy(p => p.Description).ToList();
            Assert.AreEqual(7, products.Count);
            Assert.AreEqual("Murach's ADO.NET 4 with C# 2010", products[0].Description);
            PrintAll(products);
        }

        [Test]
        public void GetWithCalculatedFieldTest()
        {
            // get a list of objects that include the productcode, unitprice, quantity and inventoryvalue
            var products = dbContext.Products.Select(
            p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
            OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        [Test]
        public void DeleteTest()
        {
            p = dbContext.Products.Find("ADV4");
            List<Invoicelineitem> invoicelineitems = dbContext.Invoicelineitems.Where(i => i.ProductCode == "ADV4").ToList();
            foreach (var item in invoicelineitems)
            {
                dbContext.Invoicelineitems.Remove(item);
            }
            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Products.Find("ADV4"));
        }

        [Test]
        public void CreateTest()
        {
            p = new Product();
            p.ProductCode = "CODE";
            p.Description = "A book.";
            p.UnitPrice = 0.0100m;
            p.OnHandQuantity = 1;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Products.Find("CODE"));
        }

        [Test]
        public void UpdateTest()
        {
            p = dbContext.Products.Find("ADV4");
            p.Description = "A different description.";
            dbContext.Products.Update(p);
            dbContext.SaveChanges();
            p = dbContext.Products.Find("ADV4");
            Assert.AreEqual("A different description.", p.Description);
        }
        private void PrintAll(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine(p);
            }
        }
        [TearDown]
        public void TearDown()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }
    }
}