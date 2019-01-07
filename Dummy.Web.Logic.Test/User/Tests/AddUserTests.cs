namespace Dummy.Web.Logic.Test.User.Tests
{
    using System;
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
            _userLogic = new UserLogic(userRepository.Object);
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
            var user = new FakeUserCreateModel(null, RandomGenerator.RandomString(3), RandomGenerator.RandomEmailAddress(12));

            _userLogic.AddUser(null);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_LastName_isNullorEmpty()
        {
            var user = new FakeUserCreateModel(RandomGenerator.RandomString(3), null, RandomGenerator.RandomEmailAddress(12));

            _userLogic.AddUser(null);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(NullReferenceException), UserErrorMessagesConstants.Null)]
        public void Should_Thrown_When_Email_isNullorEmpty()
        {
            var user = new FakeUserCreateModel(RandomGenerator.RandomString(3), RandomGenerator.RandomString(3), null);

            _userLogic.AddUser(user);
        }

        [TestMethod]
        public void Should_Not_Throw_When_CorrectValues_ArePassed()
        {
            var fakeUser = new FakeUserCreateModel();

            Assert.AreEqual(_userLogic.AddUser(fakeUser), true);
        }
    }
}
