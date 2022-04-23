using _01_ShopFaQuery.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _query;

        public InventoryController(IInventoryQuery query)
        {
            _query = query;
        }

        [HttpPost]
        public StockStatus CheckStatus(CheckStock stock)
        {
            return _query.CheckStockStatus(stock);
        }
    }
}
