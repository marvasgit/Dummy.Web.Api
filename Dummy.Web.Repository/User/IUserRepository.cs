namespace Dummy.Web.Repository.User
{
    using System;
    using System.Collections.Generic;
    using Dummy.Web.Common.Enums;
    using Dummy.Web.Common.Models;

    public interface IUserRepository
    {
        int AddUser(string userName, string firstName, string lastName, DateTime created, string password, string email, GenderType gender, bool status = true);
        bool DeleteUser(string email);
        IEnumerable<UserModel> GetActiveUsers();
        bool UpdateUser(UserModel user, string password, bool status = true);
    }
}