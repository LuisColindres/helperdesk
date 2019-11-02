using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Ticket> Add(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return null;
        }

        public void AddTicketDetail(TicketDetail ticketDetail)
        {
            _context.TicketDetails.Add(ticketDetail);
        }

        public async Task<Ticket> GetTicket(int id)
        {
            // var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            var ticket = await (from e in _context.Tickets
                        join requestinUser in _context.Users on e.RequestingUserId equals requestinUser.Id
                        join ticketType in _context.TicketTypes on e.TicketTypeId equals ticketType.Id
                        join ticketCategory in _context.TicketCategories on e.TicketCategoryId equals ticketCategory.Id
                        join usserAssing in _context.Users on e.UserAssingId equals usserAssing.Id
                        join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id
                        join ticketStatus in _context.TicketStatus on e.TicketStatusId equals ticketStatus.Id
                        select new Ticket{
                            Id = e.Id,
                            Description = e.Description,
                            RequestingUser = requestinUser,
                            RequestingUserId = e.RequestingUserId,
                            TicketType = ticketType,
                            TicketTypeId = e.TicketTypeId,
                            TicketCategory = ticketCategory,
                            TicketCategoryId = e.TicketCategoryId,
                            UserAssing = usserAssing,
                            UserAssingId = e.UserAssingId,
                            AssignedUser = e.AssignedUser,
                            AssignationDate = e.AssignationDate,
                            TicketStatus = ticketStatus,
                            TicketStatusId = e.TicketStatusId,
                            CreatedAt = e.CreatedAt
                        }).FirstOrDefaultAsync(x => x.Id == id);
                        

            return ticket;
        }

        public async Task<List<TicketForListDto>> GetTickets()
        {
            var tickets = await (from e in _context.Tickets
                        join requestinUser in _context.Users on e.RequestingUserId equals requestinUser.Id
                        join ticketType in _context.TicketTypes on e.TicketTypeId equals ticketType.Id
                        join ticketCategory in _context.TicketCategories on e.TicketCategoryId equals ticketCategory.Id
                        join usserAssing in _context.Users on e.UserAssingId equals usserAssing.Id
                        join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id
                        join ticketStatus in _context.TicketStatus on e.TicketStatusId equals ticketStatus.Id
                        select new TicketForListDto{
                            Id = e.Id,
                            Description = e.Description,
                            // RequestingUserName = requestinUser.Names + ' ' + requestinUser.LastName,
                            RequestingUserId = e.RequestingUserId,
                            TicketTypeDescription = ticketType.Description,
                            TicketTypeId = e.TicketTypeId,
                            TicketCategoryDescription = ticketCategory.Description,
                            TicketCategoryId = e.TicketCategoryId,
                            // UserAssingName = usserAssing.Names + ' ' + usserAssing.LastName,
                            UserAssingId = e.UserAssingId,
                            // AssignedUserName = e.AssignedUser.Names + ' ' + usserAssing.LastName,
                            AssignedUserId = e.AssignedUserId,
                            AssignationDate = e.AssignationDate,
                            TicketStatusDescription = ticketStatus.Description,
                            TicketStatusId = e.TicketStatusId,
                            UpdatedAt = e.UpdatedAt
                        }).ToListAsync();
                        

            return tickets;
        }

        public async Task<IEnumerable<TicketForResolveDto>> GetTicketsResolved()
        {
            
            var tickets = await (from e in _context.Tickets
                            join ticketType in _context.TicketTypes on e.TicketTypeId equals ticketType.Id
                            join detailTicket in _context.TicketDetails.Where(dt => dt.TracingStatusId == 1) on e.Id equals detailTicket.TicketId
                            join detailTickets in _context.TicketDetails.Where(dts => dts.TracingStatusId == 4) on e.Id equals detailTickets.TicketId
                            select new TicketForResolveDto
                            {
                                Description = e.Description,
                                TicketType = ticketType,
                                TicketTypeId = e.TicketTypeId,
                                TicketDetail = detailTicket,
                                TicketDetailSolution = detailTickets
                            }
                        ).Where(ticket => ticket.TicketStatusId == 6).ToListAsync();

            return tickets;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}