using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorRepositoryTests
    {
        //[TestMethod()]
        //public void RetrieveValueIntTest()
        //{
        //    // Arrange
        //    var repository = new VendorRepository();
        //    var expected = 42;

        //    // Act
        //    var actual = repository.RetrieveValue<int>("Select ...", 42);

        //    // Assert
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod()]
        //public void RetrieveValueStringTest()
        //{
        //    // Arrange
        //    var repository = new VendorRepository();
        //    var expected = "Manong";

        //    // Act
        //    var actual = repository.RetrieveValue<string>("Select ...", "Manong");

        //    // Assert
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod()]
        public void RetrieveVendorTest()
        {
            // Arrange
            var repository = new VendorRepository();
            var expected = new List<Vendor>();
            expected.Add(new Vendor()
            { VendorId = 1, CompanyName = "ABC Corp.", Email = "abc@abc.com" });
            expected.Add(new Vendor()
            { VendorId = 2, CompanyName = "XYZ Corp.", Email = "xyz@xyz.com" });

            // Act
            var actual = repository.Retrieve();

            // Assert
            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod()]
        public void RetrieveWithIteratorTest()
        {
            // Arrange
            var repository = new VendorRepository();
            var expected = new List<Vendor>()
            {
                { new Vendor()
                    { VendorId = 1, CompanyName = "ABC Corp.", Email = "abc@abc.com" }
                },
                { new Vendor()
                    { VendorId = 2, CompanyName = "XYZ Corp.", Email = "xyz@xyz.com" }
                }
            };

            // Act
            var vendorIterator = repository.RetrieveWithIterator();
            foreach (var item in vendorIterator)
            {
                Console.WriteLine(item);
            }
            var actual = vendorIterator.ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RetrieveAllTest()
        {
            // Arrange
            var repository = new VendorRepository();
            var expected = new List<Vendor>()
            {
                { new Vendor()
                    { VendorId = 22, CompanyName = "Amalgamated Toys", Email = "a@abc.com" } },
                { new Vendor()
                    { VendorId = 35, CompanyName = "Car Toys", Email = "car@abc.com" } },
                { new Vendor()
                    { VendorId = 28, CompanyName = "Toy Blocks Inc", Email = "blocks@abs.com" } },
                { new Vendor()
                    { VendorId = 42, CompanyName = "Toys for Fun", Email = "fun@abc.com" } }
            };

            // Act
            var vendors = repository.RetrieveAll();

            // Query Syntax
            //var vendorQuery = from v in vendors
            //                  where v.CompanyName.Contains("Toy")
            //                  orderby v.CompanyName
            //                  select v;

            // Method Syntax
            //var vendorQuery = vendors.Where(FilterCompanies)
            //                    .OrderBy(OrderCompaniesByName);

            // Method Syntax with Lambda
            var vendorQuery = vendors.Where(v => v.CompanyName.Contains("Toy"))
                                    .OrderBy(v => v.CompanyName);

            // Assert
            CollectionAssert.AreEqual(expected, vendorQuery.ToList());
        }
        
        // for method syntax
        //private bool FilterCompanies(Vendor v) =>
        //    v.CompanyName.Contains("Toy");

        //private string OrderCompaniesByName(Vendor v) =>
        //    v.CompanyName;

        //[TestMethod()]
        //public void RetrieveWithKeysTest()
        //{
        //    // Arrange
        //    var repository = new VendorRepository();
        //    var expected = new Dictionary<string, Vendor>()
        //    {
        //        { "ABC Corp", new Vendor()
        //            { VendorId = 5, CompanyName = "ABC Corp", Email = "abc@abc.com" } }
        //    };

        //    // Act
        //    var actual = repository.RetrieveWithKeys();

        //    // Assert
        //    CollectionAssert.AreEqual(expected, actual);
        //}
    }
}