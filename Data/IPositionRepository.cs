using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IPositionRepository
    {
         Task<int> Add(Position position);

         Task<bool> Edit(PositionForUpdateDto position, int id);

         Task<List<PositionForListDto>> List();

         Task<PositionForListDto> GetPosition(int id);
    }
}