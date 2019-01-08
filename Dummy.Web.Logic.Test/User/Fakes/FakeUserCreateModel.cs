namespace Dummy.Web.Logic.Test.User.Fakes
{
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.Test.Extensions;

    public class FakeUserCreateModel : UserCreateModel
    {
        public FakeUserCreateModel() : this(RandomGenerator.RandomString(5), RandomGenerator.RandomString(7), RandomGenerator.RandomEmailAddress(4))
        {

        }

        public FakeUserCreateModel(string firstName, string lastName, string email)
        {
            GivenName = firstName;
            FamilyName = lastName;
            Email = email;
        }

        public FakeUserCreateModel WithEmptyFirstName()
        {
            return new FakeUserCreateModel(string.Empty, RandomGenerator.RandomString(5), RandomGenerator.RandomEmailAddress(16));
        }

        public FakeUserCreateModel WithEmptyLastName()
        {
            return new FakeUserCreateModel(RandomGenerator.RandomString(5), string.Empty, RandomGenerator.RandomEmailAddress(16));
        }

        public FakeUserCreateModel WithEmptyEmail()
        {
            return new FakeUserCreateModel(RandomGenerator.RandomString(5), RandomGenerator.RandomString(16), string.Empty);
        }

        public FakeUserCreateModel WithEmptyInvalidEmail()
        {
            return new FakeUserCreateModel(RandomGenerator.RandomString(5), RandomGenerator.RandomString(16), RandomGenerator.RandomString(16));
        }
    }
}
