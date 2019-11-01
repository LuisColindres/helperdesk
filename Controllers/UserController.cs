using System.Threading.Tasks;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            return Ok(user);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserForRegisterDto userForRegisterDTO)
        {
            var userToCreate = new User
            {
                Names = userForRegisterDTO.Names,
                LastName = userForRegisterDTO.LastNames,
                Email = userForRegisterDTO.Email,
                Phone = userForRegisterDTO.Phone,
                GenderId = userForRegisterDTO.GenderId,
                RoleId = userForRegisterDTO.RoleId,
                CompanyId = userForRegisterDTO.CompanyId,
                status = userForRegisterDTO.status,
                Username = userForRegisterDTO.Username,
                
            };

            var user = await _repo.Add(userToCreate, userForRegisterDTO.Password);

            return Ok(user);
        }
    }
}