using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.UserDto;
using CSN.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("GetUsersOfCompany"), Authorize]
        public async Task<IActionResult> GetAllUsersOfCompany([FromQuery] UserGetAll request)
        {
            var response = await userService.GetAllOfCompanyAsync(new UserGetAllOfCompanyRequest()
            {
                SearchFilter = request.SearchFilter,
            });

            return Ok(new
            {
                response.Users,
                response.UsersCount
            });
        }
    }
}