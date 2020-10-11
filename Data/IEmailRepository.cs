using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IEmailRepository
    {
         Task<int> Add(Email email);

         Task<bool> Edit(EmailForUpdateDto email, int id);

         Task<List<EmailForListDto>> List();

         Task<EmailForListDto> Get(int id);
    }
}