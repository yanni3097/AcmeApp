﻿using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public enum IncludeAddress {  Yes, No };
        public enum SendCopy {  Yes, No };

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Sends a product order to vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="deliverBy">Requested delivery date.</param>
        /// <param name="instructions">Delivery instructions.</param>
        /// <returns>bool</returns>
        public OperationResult<bool> PlaceOrder(Product product, int quantity, 
                                            DateTimeOffset? deliverBy = null, 
                                            string instructions = "Standard delivery")
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" + System.Environment.NewLine +
                "Product: " + product.ProductCode + System.Environment.NewLine +
                "Quantity: " + quantity);

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                    "Deliver by: " + deliverBy.Value.ToString("d"));
            }

            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                    "Instructions: " + instructions);
            }


            var orderText = orderTextBuilder.ToString();
            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }

            var operationResult = new OperationResult<bool>(success, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends a product to the vendor.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="includeAddress"></param>
        /// <param name="sendCopy"></param>
        /// <returns>Result flag and order text</returns>
        public OperationResult<bool> PlaceOrder(Product product, int quantity,
                                            IncludeAddress includeAddress, 
                                            SendCopy sendCopy)
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " With Address";
            if (sendCopy == SendCopy.Yes) orderText += " With Copy";

            var operationResult = new OperationResult<bool>(true, orderText);
            return operationResult;
        }

        public override string ToString()
        {
            string vendorInfo = "Vendor: " + CompanyName;
            string result;
            result = vendorInfo?.ToLower();
            result = vendorInfo?.ToUpper();
            result = vendorInfo?.Replace("Vendor", "Supplier");

            var length = vendorInfo?.Length;
            var index = vendorInfo?.IndexOf(":");
            var begins = vendorInfo?.StartsWith("Vendor");

            return vendorInfo;
        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define a new line";
            return directions;
        }

        public string PrepareDirectionsOnTwoLines()
        {
            var directions = "First do this" + Environment.NewLine +
                                "Then do that";
            return directions;
        }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }

        /// <summary>
        /// Sends an email to welcome a set of vendors.
        /// </summary>
        /// <returns></returns>
        public static List<string> SendEmail(ICollection<Vendor> vendors, string message)
        {
            var confirmations = new List<string>();
            var emailService = new EmailService();
            Console.WriteLine(vendors.Count);

            foreach (var vendor in vendors)
            {
                var subject = ("Hello " + vendor.CompanyName).Trim();
                var confirmation = emailService.SendMessage(subject,
                                                            message,
                                                            vendor.Email);
                confirmations.Add(confirmation);
            }
            return confirmations;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            Vendor compareVendor = obj as Vendor;
            if (compareVendor != null &&
                VendorId == compareVendor.VendorId &&
                CompanyName == compareVendor.CompanyName &&
                Email == compareVendor.Email)
            {
                return true;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
