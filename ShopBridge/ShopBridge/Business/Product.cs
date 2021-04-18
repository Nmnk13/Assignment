using System;
using System.Collections.Generic;
using MODELS = ShopBridge.Models;
using System.Linq;
using System.Threading.Tasks;
using ShopBridge.DataAccess;

namespace ShopBridge.Business
{
    public class Product : IProduct
    {
        private readonly IProductRepository _productRepository;
        public Product(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        int IProduct.AddProduct(MODELS.Product product)
        {
            try
            {
                if (ValidateProduct(product))
                {
                    return _productRepository.AddProduct(product);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        int IProduct.DeleteProduct(MODELS.Product product)
        {
            return _productRepository.DeleteProduct(product);
        }

        List<MODELS.Product> IProduct.GetAll()
        {
            return _productRepository.GetAll();
        }

        int IProduct.UpdateProduct(MODELS.Product product)
        {
            try
            {
                if (ValidateProduct(product))
                {
                    return _productRepository.UpdateProduct(product);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        bool ValidateProduct(MODELS.Product product)
        {
            if(string.IsNullOrWhiteSpace(product.Name) ||string.IsNullOrWhiteSpace(product.Description) ||
                product.Price <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
