using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            if (_slideRepository.Exists(x => x.Heading == command.Heading))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            slide.Edit(command.Picture,command.PictureAlt,command.PictureTitle,command.Heading,command.Title,command.Text,command.BtnText);
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            slide.Remove();
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            slide.Restore();
            _slideRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }
    }
}
