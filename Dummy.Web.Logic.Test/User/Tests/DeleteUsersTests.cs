namespace Dummy.Web.Logic.Test.User.Tests
{
    using Dummy.Web.Common.Exceptions;
    using Dummy.Web.Logic.Common;
    using Dummy.Web.Logic.Test.Extensions;
    using Dummy.Web.Logic.User;
    using Dummy.Web.Repository.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class DeleteUsersTests
    {
        private readonly UserLogic _userLogic;
        private const int excpectedResult = 5;
        public DeleteUsersTests()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.DeleteUser(It.IsAny<string>())).Returns(true);
            _userLogic = new UserLogic(userRepository.Object);

        }

        [TestMethod]
        public void Should_NotBeNull_WhenInstantiated()
        {
            Assert.IsNotNull(_userLogic);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedCustomException(typeof(InvalidModelException), UserErrorMessagesConstants.EmailisNull)]
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
    }
}
