namespace Dummy.Web.Common.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class UserCreateModel
    {
        [Required]
        [MinLength(3)]
        public string GivenName { get; set; }

        [Required]
        [MinLength(3)]
        public string FamilyName { get; set; }

        [Required]
        [MinLength(8)]
        public string Email { get; set; }
    }
}