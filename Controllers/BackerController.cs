using FundProjectAPI.DTOs;
using FundProjectAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackerController : ControllerBase
    {
        private IBackerService _service;
        public BackerController(IBackerService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BackerDto>>> Get()
        {
            return await _service.GetAllBackers();
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<BackerDto>> Get(int id)
        {
            return await _service.GetBacker(id);
        }

        [HttpPost]
        public async Task<ActionResult<BackerDto>> Post(BackerDto dto)
        {
            return await _service.AddBacker(dto);
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult<BackerDto>> Patch(int id, BackerDto dto)
        {
            try
            {
                return await _service.Update(id, dto);
            }

            catch (AggregateException e)
            {
                foreach (var exception in e.InnerExceptions)
                {
                    if (exception is NotFoundException)
                        return BadRequest(e.Message);
                }
            }

            return StatusCode(500);
        }

        [HttpPut, Route("{id}")]
        public async Task<ActionResult<BackerDto>> Put(int id, BackerDto dto)
        {
            try
            {
                return await _service.Replace(id, dto);
            }

            catch (AggregateException e)
            {
                foreach (var exception in e.InnerExceptions)
                {
                    if (exception is NotFoundException)
                        return BadRequest(e.Message);
                }
            }

            return StatusCode(500);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _service.Delete(id);
        }

        [HttpPost, Route("{projectId}")]
        public async Task<ActionResult<BackerDto>> AddBacker2Project(int projectId, BackerDto dto)
        {
            return await _service.AddBacker2Project(projectId, dto);
        }

    }
}
