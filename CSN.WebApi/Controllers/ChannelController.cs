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
                SearchFilter = request.SearchFilter,
                TypeFilter = GetAllFilter.OnlyPublic
            });

            return Ok(new
            {
                response.Channels,
                response.ChannelsCount
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
                channels = response.Channels,
                channelsCount = response.ChannelsCount,
                pageNumber = response.PageNumber,
                pageSize = response.PageSize,
                pageCount = response.PagesCount,
                unreadChannelsCount = response.UnreadChannelsCount
            });
        }
    }
}
