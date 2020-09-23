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
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _repo;
        private readonly IMapper _mapper;

        public PositionController(IPositionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            var positions = await _repo.List();

            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition(int id)
        {
            var position = await _repo.GetPosition(id);

            return Ok(position);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(JObject data)
        {

            Position position = data["position"].ToObject<Position>();
            // TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            int positionId = await _repo.Add(position);
            
            return Ok(positionId);

            throw new Exception($"Error al crear el puesto");
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, PositionForUpdateDto position)
        {
            var editedPosition = await _repo.Edit(position, id);

            return StatusCode(201);
        }
        
    }
}