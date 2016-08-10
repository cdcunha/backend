using SpaUserControl.Domain.Contracts.Services;
using System;
using SpaUserControl.Domain.Models;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Common.Validation;
using SpaUserControl.Resource.Resources;

namespace SpaUserControl.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }

        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);

            user.ChangeName(name);

            _repository.Update(user);
        }

        public void ChangePassword(string email, string password, string newPassword, string confirmNewPassword)
        {
            var user = Authenticate(email, password);

            user.SetPassword(newPassword, confirmNewPassword);

            _repository.Update(user);
        }
        
        public User GetByEmail(string email)
        {
            var user = _repository.Get(email);
            if (user == null)
                throw new Exception(Errors.UserNotFound);

            return user;
        }

        public void Register(string name, string email, string password, string confirmPassword)
        {
            //O trecho abaixo foi comentado para não fazer chamada extra no banco, porque já existe chave no banco para não permitir email duplicado
            /*
            var hasUser = GetByEmail(email);
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);
            */

            var user = new User(name, email);
            user.SetPassword(password, confirmPassword);

            _repository.Create(user);
        }

        public string resetPassword(string email)
        {
            var user = GetByEmail(email);
            var password = user.ResetPassword();

            _repository.Update(user);

            return password;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
