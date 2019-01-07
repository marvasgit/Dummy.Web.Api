namespace Dummy.Web.Logic.Test.User.Tests
{
    using System;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.Common;
    using Dummy.Web.Logic.Test.Extensions;
    using Dummy.Web.Logic.Test.User.Fakes;
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
           _userLogic  = new UserLogic(userRepository.Object);
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

        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_FirstName_isNullorEmpty()
        {
            _userLogic.AddUser(null);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_LastName_isNullorEmpty()
        {
            _userLogic.AddUser(null);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_Email_isNullorEmpty()
        {
            var mockedUser = new Mock<IUserModel>();
            mockedUser.Setup(x => x.Email).Returns(string.Empty);
            _userLogic.AddUser(mockedUser.Object);
        }

        [TestMethod]
        public void Should_Not_Throw_When_CorrectValues_ArePassed()
        {
            var fakeUser = new FakeUserCreateModel();

            Assert.AreEqual(_userLogic.AddUser(fakeUser), true);
        }
    }
}
