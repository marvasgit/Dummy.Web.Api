namespace Dummy.Web.Logic.Test.User.Tests
{
    using Dummy.Web.Common.Exceptions;
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
        private const int excpectedResult = 5;
        public AddUserTests()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.AddUser(It.IsAny<UserModel>(), true)).Returns(excpectedResult);
            _userLogic = new UserLogic(userRepository.Object);
        }

        [TestMethod]
        public void Should_NotBeNull_WhenInstantiated()
        {
            Assert.IsNotNull(_userLogic);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(InvalidModelException), UserErrorMessagesConstants.ModelCantBeNull)]
        public void Should_Thrown_When_UserData_IsNull()
        {
            _userLogic.AddUser(null);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(InvalidModelException), UserErrorMessagesConstants.FirstNameIsNull)]
        public void Should_Thrown_When_FirstName_isNullorEmpty()
        {
            var user = new FakeUserCreateModel(null, RandomGenerator.RandomString(3), RandomGenerator.RandomEmailAddress(12));

            _userLogic.AddUser(user);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(InvalidModelException), UserErrorMessagesConstants.LastNameIsNull)]
        public void Should_Thrown_When_LastName_isNullorEmpty()
        {
            var user = new FakeUserCreateModel(RandomGenerator.RandomString(3), null, RandomGenerator.RandomEmailAddress(12));

            _userLogic.AddUser(user);
        }

        [TestMethod]
        [ExpectedCustomException(typeof(InvalidModelException), UserErrorMessagesConstants.EmailisNull)]
        public void Should_Thrown_When_Email_isNullorEmpty()
        {
            var user = new FakeUserCreateModel(RandomGenerator.RandomString(3), RandomGenerator.RandomString(3), null);

            _userLogic.AddUser(user);
        }

        [TestMethod]
        public void Should_Not_Throw_When_CorrectValues_ArePassed()
        {
            var fakeUser = new FakeUserCreateModel();

            Assert.AreEqual(excpectedResult, _userLogic.AddUser(fakeUser));
        }
    }
}
