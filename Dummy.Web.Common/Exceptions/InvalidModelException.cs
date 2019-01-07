namespace Dummy.Web.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class InvalidModelException : Exception
    {
        public IEnumerable<ValidationResult> Results { get; private set; }

        public InvalidModelException(IEnumerable<ValidationResult> results)
            : base(string.Join("/", results.Select(x => x.ErrorMessage)))
        {
            Results = results;
        }

        public InvalidModelException(ValidationResult validationResult)
            : base(validationResult.ErrorMessage)
        {
            var results = new List<ValidationResult>();
            results.Add(validationResult);
            Results = results;
        }
    }
}
