namespace Dummy.Web.Common.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using Dummy.Web.Common.Enums;

    public class UserUpdateModel : UserCreateModel
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }
    }
}
