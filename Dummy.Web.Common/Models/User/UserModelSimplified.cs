namespace Dummy.Web.Common.Models.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserModelSimplified : UserCreateModel
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public DateTime Created { get; set; }
    }
}
