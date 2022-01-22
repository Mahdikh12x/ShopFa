using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscount;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscount)
        {
            _customerDiscount = customerDiscount;
        }

        public OperationResult Create(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscount.Exists(x => x.DiscountRate == command.DiscountRate
                && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(command.ProductId, startDate, endDate, command.Reason,
                command.DiscountRate);
            _customerDiscount.Create(customerDiscount);
            _customerDiscount.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscount.Get(command.Id);
            if (customerDiscount == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);

            if (_customerDiscount.Exists(x => 
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId,startDate,endDate,command.Reason,command.DiscountRate);
            _customerDiscount.SaveChanges();
            return operation.Succedded();

        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearch searchModel)
        {
            return _customerDiscount.Search(searchModel);
        }

        public EditCustomerDiscount? GetDetails(long id)
        {
          return _customerDiscount.GetDetails(id);
        }
    }
}
