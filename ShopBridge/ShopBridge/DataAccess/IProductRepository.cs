using System;
using System.Collections.Generic;
using MODELS = ShopBridge.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataAccess
{
    public interface IProductRepository
    {
        int AddProduct(MODELS.Product product);
        int UpdateProduct(MODELS.Product product);
        int DeleteProduct(MODELS.Product product);
        List<MODELS.Product> GetAll();
    }
}
