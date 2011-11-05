using System.Collections.Generic;
using System.Linq;

namespace DrinkMyWine
{
    public interface IUserRepository
    {
        bool AddUser(User newUser);
        User GetUserByEmail(string email);
    }
}