using System.Collections.Generic;
using System.Threading.Tasks;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;

namespace HelperDesk.API.Data
{
    public interface IDepartmentRepository
    {
         Task<Department> Add(Department department);

         Task<Department> Edit(DepartmentForUpdateDto department, int id);

         Task<bool> DepartmentExists(string departmentDescription);

         Task<List<Department>> List();

         Task<Department> GetDepartment(int id);
    }
}