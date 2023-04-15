using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.ChannelDto;
using CSN.WebApi.Models.Channel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("GetAll"), Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ChannelGetAll request)
        {
            var response = await channelService.GetAllOfCompanyAsync(new ChannelGetAllOfCompanyRequest()
            {
                SearchFilter = request.SearchFilter,
                TypeFilter = request.TypeFilter
            });

            return Ok(new
            {
                response.Channels,
                response.ChannelsCount
            });
        }
    }
}
