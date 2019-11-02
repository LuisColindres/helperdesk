using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface ITicketRepository
    {
         Task<int> Add(TicketForRegisterDto ticket);
         void AddTicketDetail(TicketDetail ticketDetail);
         Task<int> UpdateTicket(TicketForAssingDto ticket, int id);
         Task<List<TicketForListDto>> GetTickets();
         Task<TicketForListDto> GetTicket(int id);
         Task<IEnumerable<TicketForResolveDto>> GetTicketsResolved();
         Task<bool> SaveAll();
         Task<bool> ServeTicket(TicketForServeDto ticket, int id);
         Task<bool> Edit(TicketForUpdateDto ticket, int id);
         
    }
}