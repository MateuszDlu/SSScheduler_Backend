using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperSimpleScheduler_Backend.Services;

namespace SuperSimpleScheduler_Backend.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService){
            _taskService = taskService;
        }

        [HttpGet("all-category/{categoryId:int}")]
        public async Task<IActionResult> GetCategoryTasks(int categoryId){
            var result = await _taskService.GetTasksByCategoryIdAsync(categoryId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTask(
            [FromForm] string title, [FromForm] string? description, [FromForm] DateTime? deadline, [FromForm] int categoryId)
        {
            var result = await _taskService.CreateTaskAsync(title, description, deadline, categoryId);
            if (!(result is Models.Task)){
                return BadRequest(result);
            }
            if (result==null){
                return BadRequest("result is null");
            }
            return Ok(result);
        }

        [HttpPut("{taskId:int}")]
        public async Task<IActionResult> UpdateTask(
            int taskId, [FromForm] string title, [FromForm]string? description, [FromForm]DateTime? deadline, [FromForm]int categoryId)
        {
            var result = await _taskService.UpdateTaskAsync(taskId, title, description, deadline, categoryId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{taskId:int}")]
        public async Task<IActionResult> DeleteTask(int taskId){
            var result = await _taskService.DeleteTaskByIdAsync(taskId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}