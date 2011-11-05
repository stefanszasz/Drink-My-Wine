namespace DrinkMyWine
{
    public interface IUserRegistrationService
    {
        bool RegisterUser(User user);
        bool UnregisterUser(User user);
    }
}