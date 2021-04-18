using ShopBridge.Business;
using MODELS = ShopBridge.Models;
using System;
using Xunit;
using Rhino.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace ShopBridgeTest
{
    public class TestProduct
    {
        private IProduct _productbdl;
        public TestProduct()
        {
            var services = new ServiceCollection();
            services.AddTransient<IProduct, Product>();
            var serviceProvider = services.BuildServiceProvider();
            _productbdl = serviceProvider.GetService<IProduct>();
        }

        [Fact]
        public void TestAddProductSuccess()
        {
            var product = new MODELS.Product { Id = 0, Name = "GoPro", Description = "Camera", Price = 35000.00, Count = 5 };
            MockRepository.GenerateMock<IProduct>().Expect(x => x.AddProduct(new MODELS.Product())).Return(1);
            var result = _productbdl.AddProduct(product);
            Assert.True(result > 0);
        }
    }
}
