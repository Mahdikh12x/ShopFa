using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var result=new OperationResult();
            try
            {
                if (_roleRepository.Exists(x => x.Name == command.Name))
                    return result.Failed(ApplicationValidationMessages.Duplicated);

                var role = new Role(command.Name, command.Description);
                _roleRepository.Create(role);
                _roleRepository.SaveChanges();
                return result.Succedded();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return result.Failed(ApplicationValidationMessages.SystemFailed);
            }
        }

        public OperationResult Edit(EditRole command)
        {
            var result = new OperationResult();
            try
            {
                var role=_roleRepository.Get(command.Id);
                if(role == null)
                    return result.Failed(ApplicationValidationMessages.NotExisted);

                    if (_roleRepository.Exists(x => x.Name == command.Name&&x.Id!=command.Id))
                    return result.Failed(ApplicationValidationMessages.Duplicated);

                role.Edit(command.Name, command.Description);
                _roleRepository.SaveChanges();
                return result.Succedded();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return result.Failed(ApplicationValidationMessages.SystemFailed);
            }
        }

        public EditRole? GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _roleRepository.GetRoles();
        }
    }
}