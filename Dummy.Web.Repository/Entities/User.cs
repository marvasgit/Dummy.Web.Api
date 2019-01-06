namespace Dummy.Web.Repository.Entities
{
    using System;

    class User
    {
        int Id { get; set; }
        string Email { get; set; }
        string GivenName { get; set; }
        string FamilyName { get; set; }
        DateTime Created { get; set; }
    }
}
