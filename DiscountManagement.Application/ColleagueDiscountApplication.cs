using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public  class ColleagueDiscountApplication:IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscount;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscount)
        {
            _colleagueDiscount = colleagueDiscount;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscount.Exists(x =>
                    x.DiscountRate == command.DiscountRate && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscount.Create(colleagueDiscount);
            _colleagueDiscount.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscount.Get(command.Id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            if (_colleagueDiscount.Exists(x =>
                    x.DiscountRate == command.DiscountRate && x.ProductId == command.ProductId&&x.Id!=command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            
            colleagueDiscount.Edit(colleagueDiscount.ProductId,colleagueDiscount.DiscountRate);
            _colleagueDiscount.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscount.Get(id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            colleagueDiscount.Active();
            _colleagueDiscount.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult DeActive(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscount.Get(id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            colleagueDiscount.DeActive();
            _colleagueDiscount.SaveChanges();
            return operation.Succedded();

        }

        public EditColleagueDiscount? GetDetails(long id)
        {
            return _colleagueDiscount.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscount.Search(searchModel);
        }
    }
}
