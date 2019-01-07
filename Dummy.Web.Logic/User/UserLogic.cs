namespace Dummy.Web.Logic.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Dummy.Web.Common.Enums;
    using Dummy.Web.Common.Exceptions;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.Common;
    using Dummy.Web.Repository.User;
    using PasswordUtility.PasswordGenerator;

    public class UserLogic
    {
        private readonly IUserRepository _userRepository;
        public const int passwordLenght = 43;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(UserCreateModel userCreateModel)
        {
            ValidateModel(userCreateModel);

            var complexUser = new UserModel
            {
                FamilyName = userCreateModel.FamilyName,
                GivenName = userCreateModel.GivenName,
                Email = userCreateModel.Email,
                UserName = UserNameGenerator(userCreateModel),
                Password = PwGenerator.Generate(passwordLenght).ReadString(),
                Created = DateTime.Now,
                Gender = GenderType.Unknown,
            };

            return _userRepository.AddUser(complexUser);

        }

        private void ValidateModel(object model)
        {
            NotNull(model);
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true))
            {
                throw new InvalidModelException(validationResults);
            }
        }

        private static void NotNull(object model)
        {
            if (model == null)
            {
                var validationResults = new List<ValidationResult>() {
                    new ValidationResult(UserErrorMessagesConstants.ModelCantBeNull, new List<string>() {
                    })
                };
                throw new InvalidModelException(validationResults);
            }
        }

        public string UserNameGenerator(UserCreateModel user)
        {
            return $"{user.GivenName.Substring(0, 3)}{user.FamilyName.Substring(0, 3)}";
        }
    }
}
