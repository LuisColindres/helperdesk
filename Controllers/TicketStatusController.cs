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
    public class TicketStatusController : ControllerBase
    {
        private readonly ICatalogsRepository _repo;
        private readonly IMapper _mapper;

        public TicketStatusController(ICatalogsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketStatuses()
        {
            var ticketStatuses = await _repo.GetTicketStatuses();

            return Ok(ticketStatuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketStatus(int id)
        {
            var ticketStatus = await _repo.GetTicketStatus(id);

            return Ok(ticketStatus);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TicketStatus ticketStatus)
        {
            _repo.Add(ticketStatus);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TicketStatusForUpdateDto ticketStatusForUpdateDto)
        {
            var ticketStatus = await _repo.GetTicketStatus(id);

            _mapper.Map(ticketStatusForUpdateDto, ticketStatus);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }
    }
}