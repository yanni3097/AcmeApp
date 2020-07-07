﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
                " Available on: ";

            // Act
            var actual = currentProduct.SayHello();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHelloTest_ParameterizedConstructor()
        {
            // Arrange
            var currentProduct = new Product("Saw", "15-inch steel blade hand saw", 1);
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
                " Available on: ";

            // Act
            var actual = currentProduct.SayHello();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_ObjectInitializer()
        {
            // Arrange
            var currentProduct = new Product
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "15-inch steel blade hand saw"
            };
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
                " Available on: ";

            // Act
            var actual = currentProduct.SayHello();

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void Product_Null()
        {
            // Arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName;
            string expected = null;

            // Act
            var actual = companyName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertMetersToInches()
        {
            // Arrange
            var expected = 78.74;

            // Act
            var actual = Product.InchesPerMeter * 2;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MinimumPrices_Default()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = .96m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MinimumPrices_Bulk()
        {
            // Arrange
            var currentProduct = new Product("Bulk tools", "", 1);
            var expected = 9.99m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductName_Format()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "     Steel Hammer   ";
            var expected = "Steel Hammer";

            // Act
            var actual = currentProduct.ProductName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductName_TooShort()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "aw";
            string expected = null;
            string expectedMessage = "Product Name must be at least 3 characters";

            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void ProductName_TooLog()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Steel Hammer and Wooden Saw";
            string expected = null;
            string expectedMessage = "Product Name must be less than 20 characters";

            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void ProductName_CorrectSize()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Steel Hammer";
            string expected = "Steel Hammer";
            string expectedMessage = null;

            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void Category_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = "Tools";

            // Act
            var actual = currentProduct.Category;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Category_NewValue()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Category = "Toolbox";
            var expected = "Toolbox";

            // Act
            var actual = currentProduct.Category;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SequenceNumber_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = 1;

            // Act
            var actual = currentProduct.SequenceNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SequenceNumber_NewValue()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.SequenceNumber = 69;
            var expected = 69;

            // Act
            var actual = currentProduct.SequenceNumber;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductCode_DefaultValue()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.Category = "Tools";
            currentProduct.SequenceNumber = 1;
            var expected = "Tools-0001";

            // Act
            var actual = currentProduct.ProductCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            // Arrange
            var currentProduct = new Product("Saw", "", 1);
            currentProduct.Cost = 50m;
            var expected = 55m;

            // Act
            var actual = currentProduct.CalculateSuggestedPrice(10m);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}