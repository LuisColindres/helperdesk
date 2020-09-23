using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace HelperDesk.API.Data
{
    public class RoleRepository : IRoleRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Role> Add(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role> Edit(RoleForAddDto role, int id)
        {
            _context.Entry( await _context.Roles.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(role);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<Role> GetRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            return role;
        }

        public async Task<UserForListDto> GetRoleByUser(int userId)
        {
            var user = await (from e in _context.Users
                        join role in _context.Roles on e.RoleId equals role.Id
                        select new UserForListDto{
                            Id = e.Id,
                            Names = e.Names,
                            LastName = e.LastName,
                            CompleteName = e.Names + " " + e.LastName,
                            Email = e.Email,
                            Phone = e.Phone,
                            Username = e.Username,
                            RoleDescription = role.RoleDescription,
                            RoleId = role.Id,
                            Status = e.status,
                            add = role.add,
                            edit = role.edit,
                            delete = role.delete,
                            Authy_id = e.Authy_id
                        }).FirstOrDefaultAsync(x => x.Id == userId);

            var userToReturn = _mapper.Map<UserForListDto>(user);

            return userToReturn;
        }

        public async Task<List<Role>> List()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        public async Task<bool> RoleExists(string roleDescription)
        {
            if (await _context.Roles.AnyAsync(x => x.RoleDescription == roleDescription))
                return true;

            return false;
        }

        
    }
}