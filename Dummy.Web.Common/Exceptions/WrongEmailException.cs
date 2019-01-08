namespace Dummy.Web.Common.Exceptions
{
    using System;

    public class WrongEmailException : Exception
    {
        public WrongEmailException()
        {
        }

        public WrongEmailException(string wrongEmail)
            : base($"{wrongEmail} is invalid Email") { }
    }
}
