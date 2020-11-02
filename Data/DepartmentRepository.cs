using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

namespace HelperDesk.API.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<DepartmentForPosition> GetDepartmentForPosition(int positionId)
        {
            var department = await (from e in _context.Department
                                    join position in _context.Position on e.Id equals position.DepartmentId
                                    select new DepartmentForPosition{
                                        Description = e.Description,
                                        Status = e.Status,
                                        add = e.add,
                                        edit = e.edit,
                                        delete = e.delete,
                                        PositionId = position.Id
                                    }
                                ).FirstOrDefaultAsync(x => x.PositionId == positionId);

            var departmentToReturn = _mapper.Map<DepartmentForPosition>(department);

            return departmentToReturn;
        }

        public async Task<List<Department>> List()
        {
            var department = await _context.Department.ToListAsync();

            return department;
        }
    }
}