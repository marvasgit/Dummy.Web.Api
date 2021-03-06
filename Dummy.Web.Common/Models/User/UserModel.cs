﻿namespace Dummy.Web.Common.Models.User
{
    using Dummy.Web.Common.Enums;

    public class UserModel : UserModelSimplified
    {
        public string UserName { get; set; }

        public GenderType Gender { get; set; }

        public string Password { get; set; }
        
        public bool Status { get; set; }
    }
}
