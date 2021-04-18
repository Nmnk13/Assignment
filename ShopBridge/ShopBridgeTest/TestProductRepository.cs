using Microsoft.Extensions.DependencyInjection;
using ShopBridge.DataAccess;
using ShopBridge.Models;
using System;
using Xunit;

namespace ShopBridgeTest
{
    public class TestProductRepository
    {
        private IProductRepository _productRepository;
        public TestProductRepository()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProductRepository, ProductRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _productRepository = serviceProvider.GetService<IProductRepository>();
        }

        [Fact]
        public void TestAddProductSuccess()
        {
            var product = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };          
            var result = _productRepository.AddProduct(product);
            Assert.True(result > 0);
        }

        [Fact]
        public void TestAddProductErr()
        {
            var product = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            var result = _productRepository.AddProduct(product);
            Assert.True(result == 0);
        }

        [Fact]
        public void TestGetProductSuccess()
        {
            var product1 = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            var product2= new Product { Id = 0, Name = "Iphone", Description = "Mobile", Price = 80000.00, Count = 10};
            _productRepository.AddProduct(product1);
            _productRepository.AddProduct(product2);
            var result = _productRepository.GetAll();
            Assert.True(result.Count > 0);
            Assert.Equal("GoPro", result[0].Name);
            Assert.Equal(80000.00, result[1].Price);
        }

        [Fact]
        public void TestGetProductErr()
        {
            var result = _productRepository.GetAll();
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void TestUpdateProductSuccess()
        {
            var product = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            _productRepository.AddProduct(product);
            var getProduct = _productRepository.GetAll();
            getProduct[0].Description = "New Camera";
            getProduct[0].Price = 40000.00;          
            var result = _productRepository.UpdateProduct(getProduct[0]);
            getProduct = _productRepository.GetAll();
            Assert.True(result == 1);
            Assert.NotEqual("GoPro", getProduct[0].Name);
            Assert.Equal(40000.00, getProduct[0].Price);
            Assert.Equal(product.Count, getProduct[0].Count);
        }

        [Fact]
        public void TestUpdateProductErr()
        {
            var product = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            _productRepository.AddProduct(product);
            var getProduct = _productRepository.GetAll();
            getProduct[0].Description = "New Camera";
            getProduct[0].Price = 40000.00;
            var result = _productRepository.UpdateProduct(getProduct[0]);
            getProduct = _productRepository.GetAll();
            Assert.True(result == 0);
            Assert.Equal("GoPro", getProduct[0].Name);
        }

        [Fact]
        public void TestDeleteProductSuccess()
        {
            var product = new Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            _productRepository.AddProduct(product);
            var getProduct = _productRepository.GetAll();
            var result = _productRepository.DeleteProduct(getProduct[0]);
            Assert.True(result == 1);            
        }

    }
}
