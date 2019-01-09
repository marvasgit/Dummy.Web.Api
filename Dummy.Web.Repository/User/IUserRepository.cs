namespace Dummy.Web.Repository.User
{
    using System.Collections.Generic;
    using Dummy.Web.Common.Models.User;

    public interface IUserRepository
    {
        int AddUser(UserModel user, bool status = true);
        bool DeleteUser(string email);
        IEnumerable<UserModelSimplified> GetActiveUsers();
        bool UpdateUser(UserUpdateModel user);
    }
}