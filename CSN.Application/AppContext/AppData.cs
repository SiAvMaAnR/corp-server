using CSN.Application.AppContext.Models;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Helpers.Enums;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;

namespace CSN.Application.AppData;

public class AppData : IAppData
{
    public List<UserC> ConnectedUsers { get; } = new List<UserC>();

    public UserC? GetById(int id)
    {
        return this.ConnectedUsers.FirstOrDefault(user => user.Id == id);
    }

    public IReadOnlyList<string> GetConnectionIds(ICollection<User>? users, HubType type)
    {
        var connectionUsers = ConnectedUsers?.Where(userC => users?.Any(user => user.Id == userC.Id) ?? false);

        List<string>? hubIds = type switch
        {
            HubType.Chat => connectionUsers?
                .Select(user => user.ChatHubId!)?
                .ToList(),
            HubType.State => connectionUsers?
                .Select(user => user.StateHubId!)?
                .ToList(),
            HubType.Notification => connectionUsers?
                .Select(user => user.NotificationHubId!)
                .ToList(),
            _ => throw new BadRequestException("Unknown filter")
        };

        return hubIds ?? new List<string>();
    }

    public void AddUserConnected(UserC userC)
    {
        this.ConnectedUsers.Add(userC);
    }

    public bool RemoveUserConnected(int id)
    {
        var userC = this.GetById(id);
        return (userC != null)
            ? this.ConnectedUsers.Remove(userC)
            : false;
    }
}
