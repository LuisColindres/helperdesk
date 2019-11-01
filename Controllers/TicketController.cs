using System;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repo;
        private readonly IMapper _mapper;

        public TicketController(ITicketRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _repo.GetTickets();

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _repo.GetTicket(id);

            return Ok(ticket);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Ticket ticket)
        {
            await _repo.Add(ticket);

            return Ok();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TicketForUpdateDto ticketForUpdateDto)
        {
            var ticket = await _repo.GetTicket(id);

            _mapper.Map(ticketForUpdateDto, ticket);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar el ticket {id}");
        }

        [HttpPut("assign/{id}")]
        // public async Task<IActionResult> AssignTicket(int id, TicketForUpdateDto ticketForUpdateDto)
        public async Task<IActionResult> AssignTicket(int id, JObject data)
        {
            TicketForUpdateDto ticketForUpdateDto = data["ticketData"].ToObject<TicketForUpdateDto>();
            TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            var ticketFromRepo = await _repo.GetTicket(id);

            ticketForUpdateDto.UpdatedAt = new DateTime();
            _mapper.Map(ticketForUpdateDto, ticketFromRepo);

            _repo.AddTicketDetail(detailDto);

            if( await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }
    }
}