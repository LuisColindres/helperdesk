using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class SessionRepository : ISessionRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;   

        public SessionRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Sessions> Add(Sessions sessions)
        {
            await _context.Sessions.AddAsync(sessions);
            await _context.SaveChangesAsync();

            return sessions;
        }

        public async Task<Sessions> Edit(int id, SessionForUpdateDto session)
        {
            _context.Entry( await _context.Sessions.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(session);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}