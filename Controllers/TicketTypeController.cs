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
    public class TicketTypeController : ControllerBase
    {
        private readonly ICatalogsRepository _repo;
        private readonly IMapper _mapper;

        public TicketTypeController(ICatalogsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketTypes()
        {
            var ticketTypes = await _repo.GetTicketTypes();

            return Ok(ticketTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketCategory(int id)
        {
            var ticketCategory = await _repo.GetTicketCategory(id);

            return Ok(ticketCategory);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TicketType ticketType)
        {
            _repo.Add(ticketType);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TicketTypeForUpdateDto ticketTypeForUpdateDto)
        {
            var ticketType = await _repo.GetTicketType(id);

            _mapper.Map(ticketTypeForUpdateDto, ticketType);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }
    }
}