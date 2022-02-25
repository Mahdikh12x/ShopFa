using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;



namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public  class CustomerDiscountRepository:BaseRepository<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository( DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount? GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(CultureInfo.InvariantCulture),
                ProductId = x.ProductId,
                Reason = x.Reason,
                StartDate = x.StartDate.ToString(CultureInfo.InvariantCulture)
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearch searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                Reason = x.Reason.Substring(0, Math.Min(x.Reason.Length, 75)) + "...",
                //Product = products.FirstOrDefault(zx=>x.Id==x.ProductId).Name,
                ProductId = x.ProductId
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(x => x.StartDateEnglish > searchModel.StartDate.ToGeorgianDateTime());
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
                query = query.Where(x => x.EndDateEnglish<searchModel.EndDate.ToGeorgianDateTime());

            var discounts = query.OrderByDescending(x => x.Id).ToList();

                discounts.ForEach(x=>x.Product=products.FirstOrDefault(z=>z.Id==x.ProductId)?.Name);
                return discounts;
        }
    }
}
