using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<User> Edit( int id, UserForUpdateDto user)
        {
            _context.Entry(await _context.Users.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<UserForListDto> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var userToReturn = _mapper.Map<UserForListDto>(user);

            return userToReturn;
        }

        public async Task<List<UserForListDto>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            var users = await (from e in _context.Users
                               join gender in _context.Genders on e.GenderId equals gender.Id
                               join role in _context.Roles on e.RoleId equals role.Id
                               join company in _context.Companies on e.CompanyId equals company.Id
                               select new UserForListDto
                               {
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
                                   RoleDescription = role.RoleDescription,
                                   Company = company,
                                   CompanyId = e.CompanyId,
                                   CreatedAt = e.CreatedAt,
                                   Status = e.status
                               }).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}