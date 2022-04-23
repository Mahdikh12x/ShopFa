using _01_ShopFaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet]
        public List<ProductQueryModel> GetArrivals()
        {
            return _productQuery.GetArrivalsProducts();
        }
    
    }
}
