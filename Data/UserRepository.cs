using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task<User> Edit(int id, User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<List<UserForListDto>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            var users = await (from e in _context.Users
                        join gender in _context.Genders on e.GenderId equals gender.Id
                        join role in _context.Roles on e.RoleId equals role.Id
                        join company in _context.Companies on e.CompanyId equals company.Id 
                        select new UserForListDto {
                            Id = e.Id,
                            Names = e.Names,
                            LastName = e.LastName,
                            Gender = gender,
                            GenderId = e.GenderId,
                            Email = e.Email,
                            Phone = e.Phone,
                            Username = e.Username,
                            Role = role,
                            RoleId = e.RoleId,
                            Company = company,
                            CompanyId = e.CompanyId,
                            CreatedAt = e.CreatedAt,
                            Status = e.status
                        }).ToListAsync();

            return users;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}