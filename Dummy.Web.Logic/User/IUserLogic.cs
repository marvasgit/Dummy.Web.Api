namespace Dummy.Web.Logic.User
{
    using System.Collections.Generic;
    using Dummy.Web.Common.Models.User;

    public interface IUserLogic
    {
        int AddUser(UserCreateModel userCreateModel);
        bool DeleteUser(string email);
        IEnumerable<UserModelSimplified> GetAvailableUsers();
        bool UpdateUser(UserUpdateModel userUpdateModel);
    }
}