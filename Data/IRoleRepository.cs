using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IRoleRepository
    {
         Task<Role> Add(Role role);

         Task<Role> Edit(RoleForAddDto role, int id);

         Task<bool> RoleExists(string roleDescription);

         Task<List<Role>> List();

         Task<Role> GetRole(int id);
    }
}