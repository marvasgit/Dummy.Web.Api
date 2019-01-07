using Dummy.Web.Common.Models.User;
using Dummy.Web.Repository.User;

namespace Dummy.Web.Logic.User
{
    public class UserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUser(IUserModel userModel)
        {
            return true;
        }
    }
}
