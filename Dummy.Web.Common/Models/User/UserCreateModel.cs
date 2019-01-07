namespace Dummy.Web.Common.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserCreateModel
    {
        [Required]
        public string GivenName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}