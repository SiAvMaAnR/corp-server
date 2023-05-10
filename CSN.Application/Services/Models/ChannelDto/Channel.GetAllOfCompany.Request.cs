using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetAllOfCompanyRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllFilter TypeFilter { get; set; }
        public string? SearchFilter { get; set; }
    }
}