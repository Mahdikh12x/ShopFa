using _0_Framework.Application;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        public UserApplication(IUserRepository userRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper)
        {
            _userRepository = userRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
        }

        public bool Active(long id)
        {
            var user = _userRepository.Get(id);
            if(user == null)
                return false;

            user.Active();
            _userRepository.SaveChanges();
            return true;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var result = new OperationResult();
            try
            {

                var user = _userRepository.Get(command.Id);
                if (user == null)
                    result.Failed(ApplicationValidationMessages.NotExisted);

                if(command.Password!=command.RePassword)
                    return result.Failed(ApplicationValidationMessages.PasswordNotMatch);

                var password = _passwordHasher.Hash(command.Password);
                user.ChangePassword(password);
                _userRepository.SaveChanges();
                return result.Succedded();
            }
            catch (Exception excption)
            {
                Console.WriteLine(excption);
                return result.Failed(ApplicationValidationMessages.SystemFailed);
            }
        }

        public OperationResult Register(RegisterUser command)
        {
           var result=new OperationResult();
            try
            {
                if (_userRepository.Exists(x => x.Username == command.Username && x.Mobile == command.Mobile))
                    return result.Failed(ApplicationValidationMessages.Duplicated);

                var password = _passwordHasher.Hash(command.Password);
                var path = "profilePicture";
                var picurteName = _fileUploader.Upload(command.ProfilePicture,path);
                var user = new User(command.Fullname, command.Username, password, command.Mobile, command.RoleId, picurteName);

                _userRepository.Create(user);
                _userRepository.SaveChanges();

                return result.Succedded();


            }
            catch (Exception excption)
            {
                Console.WriteLine(excption);
                return result.Failed(ApplicationValidationMessages.SystemFailed);
            }
        }

        public bool DeActive(long id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
                return false;
            
            user.DeActive();
            _userRepository.SaveChanges();
            
            return true;
        }

        public OperationResult Edit(EditUser command)
        {
            var result = new OperationResult();
            try
            {

                var user=_userRepository.Get(command.Id);
                if(user == null)
                    result.Failed(ApplicationValidationMessages.NotExisted);

                if (_userRepository.Exists(x => (x.Username==command.Username||x.Fullname==command.Fullname)&&x.Id!=command.Id))
                    return result.Failed(ApplicationValidationMessages.Duplicated);

                var path = "profilePicture";
                var picurteName = _fileUploader.Upload(command.ProfilePicture, path);

                user.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picurteName);
                _userRepository.SaveChanges();

                return result.Succedded();


            }
            catch (Exception excption)
            {
                Console.WriteLine(excption);
                return result.Failed(ApplicationValidationMessages.SystemFailed);
            }
        }

        public EditUser? GetDetails(long id)
        {
            return _userRepository.GetDetails(id);
        }

        public List<UserViewModel> Search(UserSearchModel searchModel)
        {
            return _userRepository.Search(searchModel);
        }

        public OperationResult Login(LoginUser command)
        {
            var result = new OperationResult();
            try
            {
                var user = _userRepository.GetBy(command.Username);

                if(user == null)
                    return result.Failed(ApplicationValidationMessages.WrongUserPass);

                (bool Verified, bool NeedsUpgrade) = _passwordHasher.Check(user.Password, command.Password);
                if (!Verified)
                    return result.Failed(ApplicationValidationMessages.PasswordNotMatch);

                var account = new AccountViewModel(user.Id, user.RoleId, user.Role.Name,user.Username,user.Fullname,user.Password,user.Mobile);
                _authHelper.Signin(account);
                return result.Succedded();

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                throw;
            }
        }

        public void SignOut()
        {
            _authHelper.SignOut();
        }
    }
}
