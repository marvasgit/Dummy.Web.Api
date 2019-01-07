﻿using System;
using Dummy.Web.Common.Models.User;
using Dummy.Web.Logic.Test.Extensions;

namespace Dummy.Web.Logic.Test.User.Fakes
{
    public class FakeUserCreateModel : UserModel
    {
        public FakeUserCreateModel() : this(RandomGenerator.RandomString(5), RandomGenerator.RandomString(7), RandomGenerator.RandomEmailAddress(4))
        {

        }

        public FakeUserCreateModel(string firstName, string lastName, string email)
        {
            GivenName = firstName;
            FamilyName = lastName;
            Email = email;
            Gender = 0;
            UserName = $"{GivenName.Substring(0, 3)}{FamilyName.Substring(0, 3)}";
            Password = RandomGenerator.RandomString(65);
            Created = DateTime.Now;
        }

        public FakeUserCreateModel WithEmptyFirstName()
        {
            return new FakeUserCreateModel(string.Empty, RandomGenerator.RandomString(5), RandomGenerator.RandomEmailAddress(16));
        }
        //......
    }
}
