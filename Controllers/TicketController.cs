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
    [Authorize]
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

        [HttpPost("add")]
        public async Task<IActionResult> Add(JObject data)
        {

            TicketForRegisterDto ticket = data["ticketData"].ToObject<TicketForRegisterDto>();
            TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            int ticketId = await _repo.Add(ticket);

            detailDto.TicketId = ticketId;
            _repo.AddTicketDetail(detailDto);

            if( await _repo.SaveAll())
                return Ok(ticketId);

            throw new Exception($"Error al actualizar al crear el ticket");
            
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TicketForUpdateDto ticketForUpdateDto)
        {
            await _repo.Edit(ticketForUpdateDto, id);

            return StatusCode(200);
            // var ticket = await _repo.GetTicket(id);

            // // return Ok(ticket);
            // _mapper.Map(ticket, ticketForUpdateDto);

            // if (await _repo.SaveAll())
            //     return NoContent();

            // throw new Exception($"Error al actualizar el ticket {id}");
        }

        [HttpPut("assign/{id}")]
        // public async Task<IActionResult> AssignTicket(int id, TicketForUpdateDto ticketForUpdateDto)
        public async Task<IActionResult> AssignTicket(int id, JObject data)
        {
            TicketForAssingDto ticketForAssingDto = data["ticketData"].ToObject<TicketForAssingDto>();
            TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            var ticketId = await _repo.UpdateTicket(ticketForAssingDto, id);

            // var ticketFromRepo = await _repo.GetTicket(id);

            // return Ok(ticketFromRepo);

            // ticketForAssingDto.UpdatedAt = new DateTime();
            // _mapper.Map(ticketForAssingDto, ticketFromRepo);

            detailDto.TicketId = ticketId;
            _repo.AddTicketDetail(detailDto);

            if( await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }

        [HttpPut("serve/{id}")]
        public async Task<IActionResult> ServeTicket(int id, JObject data)
        {
            TicketForServeDto ticketForServeDto = data["ticketData"].ToObject<TicketForServeDto>();
            TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            await _repo.ServeTicket(ticketForServeDto, id);

            detailDto.TicketId = id;
            _repo.AddTicketDetail(detailDto);

            if( await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar al atender el ticket {id}");
        }
    }
}