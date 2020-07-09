using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Acme.Biz
{
    /// <summary>
    /// Manages product carried in the inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructors
        public Product()
        {
            //ProductVendor = new Vendor();
            MinimumPrice = .96m;
            Category = "Tools";

            var colorOptions = new List<string>() { "Red", "Blue" };
            colorOptions.Add("Red");
            colorOptions.Add("Espresso");
            colorOptions.Add("White");
            colorOptions.Add("Navy");

            Console.WriteLine("Product created.");
        }

        public Product(string productName, string description, int productId) : this()
        {
            ProductName = productName;
            Description = description;
            ProductId = productId;
            if(ProductName.StartsWith("Bulk"))
            {
                MinimumPrice = 9.99m;
            }
        }
        #endregion

        #region Properties
        private string productName;

        public string ProductName
        {
            get 
            {
                var formattedValue = productName?.Trim();
                return formattedValue; 
            }
            set 
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name must be less than 20 characters";
                }
                else
                {
                    productName = value;
                }
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get 
            { 
                if(productVendor == null )
                {
                    productVendor = new Vendor();
                }
                return productVendor; 
            }
            set { productVendor = value; }
        }

        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;

        public string ValidationMessage { get; private set; }

        //public string ProductCode => Category + "-" + SequenceNumber;
        public string ProductCode => $"{Category}-{SequenceNumber:0000}";

        public decimal Cost { get; set; }
        #endregion

        public string SayHello()
        {
            var vendor = new Vendor();
            vendor.SendWelcomeEmail("Message from Product");

            var emailService = new EmailService();
            emailService.SendMessage("New Product", this.ProductName, "sales@abc.com");

            var result = LogAction("Saying hello.");
            return "Hello " + ProductName +
                " (" + ProductId + "): " +
                Description + " Available on: " +
                AvailabilityDate?.ToShortDateString();
        }

        public override string ToString()
        {
            return ProductName + " (" + ProductId + ") ";
        }

        /// <summary>
        /// Calculates suggested retail price.
        /// </summary>
        /// <param name="markupPrecent">Percent used to mark up the cost.</param>
        /// <returns></returns>
        public OperationResult<decimal> CalculateSuggestedPrice(decimal markupPrecent)
        {
            var message = "";
            if (markupPrecent <= 0m)
            {
                message = "Invalid markup percentage";
            }
            else if (markupPrecent < 10)
            {
                message = "Below recommended markup percentage";
            }

            var value = Cost + (Cost * markupPrecent / 100);
            var operationResultDecimal = new OperationResult<decimal>(value, message);
            var operationResult = operationResultDecimal;
            return operationResult;
        }
    }
}
