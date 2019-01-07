namespace Dummy.Web.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Column("ID")]
        int Id { get; set; }

        [Column("UserName")]
        string UserName { get; set; }

        [Column("FirstName")]
        string GivenName { get; set; }

        [Column("LastName")]
        string FamilyName { get; set; }

        [Column("Gender")]
        string Gender { get; set; }

        [Column("Password")]
        string Password { get; set; }

        [Column("Email")]
        string Email { get; set; }

        [Column("DateCreated")]
        DateTime Created { get; set; }
    }
}
