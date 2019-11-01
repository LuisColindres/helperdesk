using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface ICatalogsRepository
    {
         void Add<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<TicketCategory>> GetTicketCategories();
         Task<TicketCategory> GetTicketCategory(int id);
         Task<IEnumerable<TicketStatus>> GetTicketStatuses();
         Task<TicketStatus> GetTicketStatus(int id);
         Task<IEnumerable<TicketType>> GetTicketTypes();
         Task<TicketType> GetTicketType(int id);
         Task<IEnumerable<TracingStatus>> GetTracingStatuses();
         Task<TracingStatus> GetTracingStatus(int id);
    }
}