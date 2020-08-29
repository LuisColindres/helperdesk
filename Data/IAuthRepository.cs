using System.Threading.Tasks;
using HelperDesk.API.Models;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);

         Task<User> UserActive(string username);

         Task<User> ChangeActive(int userId);

         Task<Sessions> Add(Sessions session);

         Task<UserForListDto> GetUser(int id);
    }
}