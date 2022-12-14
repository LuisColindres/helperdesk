using System;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HelperDesk.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
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

        [HttpGet("byrole/{roleId}")]
        public async Task<IActionResult> GetByRole(int roleId)
        {
            var users = await _repo.GetUserForRole(roleId);

            return Ok(users);
        }

        [HttpGet("bydepartment/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var users = await _repo.GetUsersForDepartment(departmentId);

            return Ok(users);
        }

        [HttpPost("byfilter")]
        public async Task<IActionResult> GetByFilter(UserForFilterDto user)
        {

            var users = await _repo.GetUsersByFilter(user.DepartmentId, user.StartDate, user.EndDate);

            return Ok(users);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserForRegisterDto userForRegisterDTO)
        {
            var userToCreate = new User
            {
                Names = userForRegisterDTO.Names,
                LastName = userForRegisterDTO.LastName,
                Email = userForRegisterDTO.Email,
                Phone = userForRegisterDTO.Phone,
                GenderId = userForRegisterDTO.GenderId,
                RoleId = userForRegisterDTO.RoleId,
                CompanyId = userForRegisterDTO.CompanyId,
                status = userForRegisterDTO.status,
                Username = userForRegisterDTO.Username
                // Authy_id = "1"
                // Authy_id = userForRegisterDTO.Authy_id

            };

            var user = await _repo.Add(userToCreate, userForRegisterDTO.Password);

            return Ok(user);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, UserForUpdateDto userForUpdateDto)
        {
            await _repo.Edit(id,userForUpdateDto);

            return StatusCode(200);
            // var user = await _repo.GetUser(id);

            // _mapper.Map(userForUpdateDto, user);

            // if (await _repo.SaveAll())
            //     return NoContent();

            // throw new Exception($"Error al actualizar el usuario {id}");

        }
    }
}
