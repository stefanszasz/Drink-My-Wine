using System.Text.RegularExpressions;

namespace DrinkMyWine
{
    public class EmailValidator : IValidator
    {
        private const string Pattern = @"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$";
        private readonly string _email;

        public EmailValidator(string email)
        {
            _email = email;
        }

        public bool ValidateWithResult()
        {
            return Regex.IsMatch(_email, Pattern, RegexOptions.CultureInvariant);
        }
    }
}