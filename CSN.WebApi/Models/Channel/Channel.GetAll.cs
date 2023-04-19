using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.WebApi.Models.Channel;

public class ChannelGetAll
{
    public string? SearchFilter { get; set; }
}