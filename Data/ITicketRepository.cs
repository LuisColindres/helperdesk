using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface ITicketRepository
    {
         Task<Ticket> Add(Ticket ticket);
         void AddTicketDetail(TicketDetail ticketDetail);
         Task<List<TicketForListDto>> GetTickets();
         Task<Ticket> GetTicket(int id);
         Task<IEnumerable<TicketForResolveDto>> GetTicketsResolved();
         Task<bool> SaveAll();
         
    }
}