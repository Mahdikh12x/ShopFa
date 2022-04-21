using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public List<OrderViewModel> Orders;
        public OrderSearchModel SearchModel;
        private readonly IOrderApplication _orderApplication;
        public IndexModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
        }

        public IActionResult OnGetOrderItems(long id)
        {
            var items = _orderApplication.GetItems(id);
            return Partial("./OrderItems",items);
        }

        public RedirectToPageResult OnGetConfirm(long id)
        {
            _orderApplication. Confirm(id);
            return RedirectToPage();
        }
        public RedirectToPageResult OnGetCancel(long id)
        {
            _orderApplication. Cancel(id);
            return RedirectToPage();
        }

    }
}
