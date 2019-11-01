using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IGenderRepository
    {
         Task<Gender> Add(Gender gender);

         Task<Gender> Edit(GenderForUpdateDto gender, int id);

         Task<bool> GenderExists(string genderDescription);

         Task<List<Gender>> List();

         Task<Gender> GetGender(int id);
    }
}