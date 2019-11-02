using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IUserRepository
    {
         Task<User> Add(User user, string password);
         Task<User> Edit(int id, UserForUpdateDto user);
         Task<List<UserForListDto>> GetUsers();
         Task<UserForListDto> GetUser(int id);
         Task<bool> SaveAll();

    }
}