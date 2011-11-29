using System;

namespace DrinkMyWine
{
    public class User
    {
        public static User Create(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            IValidator validator = new EmailValidator(email);
            var result = validator.ValidateWithResult();
            if (!result)
            {
                return new InvalidUser();   
            }

            User user = new User { Email = email, Password = password };

            return user;
        }

        public string Password { get; private set; }
        public string Email { get; private set; }
    }

    public class InvalidUser : User
    {
    }
}