using Microsoft.AspNetCore.Mvc;
using ShopBridge.Business;
using MODELS = ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Logging;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _bdlProduct;

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IProduct bdlProduct)
        {
            _logger = logger;
            _bdlProduct = bdlProduct;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = _bdlProduct.GetAll();
                return new OkObjectResult(results);
            }
            catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddProduct(MODELS.Product product)
        {
            var success = _bdlProduct.AddProduct(product);
            if(success == 1)
            {
                return StatusCode((int)HttpStatusCode.Created, 1);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }                     
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateProduct(MODELS.Product product)
        {
            var success = _bdlProduct.UpdateProduct(product);
            if (success == 1)
            {
                return StatusCode((int)HttpStatusCode.Created, success);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteProduct(MODELS.Product product)
        {
            var success = _bdlProduct.DeleteProduct(product);
            if (success == 1)
            {
                return StatusCode((int)HttpStatusCode.Created, 1);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }
    }
}
