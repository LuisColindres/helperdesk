using System;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using HelperDesk.API.Dtos;
using System.Linq;
using AutoMapper;

namespace HelperDesk.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // var useToActive = new UserForActivateDto
            // {
            //     Active = true
            // };

            // _context.Entry(await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id)).CurrentValues.SetValues(useToActive);
            // await _context.SaveChangesAsync();

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        public async Task<Sessions> Add(Sessions sessions)
        {
            await _context.Sessions.AddAsync(sessions);
            await _context.SaveChangesAsync();

            return sessions;
        }

        public async Task<User> UserActive(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> ChangeActive(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var useToActive = new UserForActivateDto
            {
                Active = false
            };

            _context.Entry(await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).CurrentValues.SetValues(useToActive);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<UserForListDto> GetUser(int id)
        {
            // var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
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
                        }).FirstOrDefaultAsync(x => x.Id == id);

            var userToReturn = _mapper.Map<UserForListDto>(user);

            return userToReturn;
        }
    }
}