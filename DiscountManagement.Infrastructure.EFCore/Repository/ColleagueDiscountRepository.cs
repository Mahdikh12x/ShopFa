using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository:BaseRepository<long,ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext):base(discountContext)
        {
            _discountContext=discountContext;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount? GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductId = x.ProductId,
                IsActive = x.IsActivated
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(dis=>dis.Product=products.FirstOrDefault(x=>x.Id==dis.ProductId)?.Name);
            return discounts;
        }
    }
}
