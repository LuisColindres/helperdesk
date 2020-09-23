using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Department> Add(Department department)
        {
            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync();

            return department;   
        }

        public async Task<bool> DepartmentExists(string departmentDescription)
        {
            if(await _context.Department.AnyAsync(x => x.Description == departmentDescription))
                return true;

            return false;
        }

        public async Task<Department> Edit(DepartmentForUpdateDto department, int id)
        {
            _context.Entry(await _context.Department.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(department);
            var tmpDepartment = await _context.Department.FirstOrDefaultAsync(x => x.Id == id);

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<Department> GetDepartment(int id)
        {
            var department = await _context.Department.FirstOrDefaultAsync(x => x.Id == id);

            return department;
        }

        public async Task<List<Department>> List()
        {
            var department = await _context.Department.ToListAsync();

            return department;
        }
    }
}