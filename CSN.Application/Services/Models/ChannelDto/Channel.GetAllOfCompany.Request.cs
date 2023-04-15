using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class ChannelGetAllOfCompanyRequest
    {
        public GetAllFilter TypeFilter { get; set; }
        public string? SearchFilter { get; set; }
    }
}