using System.Threading.Tasks;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HelperDesk.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentController(IDepartmentRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _repo.List();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _repo.GetDepartment(id);

            return Ok(department);
        }

        [HttpGet("position/{id}")]
        public async Task<IActionResult> GetDepartmentByPosition(int id)
        {
            var department = await _repo.GetDepartmentForPosition(id);

            return Ok(department);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Department department)
        {
            // company.description = company.description.ToLower();

            if(await _repo.DepartmentExists(department.Description.ToLower()))
                return BadRequest("El departamento ya existe");

            var addedDepartment = await _repo.Add(department);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, DepartmentForUpdateDto department)
        {
            var editedDepartment = await _repo.Edit(department, id);

            return StatusCode(201);
        }
    }
}