using System.Threading.Tasks;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repo;

        public CompanyController(ICompanyRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _repo.List();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _repo.GetCompany(id);

            return Ok(company);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Company company)
        {
            // company.description = company.description.ToLower();

            if(await _repo.CompanyExists(company.description.ToLower()))
                return BadRequest("La empresa ya existe");

            var addedCompany = await _repo.Add(company);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id,CompanyForUpdateDto company)
        {
            var editedCompany = await _repo.Edit(company, id);

            return StatusCode(201);
        }
    }
}