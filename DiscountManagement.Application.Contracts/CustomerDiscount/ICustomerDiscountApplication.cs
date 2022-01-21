using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public interface ICustomerDiscountApplication
{
    OperationResult Create(DefineCustomerDiscount command);
    OperationResult Edit(EditCustomerDiscount command);
    List<CustomerDiscountViewModel> Search(CustomerDiscountSearch searchModel);
    EditCustomerDiscount? GetDetails(long id);

}