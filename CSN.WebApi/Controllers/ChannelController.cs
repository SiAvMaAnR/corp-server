using System.Diagnostics;
using System.Reflection;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.WebApi.Controllers.Models.Channel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService channelService;
        private readonly ILogger<ChannelController> logger;

        public ChannelController(IChannelService channelService, ILogger<ChannelController> logger)
        {
            this.channelService = channelService;
            this.logger = logger;
        }

        [HttpGet("GetPublicChannelsOfCompany"), Authorize]
        public async Task<IActionResult> GetAllPublicChannelsOfCompany([FromQuery] ChannelGetAllPublic request)
        {
            var response = await channelService.GetAllOfCompanyAsync(new ChannelGetAllOfCompanyRequest()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchFilter = request.SearchFilter,
                TypeFilter = GetAllFilter.OnlyPublic
            });

            return Ok(new
            {
                response.Channels,
                response.ChannelsCount,
                response.PageNumber,
                response.PagesCount,
                response.PageSize
            });
        }

        [HttpGet("GetAllChannels"), Authorize]
        public async Task<IActionResult> GetAllChannels([FromQuery] ChannelGetAll request)
        {
            var response = await channelService.GetAllAsync(new ChannelGetAllRequest()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TypeFilter = request.TypeFilter,
                SearchFilter = request.SearchFilter
            });

            return Ok(new
            {
                response.Channels,
                response.ChannelsCount,
                response.PageNumber,
                response.PageSize,
                response.PagesCount,
                response.UnreadChannelsCount
            });
        }

        [HttpGet("GetUsersWhoAreNotInChannel"), Authorize]
        public async Task<IActionResult> GetUsersWhoAreNotInChannel([FromQuery] ChannelGetUsersNotInChannel request)
        {
            var response = await channelService.GetUsersNotInChannelAsync(new ChannelGetUsersRequest()
            {
                ChannelId = request.ChannelId,
                Search = request.Search
            });

            return Ok(new
            {
                response.Users,
                response.UsersCount,
            });
        }
    }
}
