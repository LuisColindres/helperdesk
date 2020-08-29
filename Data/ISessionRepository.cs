using System.Threading.Tasks;
using HelperDesk.API.Models;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public interface ISessionRepository
    {
         Task<Sessions> Add(Sessions session);

         Task<Sessions> Edit(int id, SessionForUpdateDto session);
    }
}