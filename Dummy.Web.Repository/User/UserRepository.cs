namespace Dummy.Web.Repository.User
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.DataAccess;

    public class UserRepository : IUserRepository
    {
        private readonly IDapperHelper _dapperHelper;
        private readonly string GetActiveUsersStoredProcedureName = "dbo.DummyPerson_Get_Active_Users";
        private readonly string UpdateUserStoredProcedure = "dbo.DummyPerson_Update_By_PK";
        private readonly string DeleteUserByEmailStoredProcedure = "dbo.DummyPerson_Delete_By_Email";
        private readonly string DeleteUserByPrimaryKeyStoredProcedure = "dbo.DummyPerson_Delete_By_PK";
        private const string UserRegistrationStoredProcedure = "dbo.DummyPerson_Insert";

        public UserRepository(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public int AddUser(UserModel user, bool status = true)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@FirstName", user.GivenName);
            parameters.Add("@DateCreated", user.Created);
            parameters.Add("@LastName", user.FamilyName);
            parameters.Add("@Password", user.Password);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@Status", status);
            parameters.Add("@Email", user.Email);

            return _dapperHelper.ExecuteStoredProcedure<System.Int32>(CommandType.StoredProcedure, UserRegistrationStoredProcedure, parameters);
        }

        public bool DeleteUser(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            return _dapperHelper.ExecuteStoredProcedure<bool>(CommandType.StoredProcedure, DeleteUserByEmailStoredProcedure, parameters);
        }

        public bool DeleteUser(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", id);

            return _dapperHelper.ExecuteStoredProcedure<bool>(CommandType.StoredProcedure, DeleteUserByPrimaryKeyStoredProcedure, parameters);
        }

        public bool UpdateUser(UserUpdateModel userUpdateModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userUpdateModel.UserName);
            parameters.Add("@FirstName", userUpdateModel.GivenName);
            parameters.Add("@LastName", userUpdateModel.FamilyName);
            parameters.Add("@Password", userUpdateModel.Password);
            parameters.Add("@Gender", userUpdateModel.Gender);
            parameters.Add("@Email", userUpdateModel.Email);

            return _dapperHelper.ExecuteStoredProcedure<bool>(CommandType.StoredProcedure, UpdateUserStoredProcedure, parameters);
        }

        public IEnumerable<UserModelSimplified> GetActiveUsers()
        {
            return _dapperHelper.ExecuteDataset<UserModelSimplified>(CommandType.StoredProcedure, GetActiveUsersStoredProcedureName);
        }
    }
}
