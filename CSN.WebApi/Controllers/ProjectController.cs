using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ProjectDto;
using CSN.WebApi.Controllers.Models.Channel;
using CSN.WebApi.Controllers.Models.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("GetAll"), Authorize]
        public async Task<IActionResult> GetAllProjects([FromQuery] ProjectGetAll request)
        {
            var response = await this.projectService.GetProjectsAsync(new ProjectGetAllRequest()
            {
                Search = request.SearchFilter,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            });

            return Ok(new
            {
                response.Projects,
                response.PageNumber,
                response.PageSize,
                response.PagesCount,
                response.ProjectsCount,
                response.ActiveCount,
                response.CompletedCount,
            });
        }


        [HttpPost("Create"), Authorize(Policy = "OnlyCompany")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreate request)
        {
            var response = await this.projectService.CreateProjectAsync(new ProjectCreateRequest()
            {
                Name = request.Name,
                Customer = request.Customer,
                Description = request.Description,
                Link = request.Link,
                State = request.State
            });

            return Ok(new
            {
                response.IsSuccess
            });
        }
    }
}
