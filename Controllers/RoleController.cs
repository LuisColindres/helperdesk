using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository repo, IMapper mapper)
        {
            this._repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _repo.List();

            // var rolesToReturn = _mapper.Map<List<RoleForAddDto>>(roles);

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _repo.GetRole(id);

            // var roleToReturn = _mapper.Map<RoleForAddDto>(role);

            return Ok(role);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Role role)
        {
            role.RoleDescription = role.RoleDescription.ToLower();

            if( await _repo.RoleExists(role.RoleDescription))
                return BadRequest("El rol ya existe");

            var addedRole = await _repo.Add(role);

            return StatusCode(201);

        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, RoleForAddDto role)
        {
            var editedRole = await _repo.Edit(role, id);

            // var roleFromRepo = await _repo.GetRole(id);

            // _mapper.Map(role, roleFromRepo);

            return StatusCode(201);
        }
    }
}