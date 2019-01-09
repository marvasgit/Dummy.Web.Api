namespace Dummy.Web.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("FirstName")]
        public string GivenName { get; set; }

        [Column("LastName")]
        public string FamilyName { get; set; }

        [Column("Gender")]
        public string Gender { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("DateCreated")]
        public DateTime Created { get; set; }
    }
}
