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
    public class TicketCategoryController : ControllerBase
    {
        private readonly ICatalogsRepository _repo;
        private readonly IMapper _mapper;

        public TicketCategoryController(ICatalogsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketCategories()
        {
            var ticketCategories = await _repo.GetTicketCategories();

            return Ok(ticketCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketCategory(int id)
        {
            var ticketCategory = await _repo.GetTicketCategory(id);

            return Ok(ticketCategory);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TicketCategory ticketCategory)
        {
            _repo.Add(ticketCategory);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TicketCategoryForUpdateDto ticketCategoryForUpdateDto)
        {
            var ticketCategory = await _repo.GetTicketCategory(id);

            _mapper.Map(ticketCategoryForUpdateDto, ticketCategory);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }
    }
}