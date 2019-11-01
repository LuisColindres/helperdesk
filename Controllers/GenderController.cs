using System.Threading.Tasks;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _repo;

        public GenderController(IGenderRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenders()
        {
            var genders = await _repo.List();

            return Ok(genders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGender(int id)
        {
            var gender = await _repo.GetGender(id);

            return Ok(gender);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Gender gender)
        {
            if (await _repo.GenderExists(gender.description.ToLower()))
                return BadRequest("El genero ya existe");

            await _repo.Add(gender);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, GenderForUpdateDto gender)
        {
            var editedGender = await _repo.Edit(gender, id);

            return NoContent();

        }
        
    }
}