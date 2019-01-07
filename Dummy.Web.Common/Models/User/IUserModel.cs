namespace Dummy.Web.Common.Models.User
{
    using System;
    using Dummy.Web.Common.Enums;

    public interface IUserModel
    {
        GenderType Gender { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
        string GivenName { get; set; }
        string FamilyName { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        DateTime Created { get; set; }
    }
}