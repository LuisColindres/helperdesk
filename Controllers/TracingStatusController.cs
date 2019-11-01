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
    public class TracingStatusController : ControllerBase
    {
        private readonly ICatalogsRepository _repo;
        private readonly IMapper _mapper;

        public TracingStatusController(ICatalogsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTracingStatuses()
        {
            var tracings = await _repo.GetTracingStatuses();

            return Ok(tracings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTracingStatus(int id)
        {
            var tracingStatus = await _repo.GetTracingStatus(id);

            return Ok(tracingStatus);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TracingStatus tracionStatus)
        {
            _repo.Add(tracionStatus);

            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update(int id, TracingStatusForUpdateDto tracingStatusForUpdateDto)
        {
            var tracingStatus = await _repo.GetTracingStatus(id);

            _mapper.Map(tracingStatusForUpdateDto, tracingStatus);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Error al actualizar la categoria de ticket {id}");
        }
    }
}