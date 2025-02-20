﻿using BusinessLogic.Contracts;
using BusinessLogic.DTOs.Task;
using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = await _service.GetTasks();
            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = await _service.GetById(id);
            return Ok(request);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTask([FromBody]TaskAddDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.AddTask(task);
                return Ok();
            }
            catch(TaskNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("execute{id}")]
        public async Task<IActionResult> ExecuteTask(Guid id)
        {
            await _service.ExecuteTask(id);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask([FromBody]TaskUpdateDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateTask(task);
                return Ok();
            }
            catch (TaskNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _service.DeleteTask(id);
            return NoContent();
        }
    }
}
