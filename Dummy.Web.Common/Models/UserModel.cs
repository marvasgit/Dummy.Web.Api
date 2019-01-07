namespace Dummy.Web.Common.Models
{
    using System;
    using Dummy.Web.Common.Enums;

    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public GenderType Gender { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }
    }
}
