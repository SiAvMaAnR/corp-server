using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetAllRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllFilter? Filter { get; set; }
    }
}