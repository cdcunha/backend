using SpaUserControl.Common.Validation;
using SpaUserControl.Resource.Resources;
using System;

namespace SpaUserControl.Domain.Models
{
    public class User
    {
        #region Ctor
        protected User() { } //Scopo protected para que somente o EntityFramework enxergue o construtor

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            
            //O ideal é fazer a validação do novo Password, pois num teste unitário será lançada uma exceção, caso haja alguma inconsistência
            EmailAssertionConcern.AssertIsValid(this.Email);
        }
        #endregion

        #region Properties
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        #endregion

        #region Methods
        public void SetPassword(string password, string confirmPassword)
        {
            AssertionConcern.AssertArgumentNotNull(password, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentNotNull(confirmPassword, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentLength(password, 6, 20, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentEquals(password, confirmPassword, Errors.PasswordDoesNotMatch);

            this.Password = PasswordAssertionConcern.Encrypt(password);

            //O ideal é fazer a validação do novo Password, pois num teste unitário será lançada uma exceção, caso haja alguma inconsistência
            PasswordAssertionConcern.AssertIsValid(this.Password);
        }

        public string ResetPassword()
        {
            string password = Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = PasswordAssertionConcern.Encrypt(password);
            
            //O ideal é fazer a validação do novo Password, pois num teste unitário será lançada uma exceção, caso haja alguma inconsistência
            PasswordAssertionConcern.AssertIsValid(this.Password);

            return password;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
            //O ideal é fazer a validação do novo nome, pois num teste unitário será lançada uma exceção, caso haja alguma inconsistência
            AssertionConcern.AssertArgumentLength(this.Name, 3, 250, Errors.InvalidUserName);
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(this.Name, 3, 250, Errors.InvalidUserName);
            EmailAssertionConcern.AssertIsValid(this.Email);
            PasswordAssertionConcern.AssertIsValid(this.Password);
        }
        #endregion
    }
}
