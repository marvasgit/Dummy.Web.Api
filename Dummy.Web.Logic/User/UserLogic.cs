namespace Dummy.Web.Logic.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Dummy.Web.Common.Enums;
    using Dummy.Web.Common.Exceptions;
    using Dummy.Web.Common.Models.User;
    using Dummy.Web.Logic.Common;
    using Dummy.Web.Repository.User;

    public class UserLogic : IUserLogic
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

        public bool UpdateUser(UserUpdateModel userUpdateModel)
        {
            ValidateModel(userUpdateModel);

            IsValidEmail(userUpdateModel.Email);

            var test = _userRepository.UpdateUser(userUpdateModel);

            return test;
        }

        public bool DeleteUser(string email)
        {
            ValidateModel(email);
            IsValidEmail(email);

            return _userRepository.DeleteUser(email);
        }

        public IEnumerable<UserModelSimplified> GetAvailableUsers()
        {
            var users = _userRepository.GetActiveUsers();

            return users.Select(x => new UserModelSimplified
            {
                Created = x.Created,
                Email = x.Email,
                FamilyName = x.FamilyName,
                GivenName = x.GivenName,
                Id = x.Id
            });
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
