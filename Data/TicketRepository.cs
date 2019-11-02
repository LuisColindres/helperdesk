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

        public async Task<int> Add(TicketForRegisterDto ticketForRegisterDto)
        {
            var ticketToCreate = new Ticket {
                Description = ticketForRegisterDto.Description,
                RequestingUserId = ticketForRegisterDto.RequestingUserId,
                TicketTypeId = ticketForRegisterDto.TicketTypeId,
                TicketCategoryId = ticketForRegisterDto.TicketCategoryId,
                TicketStatusId = ticketForRegisterDto.TicketStatusId
            };
            await _context.Tickets.AddAsync(ticketToCreate);
            await _context.SaveChangesAsync();

            return ticketToCreate.Id;
        }

        public void AddTicketDetail(TicketDetail ticketDetail)
        {
            _context.TicketDetails.Add(ticketDetail);
        }

        public async Task<bool> Edit(TicketForUpdateDto ticket, int id)
        {
            _context.Entry(await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(ticket);
            await _context.SaveChangesAsync();

            return true;   
        }

        public async Task<TicketForListDto> GetTicket(int id)
        {
            // var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            var ticket = await (from e in _context.Tickets
                        join requestinUser in _context.Users on e.RequestingUserId equals requestinUser.Id
                        join ticketType in _context.TicketTypes on e.TicketTypeId equals ticketType.Id
                        join ticketCategory in _context.TicketCategories on e.TicketCategoryId equals ticketCategory.Id
                        join ticketStatus in _context.TicketStatus on e.TicketStatusId equals ticketStatus.Id
                        join usserAssing in _context.Users on e.UserAssingId equals usserAssing.Id into us
                        from us1 in us.DefaultIfEmpty()
                        join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id into au
                        from au1 in au.DefaultIfEmpty()
                        join ticketDetail in _context.TicketDetails.Where(dt => dt.TracingStatusId == 1) on e.Id equals ticketDetail.TicketId into td
                        from td1 in td.DefaultIfEmpty()
                        select new TicketForListDto{
                            Id = e.Id,
                            Description = e.Description,
                            RequestingUserName = requestinUser.Names + " " + requestinUser.LastName,
                            RequestingUserId = e.RequestingUserId,
                            TicketTypeDescription = ticketType.Description,
                            TicketTypeId = e.TicketTypeId,
                            TicketCategoryDescription = ticketCategory.Description,
                            TicketCategoryId = e.TicketCategoryId,
                            UserAssingName = us1.Names + " " + us1.LastName,
                            UserAssingId = e.UserAssingId,
                            AssignedUserName = au1.Names + " " + au1.LastName,
                            AssignedUserId = e.AssignedUserId,
                            AssignationDate = e.AssignationDate,
                            TicketStatusDescription = ticketStatus.Description,
                            TicketStatusId = e.TicketStatusId,
                            DetailTicketDescription = td1.Description,
                            UpdatedAt = e.UpdatedAt
                        }).FirstOrDefaultAsync(x => x.Id == id);
                        

            return ticket;
        }

        public async Task<List<TicketForListDto>> GetTickets()
        {
            var tickets = await (from e in _context.Tickets
                        join requestinUser in _context.Users on e.RequestingUserId equals requestinUser.Id
                        join ticketType in _context.TicketTypes on e.TicketTypeId equals ticketType.Id
                        join ticketCategory in _context.TicketCategories on e.TicketCategoryId equals ticketCategory.Id
                        join ticketStatus in _context.TicketStatus on e.TicketStatusId equals ticketStatus.Id
                        join usserAssing in _context.Users on e.UserAssingId equals usserAssing.Id into us
                        from us1 in us.DefaultIfEmpty()
                        join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id into au
                        from au1 in au.DefaultIfEmpty()
                        join ticketDetail in _context.TicketDetails.Where(dt => dt.TracingStatusId == 4) on e.Id equals ticketDetail.TicketId into td
                        from td1 in td.DefaultIfEmpty()
                        select new TicketForListDto{
                            Id = e.Id,
                            Description = e.Description,
                            RequestingUserName = requestinUser.Names + " " + requestinUser.LastName,
                            RequestingUserId = e.RequestingUserId,
                            TicketTypeDescription = ticketType.Description,
                            TicketTypeId = e.TicketTypeId,
                            TicketCategoryDescription = ticketCategory.Description,
                            TicketCategoryId = e.TicketCategoryId,
                            UserAssingName = us1.Names + " " + us1.LastName,
                            UserAssingId = e.UserAssingId,
                            AssignedUserName = au1.Names + " " + au1.LastName,
                            AssignedUserId = e.AssignedUserId,
                            AssignationDate = e.AssignationDate,
                            TicketStatusDescription = ticketStatus.Description,
                            TicketStatusId = e.TicketStatusId,
                            DetailTicketDescription = td1.Description,
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

        public async Task<bool> ServeTicket(TicketForServeDto ticket, int id)
        {
            _context.Entry(await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(ticket);

            return true;
        }

        public async Task<int> UpdateTicket(TicketForAssingDto ticket, int id)
        {
            _context.Entry(await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(ticket);

            return id;
        }
    }
}