using System;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _repo;
        private readonly IMapper _mapper;

        public EmailController(IEmailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            var emails = await _repo.List();

            return Ok(emails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMail(int id)
        {
            var email = await _repo.Get(id);

            return Ok(email);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Email email)
        {
            var addedEmail = await _repo.Add(email);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, EmailForUpdateDto email)
        {
            var editedEmail = await _repo.Edit(email, id);

            // var roleFromRepo = await _repo.GetRole(id);

            // _mapper.Map(role, roleFromRepo);

            return StatusCode(201);
        }
    }
}