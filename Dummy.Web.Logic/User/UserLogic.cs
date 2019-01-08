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

            IsValidEmail(userCreateModel.Email);

            var password = PasswordGenerator.GenerateRandomPassword(passwordLenght);

            var complexUser = new UserModel
            {
                FamilyName = userCreateModel.FamilyName,
                GivenName = userCreateModel.GivenName,
                Email = userCreateModel.Email,
                UserName = UserNameGenerator(userCreateModel),
                Password = password,
                Created = DateTime.Now,
                Gender = GenderType.Unknown,
            };

            return _userRepository.AddUser(complexUser);
        }

        public bool DeleteUser(string email)
        {
            ValidateModel(email);
            IsValidEmail(email);

            return _userRepository.DeleteUser(email);
        }

        public IEnumerable<UserModelSimplified> GetAvailableUsers()
        {
            return _userRepository.GetActiveUsers();
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

        private string UserNameGenerator(UserCreateModel user)
        {
            return $"{user.GivenName.Substring(0, 3)}{user.FamilyName.Substring(0, 3)}";
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                throw new WrongEmailException(email);
            }
        }
    }
}
