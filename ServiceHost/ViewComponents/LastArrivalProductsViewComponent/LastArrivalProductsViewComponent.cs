using _01_ShopFaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.LastArrivalProductsViewComponent
{
    public class LastArrivalProductsViewComponent:ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LastArrivalProductsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetArrivalsProducts();
            return View(products);
        }
    }
}
