using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HelperDesk.API.Dtos;

namespace HelperDesk.API.Data
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DataContext _context;
        public PositionRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<int> Add(Position position)
        {
            await _context.Position.AddAsync(position);
            await _context.SaveChangesAsync();

            return position.Id;
        }

        public async Task<bool> Edit(PositionForUpdateDto position, int id)
        {
            _context.Entry(await _context.Position.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(position);
            await _context.SaveChangesAsync();

            return true;   
        }

        public async Task<PositionForListDto> GetPosition(int id)
        {
            var position = await (from e in _context.Position
                            join department in _context.Department on e.DepartmentId equals department.Id
                            join userReported in _context.Users on e.UserReportedId equals userReported.Id
                            select new PositionForListDto{
                                Id = e.Id,
                                Name = e.Name,
                                DepartmentId = department.Id,
                                DepartmentDescription = department.Description,
                                UserReportedId = userReported.Id,
                                UserName = userReported.Names + " " + userReported.LastName,
                                Target = e.Target,
                                OrganizationalPosition = e.OrganizationalPosition,
                                Scope = e.Scope,
                                Dimension = e.Dimension,
                                Education = e.Education,
                                Experience = e.Experience,
                                Skills = e.Skills,
                                OtherSkills = e.OtherSkills,
                                StartDateSkills = e.StartDateSkills,
                                EndDateSkills = e.EndDateSkills,
                                StartDatePerfomance = e.StartDatePerfomance,
                                EndDatePerfomance = e.EndDatePerfomance,
                                Status = e.Status,
                                CreatedAt = e.CreatedAt
                            }).FirstOrDefaultAsync(x => x.Id == id);

            return position;
        }

        public async Task<List<PositionForListDto>> List()
        {
            var position = await (from e in _context.Position
                            join department in _context.Department on e.DepartmentId equals department.Id
                            join userReported in _context.Users on e.UserReportedId equals userReported.Id
                            select new PositionForListDto{
                                Id = e.Id,
                                Name = e.Name,
                                DepartmentId = department.Id,
                                DepartmentDescription = department.Description,
                                UserReportedId = userReported.Id,
                                UserName = userReported.Names + " " + userReported.LastName,
                                Target = e.Target,
                                OrganizationalPosition = e.OrganizationalPosition,
                                Scope = e.Scope,
                                Dimension = e.Dimension,
                                Education = e.Education,
                                Experience = e.Experience,
                                Skills = e.Skills,
                                OtherSkills = e.OtherSkills,
                                StartDateSkills = e.StartDateSkills,
                                EndDateSkills = e.EndDateSkills,
                                StartDatePerfomance = e.StartDatePerfomance,
                                EndDatePerfomance = e.EndDatePerfomance,
                                Status = e.Status,
                                CreatedAt = e.CreatedAt
                            }).ToListAsync();

            return position;
        }
    }
}