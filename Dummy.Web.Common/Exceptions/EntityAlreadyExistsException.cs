namespace Dummy.Web.Common.Exceptions
{
    using System;

    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string entityName, string propertyName, object propertyValue)
            : base($"{entityName} with {propertyName} {propertyValue} already exists") { }
    }

}