using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IManagamentRepository
    {
         Task<int> Add(Management management);

         Task<bool> Edit(ManagamentForUpdateDto managament, int id);

         Task<List<ManagamentForListDto>> List();

         Task<ManagamentForListDto> Get(int id);
    }
}