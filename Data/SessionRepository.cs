using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<Sessions> Edit(SessionForUpdateDto session, int id)
        {
            _context.Entry( await _context.Sessions.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(session);
            await _context.SaveChangesAsync();

            return null;
        }

        public Task<Sessions> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SessionForListDto>> GetSessionByFilter(int departmentId, DateTime startDate, DateTime endDate)
        {
            var sessions = (from e in _context.Sessions
                                    join user in _context.Users on e.UserId equals user.Id
                                    join position in _context.Position on user.PositionId equals position.Id
                                    join department in _context.Department on position.DepartmentId equals department.Id
                                    select new SessionForListDto
                                    {
                                        Id = e.Id,
                                        User = user,
                                        UserId = user.Id,
                                        Names = user.Names,
                                        LastName = user.LastName,
                                        PositionDescription = position.Name,
                                        DepartmentId = department.Id,
                                        DepartmentDescription = department.Description,
                                        Information = e.Information,
                                        Attempts = e.Attempts,
                                        Active = e.Active,
                                        LoginAt = e.CreatedAt
                                    }
                                );
                                // .Where(x => x.LoginAt >= startDate)
                                // .Where(x => x.LoginAt <= endDate);
                                
            if (departmentId > 0) {
                sessions = sessions.Where(x => x.DepartmentId == departmentId);
            }

            var list = await sessions.ToListAsync();

            return list;
        }

        public async Task<List<SessionForListDto>> List()
        {
            // var sessions = await _context.Sessions.ToListAsync();
            var sessions = await (from e in _context.Sessions
                                    join user in _context.Users on e.UserId equals user.Id
                                    join position in _context.Position on user.PositionId equals position.Id
                                    join department in _context.Department on position.DepartmentId equals department.Id
                                    select new SessionForListDto
                                    {
                                        Id = e.Id,
                                        User = user,
                                        UserId = user.Id,
                                        Names = user.Names,
                                        LastName = user.LastName,
                                        PositionDescription = position.Name,
                                        DepartmentDescription = department.Description,
                                        Information = e.Information,
                                        Attempts = e.Attempts,
                                        Active = e.Active,
                                        LoginAt = e.CreatedAt
                                    }
                                ).ToListAsync();

            return sessions;
        }
    }
}