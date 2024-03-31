using Application.Dtos;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningStatApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RunningStatController : ControllerBase
    {
        private readonly IRunningStatRepository _repo;
        private readonly IMapper _mapper;

        public RunningStatController(IRunningStatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<RunningStatConvertedDto>>> GetStatDetails(int id)
        {
            var response = new ServiceResponse<RunningStatConvertedDto>();

            var stats = await _repo.Get(id);

            if (stats == null)
            {
                response.Message = "Activity Not Found";
                response.Success = false;
                return BadRequest(response);
            }

            response.Data = _mapper.Map<RunningStatConvertedDto>(stats);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunningStatConvertedDto>>> GetAllStatistics()
        {
            var stats = await _repo.All();

            return Ok(_mapper.Map<IEnumerable<RunningStatConvertedDto>>(stats));
        }

        [HttpPost("{fileName}", Name="ParseGarminData")]
        public async Task<ActionResult<ServiceResponse<string>>>ParseGarminData(string fileName)
        {
            var response = await _repo.ParseGarminJsonFile(fileName);

            if (response != null && !response.Success)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
}
