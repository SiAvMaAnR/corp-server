using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.TaskDto;
using CSN.WebApi.Controllers.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("GetAll"), Authorize]
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskGetAll request)
        {
            var response = await this.taskService.GetTasksAsync(new TaskGetAllRequest()
            {
                Filter = request.Filter,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Search = request.Search,
            });

            return Ok(new
            {
                response.Tasks,
                response.PageNumber,
                response.PageSize,
                response.PagesCount,
                response.TasksCount,
            });
        }


        [HttpPost("Create"), Authorize]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreate request)
        {
            var response = await this.taskService.CreateTaskAsync(new TaskCreateRequest()
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                State = request.State,
                Progress = request.Progress,
                EstimatedTime = request.EstimatedTime,
                ProjectId = request.ProjectId,
                TargetVersion = request.TargetVersion,
                TypeActivity = request.TypeActivity,
                TargetUserId = request.TargetUserId,
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }


        [HttpPut("Edit"), Authorize]
        public async Task<IActionResult> EditTask([FromBody] TaskEdit request)
        {
            var response = await this.taskService.EditTaskAsync(new TaskEditRequest()
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                State = request.State,
                Progress = request.Progress,
                EstimatedTime = request.EstimatedTime,
                TargetVersion = request.TargetVersion,
                TypeActivity = request.TypeActivity,
                TargetUserId = request.TargetUserId,
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }
    }
}