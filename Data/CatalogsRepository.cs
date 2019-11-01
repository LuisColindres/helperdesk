using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class CatalogsRepository : ICatalogsRepository
    {
        private readonly DataContext _context;
        public CatalogsRepository(DataContext context)
        {
            _context = context;

        }
        public async void Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<TicketCategory>> GetTicketCategories()
        {
            var ticketCategories = await _context.TicketCategories.ToListAsync();

            return ticketCategories;
        }

        public async Task<TicketCategory> GetTicketCategory(int id)
        {
            var ticketCategory = await _context.TicketCategories.FirstOrDefaultAsync(x => x.Id == id);

            return ticketCategory;
        }

        public async Task<TicketStatus> GetTicketStatus(int id)
        {
            var ticketStatus = await _context.TicketStatus.FirstOrDefaultAsync(x => x.Id == 1);

            return ticketStatus;
        }

        public async Task<IEnumerable<TicketStatus>> GetTicketStatuses()
        {
            var ticketStatuses = await _context.TicketStatus.ToListAsync();

            return ticketStatuses;
        }

        public async Task<TicketType> GetTicketType(int id)
        {
            var ticketType = await _context.TicketTypes.FirstOrDefaultAsync(x => x.Id == id);

            return ticketType;
        }

        public async Task<IEnumerable<TicketType>> GetTicketTypes()
        {
            var ticketTypes = await _context.TicketTypes.ToListAsync();

            return ticketTypes;
        }

        public async Task<TracingStatus> GetTracingStatus(int id)
        {
            var tracingStatus = await _context.TracingStatus.FirstOrDefaultAsync(x => x.Id == id);

            return tracingStatus;
        }

        public async Task<IEnumerable<TracingStatus>> GetTracingStatuses()
        {
            var tracingStatuses = await _context.TracingStatus.ToListAsync();

            return tracingStatuses;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}