using static CSN.Application.Services.Filters.ChannelFilters;

namespace CSN.WebApi.Controllers.Models.Channel;

public class ChannelGetAll
{
    public string? SearchFilter { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public GetAllFilter TypeFilter { get; set; }
}