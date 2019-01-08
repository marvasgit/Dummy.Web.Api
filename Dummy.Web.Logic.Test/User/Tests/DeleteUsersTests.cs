namespace Dummy.Web.Logic.Test.User.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Dummy.Web.Common.Exceptions;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.Test.Extensions;
    using Dummy.Web.Logic.Test.User.Fakes;
    using Dummy.Web.Logic.User;
    using Dummy.Web.Repository.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class DeleteUsersTests
    {
        private readonly UserLogic _userLogic;
        private const int excpectedResult = 5;
        private readonly IList<UserModel> _userList;
        public DeleteUsersTests()
        {
            var userRepository = new Mock<IUserRepository>();
            _userList = new List<UserModel>();

            userRepository.Setup(x => x.DeleteUser(It.IsAny<string>()))
                                        .Returns(true)
                                        .Callback((string email) => _userList
                                                                    .Where(u => u.Email == email)
                                                                    .Select(y => y.Status = false));

            userRepository.Setup(x => x.AddUser(It.IsAny<UserModel>(), true))
                                       .Callback((UserModel x, bool z) => _userList.Add(x));

            userRepository.Setup(x => x.GetActiveUsers())
                                       .Returns(_userList.Where(u => u.Status != false)
                                       .ToList());

            _userLogic = new UserLogic(userRepository.Object);
        }

        [TestMethod]
        public void Should_NotBeNull_WhenInstantiated()
        {
            Assert.IsNotNull(_userLogic);
        }

        [DataTestMethod]
        [DataRow("jsdasfdscadcdsdsadsa@")]
        [DataRow("@udsdsdsdasds")]
        [DataRow("dsa%&^*&udsdsd@.doc")]
        [ExpectedCustomException(typeof(WrongEmailException))]
        public void Should_Thrown_When_Email_Is_Invalid(string email)
        {
            _userLogic.DeleteUser(email);
        }

        [DataTestMethod]
        [DataRow("stsdsads@dsadsa.com")]
        [DataRow("sdsadsads@dsadasdsadads.com")]
        public void Should_Not_Thrown_When_A_Valid_String_Is_Passed(string email)
        {
            _userLogic.DeleteUser(email);
        }

        [TestMethod]
        public void Should_Change_Status_AfterDelete()
        {
            var user = new FakeUserCreateModel();
            _userLogic.AddUser(user);

            var numberOfAddedUsers = _userLogic.GetAvailableUsers().Count();
            _userLogic.DeleteUser(user.Email);
            var numberAfterDelete = _userLogic.GetAvailableUsers().Count();

            Assert.AreEqual(numberOfAddedUsers - 1, numberAfterDelete);
        }
    }
}
