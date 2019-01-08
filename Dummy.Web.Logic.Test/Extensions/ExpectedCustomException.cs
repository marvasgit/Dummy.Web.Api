namespace Dummy.Web.Logic.Test.Extensions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public sealed class ExpectedCustomExceptionAttribute : ExpectedExceptionBaseAttribute
    {
        private readonly Type _expectedExceptionType;
        private readonly IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> _expectedValidationResult;

        private string _expectedExceptionMessage;

        public ExpectedCustomExceptionAttribute(Type expectedExceptionType)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = string.Empty;
        }

        public ExpectedCustomExceptionAttribute(Type expectedExceptionType, string expectedExceptionMessage)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = expectedExceptionMessage.TrimEnd();
        }

        public ExpectedCustomExceptionAttribute(Type expectedExceptionType, IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> validationResult)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedValidationResult = validationResult;
        }

        protected override void Verify(Exception exception)
        {
            Assert.IsNotNull(exception);

            Assert.IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");

            if (!_expectedExceptionMessage.Length.Equals(0))
            {
                Assert.AreEqual(_expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
            }
        }
    }
}
