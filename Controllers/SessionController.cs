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
    public class SessionController : ControllerBase
    {
        
        private readonly ISessionRepository _repo;
        private readonly IMapper _mapper;

        public SessionController(ISessionRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSession()
        {
            var sessions = await _repo.List();

            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(int id)
        {
            var session = await _repo.Get(id);

            return Ok(session);
        }

        [HttpPost("byfilter")]
        public async Task<IActionResult> GetByFilter(SessionForFilterDto session) 
        {
            var sessions = await _repo.GetSessionByFilter(session.DepartmentId, session.StartDate, session.EndDate);

            return Ok(sessions);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Sessions session)
        {

            var addedSession = await _repo.Add(session);
            
            return Ok(addedSession);

            // throw new Exception($"Error al crear la sesión");
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, SessionForUpdateDto session)
        {
            var editedSession = await _repo.Edit(session, id);

            return StatusCode(201);
        }
    }
}