using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    public class VendorRepository
    {
        private List<Vendor> vendors;
        
        /// <summary>
        /// Retrieve one vendor.
        /// </summary>
        public Vendor Retrieve(int vendorId)
        {
            // Create the instance of the Vendor class
            Vendor vendor = new Vendor();

            // Code that retrieves the defined customer

            // Temporary hard coded values to return 
            if (vendorId == 1)
            {
                vendor.VendorId = 1;
                vendor.CompanyName = "ABC Corp";
                vendor.Email = "abc@abc.com";
            }
            return vendor;
        }

        /// <summary>
        /// Retrieve all approved vendors.
        /// </summary>
        public ICollection<Vendor> Retrieve()
        {
            if (vendors == null)
            {
                vendors = new List<Vendor>();

                vendors.Add(new Vendor() 
                    { VendorId = 1, CompanyName = "ABC Corp.", Email = "abc@abc.com" });
                vendors.Add(new Vendor() 
                    { VendorId = 2, CompanyName = "XYZ Corp.", Email = "xyz@xyz.com" });
            }

            foreach (var vendor in vendors)
            {
                Console.WriteLine("DB: " + vendor);
            }
            return vendors;
        }

        public IEnumerable<Vendor> RetrieveAll()
        {
            var vendors = new List<Vendor>()
            {
                { new Vendor()
                    { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
                { new Vendor()
                    { VendorId = 2, CompanyName = "XYZ Corp", Email = "xyz@xyz.com" } },
                { new Vendor()
                    { VendorId = 12, CompanyName = "EFG Ltd", Email = "efg@efg.com" } },
                { new Vendor()
                    { VendorId = 17, CompanyName = "HIJ AG", Email = "hij@hij.com" } },
                { new Vendor()
                    { VendorId = 22, CompanyName = "Amalgamated Toys", Email = "a@abc.com" } },
                { new Vendor()
                    { VendorId = 28, CompanyName = "Toy Blocks Inc", Email = "blocks@abs.com" } },
                { new Vendor()
                    { VendorId = 31, CompanyName = "Home Products Inc", Email = "home@abc.com" } },
                { new Vendor()
                    { VendorId = 35, CompanyName = "Car Toys", Email = "car@abc.com" } },
                { new Vendor()
                    { VendorId = 42, CompanyName = "Toys for Fun", Email = "fun@abc.com" } },
            };
            return vendors;
        }

        public T RetrieveValue<T>(string sql, T defaultValue)
        {
            // Call database to retrieve value
            // If no value returned, return default value
            T value = defaultValue;

            return value;
        }

        //public Dictionary<string, Vendor> RetrieveWithKeys()
        //{
        //    var vendors = new Dictionary<string, Vendor>()
        //    {   
        //        { "ABC Corp", new Vendor()
        //                { VendorId = 5, CompanyName = "ABC Corp", Email = "abc@abc.com" }
        //        }
        //    };

        //    foreach (var v in vendors.Keys)
        //    {
        //        Console.WriteLine(v);
        //        Console.WriteLine(vendors[v]);
        //    }

        //    foreach (var v in vendors)
        //    {
        //        Console.WriteLine(v.Key + " is to " + v.Value);
        //    }

        //    return vendors;
        //}

        public IEnumerable<Vendor> RetrieveWithIterator()
        {
            // get data from DB
            Retrieve();

            foreach (var vendor in vendors)
            {
                Console.WriteLine($"Vendor Id: {vendor.VendorId}");
                yield return vendor;
            }
        }

        public bool Save(Vendor vendor)
        {
            var success = true;

            // Code that saves the vendor

            return success;
        }

    }
}
