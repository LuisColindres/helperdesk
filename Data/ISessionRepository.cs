using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface ISessionRepository
    {
         Task<Sessions> Add(Sessions sessions);

         Task<Sessions> Edit(SessionForUpdateDto session, int id);

        Task<List<SessionForListDto>> List();

        Task<Sessions> Get(int id);

        Task<List<SessionForListDto>> GetSessionByFilter(int departmentId, DateTime startDate, DateTime endDate);

    }
}