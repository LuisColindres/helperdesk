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

        public async Task<List<UserForListDto>> GetUserForRole(int roleId)
        {
            var users = await (from e in _context.Users
                               join gender in _context.Genders on e.GenderId equals gender.Id
                               join role in _context.Roles on e.RoleId equals role.Id
                               join company in _context.Companies on e.CompanyId equals company.Id
                               select new UserForListDto
                               {
                                   Id = e.Id,
                                   Names = e.Names,
                                   LastName = e.LastName,
                                   CompleteName = e.Names + " " + e.LastName,
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
                               }).Where(x => x.RoleId == roleId).ToListAsync();

            return users;
        }

        public async Task<List<UserForListDto>> GetUsersForDepartment(int departmentId) {
            
            var users = await (from e in _context.Users
                                join position in _context.Position on e.PositionId equals position.Id
                                join department in _context.Department on position.DepartmentId equals department.Id
                            //    join gender in _context.Genders on e.GenderId equals gender.Id
                            //    join role in _context.Roles on e.RoleId equals role.Id
                            //    join company in _context.Companies on e.CompanyId equals company.Id
                               select new UserForListDto
                               {
                                   Id = e.Id,
                                   Names = e.Names,
                                   LastName = e.LastName,
                                   CompleteName = e.Names + " " + e.LastName,
                                   Department = department,
                                   DepartmentId = department.Id,
                                //    Gender = gender,
                                //    GenderId = e.GenderId,
                                   Email = e.Email,
                                   Phone = e.Phone,
                                   Username = e.Username,
                                //    Role = role,
                                //    RoleId = e.RoleId,
                                //    RoleDescription = role.RoleDescription,
                                //    Company = company,
                                //    CompanyId = e.CompanyId,
                                   CreatedAt = e.CreatedAt,
                                   Status = e.status
                               }).Where(x => x.DepartmentId == departmentId).ToListAsync();

            return users;
        }

        public async Task<List<UserForListDto>> GetUsers()
        {
            // var users = await _context.Users.ToListAsync();
            var users = await (from e in _context.Users
                               join gender in _context.Genders on e.GenderId equals gender.Id
                               join role in _context.Roles on e.RoleId equals role.Id
                               join company in _context.Companies on e.CompanyId equals company.Id
                               join position in _context.Position on e.PositionId equals position.Id
                               join department in _context.Department on position.DepartmentId equals department.Id
                            //    join position in _context.Position on e.PositionId equals position.Id into ps 
                            //    from ps1 in ps.DefaultIfEmpty()
                            //    join department in _context.Department on ps1.DepartmentId equals department.Id into dp
                            //    from dp1 in dp.DefaultIfEmpty()
                               select new UserForListDto
                               {
                                   Id = e.Id,
                                   Names = e.Names,
                                   LastName = e.LastName,
                                   CompleteName = e.Names + " " + e.LastName,
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
                                   Department = department,
                                   DepartmentId = department.Id,
                                   DepartmentDescription = department.Description,
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

        public async Task<List<UserForListDto>> GetUsersByFilter(int departmentId, DateTime startDate, DateTime endDate)
        {
            var users = (from e in _context.Users
                               join gender in _context.Genders on e.GenderId equals gender.Id
                               join role in _context.Roles on e.RoleId equals role.Id
                               join company in _context.Companies on e.CompanyId equals company.Id
                               join position in _context.Position on e.PositionId equals position.Id
                               join department in _context.Department on position.DepartmentId equals department.Id
                               select new UserForListDto
                               {
                                   Id = e.Id,
                                   Names = e.Names,
                                   LastName = e.LastName,
                                   CompleteName = e.Names + " " + e.LastName,
                                   Gender = gender,
                                   GenderId = e.GenderId,
                                   Email = e.Email,
                                   Phone = e.Phone,
                                   Username = e.Username,
                                   Role = role,
                                   RoleId = e.RoleId,
                                   RoleDescription = role.RoleDescription,
                                   Position = position,
                                   PositionDescription = position.Name,
                                   Company = company,
                                   CompanyId = e.CompanyId,
                                   CreatedAt = e.CreatedAt,
                                   Department = department,
                                   DepartmentId = department.Id,
                                   DepartmentDescription = department.Description,
                                   Status = e.status
                               })
                               .Where(x => x.CreatedAt >= startDate)
                               .Where(x => x.CreatedAt <= endDate);
                            //    .ToListAsync();

            if (departmentId > 0) {
                users = users.Where(x => x.DepartmentId == departmentId);
            }

            var list = await users.ToListAsync();

            return list;
        }
    }
}