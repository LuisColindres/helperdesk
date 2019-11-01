using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class RoleRepository : IRoleRepository
    {
        public readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
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