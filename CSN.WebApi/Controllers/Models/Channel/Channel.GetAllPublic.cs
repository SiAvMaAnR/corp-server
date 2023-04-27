using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.WebApi.Controllers.Models.Channel;

public class ChannelGetAllPublic
{
    public string? SearchFilter { get; set; }
}