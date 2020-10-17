using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HelperDesk.API.Dtos;
using System;

namespace HelperDesk.API.Data
{
    public class ManagamentRepository : IManagamentRepository
    {
        private readonly DataContext _context;
        public ManagamentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Management management)
        {
            await _context.Management.AddAsync(management);
            await _context.SaveChangesAsync();

            return management.Id;
        }

        public async Task<bool> Edit(ManagamentForUpdateDto managament, int id)
        {
            _context.Entry(await _context.Management.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(managament);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ManagamentForListDto> Get(int id)
        {
            var management = await (from e in _context.Management
                                join department in _context.Department on e.DepartmentId equals department.Id
                                join user in _context.Users on e.UserCreatedId equals user.Id
                                join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id
                                select new ManagamentForListDto{
                                    Id = e.Id,
                                    DepartmentId = department.Id,
                                    Department = department.Description,
                                    UserCreatedId = user.Id,
                                    UserCreated = user.Names + " " + user.LastName,
                                    AssignedUserId = assignedUser.Id,
                                    AssignedUser = assignedUser.Names + " " + assignedUser.LastName,
                                    Description = e.Description,
                                    File = e.File,
                                    Response = e.Response,
                                    Status = e.Status,
                                    CreatedByUserId = e.CreatedByUserId,
                                    CreatedAt = e.CreatedAt,
                                    UpdatedByUserId = e.UpdatedByUserId,
                                    UpdatedAt = e.UpdatedAt                                    
                                }
                            ).FirstOrDefaultAsync(x => x.Id == id);

            return management;
        }

        public async Task<List<ManagamentForListDto>> GetManagamentByFilter(int departmentId, int status)
        {
            var management = (from e in _context.Management
                                join department in _context.Department on e.DepartmentId equals department.Id
                                join user in _context.Users on e.UserCreatedId equals user.Id
                                join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id
                                select new ManagamentForListDto{
                                    Id = e.Id,
                                    DepartmentId = department.Id,
                                    Department = department.Description,
                                    UserCreatedId = user.Id,
                                    UserCreated = user.Names + " " + user.LastName,
                                    AssignedUserId = assignedUser.Id,
                                    AssignedUser = assignedUser.Names + " " + assignedUser.LastName,
                                    Description = e.Description,
                                    File = e.File,
                                    Response = e.Response,
                                    Status = e.Status,
                                    CreatedByUserId = e.CreatedByUserId,
                                    CreatedAt = e.CreatedAt,
                                    UpdatedByUserId = e.UpdatedByUserId,
                                    UpdatedAt = e.UpdatedAt                                    
                                }
                            );

            if (departmentId > 0) {
                management = management.Where(x => x.DepartmentId == departmentId);
            }

            if (status > 0) {
                management = management.Where(x => x.Status == status);
            }

            var list = await management.ToListAsync();

            return list;
        }

        public async Task<List<ManagamentForListDto>> List()
        {
            var management = await (from e in _context.Management
                                join department in _context.Department on e.DepartmentId equals department.Id
                                join user in _context.Users on e.UserCreatedId equals user.Id
                                join assignedUser in _context.Users on e.AssignedUserId equals assignedUser.Id
                                select new ManagamentForListDto{
                                    Id = e.Id,
                                    DepartmentId = department.Id,
                                    Department = department.Description,
                                    UserCreatedId = user.Id,
                                    UserCreated = user.Names + " " + user.LastName,
                                    AssignedUserId = assignedUser.Id,
                                    AssignedUser = assignedUser.Names + " " + assignedUser.LastName,
                                    Description = e.Description,
                                    File = e.File,
                                    Response = e.Response,
                                    Status = e.Status,
                                    CreatedByUserId = e.CreatedByUserId,
                                    CreatedAt = e.CreatedAt,
                                    UpdatedByUserId = e.UpdatedByUserId,
                                    UpdatedAt = e.UpdatedAt                                    
                                }
                            ).ToListAsync();

            return management;
        }
    }
}