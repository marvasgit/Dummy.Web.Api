namespace Dummy.Web.Logic.Test.User.Tests
{
    using System;
    using Dummy.Web.Logic.Common;
    using Dummy.Web.Logic.Test.Extensions;
    using Dummy.Web.Logic.User;
    using Dummy.Web.Repository.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AddUserTests
    {
        private readonly UserLogic _userLogic;
        public AddUserTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var _userLogic = new UserLogic(userRepository);
        }
        [TestMethod]
        public void Should_NotBeNull_WhenInstantiated()
        {
            Assert.IsNotNull(_userLogic);
        }
        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_UserData_IsNull()
        {
            _userLogic.AddUser(null);
        }
    }
}
