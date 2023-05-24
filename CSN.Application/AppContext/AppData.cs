using CSN.Application.AppContext.Models;
using CSN.Application.AppData.Interfaces;
using CSN.Application.Services.Helpers.Enums;
using CSN.Domain.Entities.Users;
using CSN.Domain.Exceptions;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.AppData;

public class AppData : IAppData
{
    public List<UserC> ConnectedUsers { get; } = new List<UserC>();
    private readonly ReaderWriterLockSlim _lock = new();

    public UserC? GetById(int id)
    {
        return this.ConnectedUsers.FirstOrDefault(user => user.Id == id);
    }

    public UserC? GetByChatCId(string chatCId)
    {
        return this.ConnectedUsers.FirstOrDefault(user => user.ChatHubId == chatCId);
    }

    public IReadOnlyList<string> GetConnectionIds(ICollection<User>? users, HubType type)
    {
        var connectionUsers = this.ConnectedUsers?.Where(userC => users?.Any(user => user.Id == userC.Id) ?? false).ToList();

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


    public IReadOnlyList<string> GetConnectionIds(IEnumerable<User>? users, HubType type)
    {
        var connectionUsers = this.ConnectedUsers?.Where(userC => users?.Any(user => user.Id == userC.Id) ?? false).ToList();

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

    public void SetState(int userId, UserState state)
    {
        lock (_lock)
        {
            var userC = this.GetById(userId) ??
                throw new BadRequestException("Connected user not found");

            userC.State = state;
        }
    }

    public void AddUserConnected(User user, HubType type, string? connectionId)
    {
        lock (_lock)
        {
            var userC = this.GetById(user.Id);

            if (userC == null)
            {
                userC = new UserC(user.Id);
                this.ConnectedUsers.Add(userC);
            }

            if (type == HubType.Chat)
                userC.ChatHubId = connectionId;
            else if (type == HubType.State)
                userC.StateHubId = connectionId;
            else if (type == HubType.Notification)
                userC.NotificationHubId = connectionId;
        }
    }

    public bool RemoveUserConnected(int userId, HubType type)
    {
        lock (_lock)
        {
            var userC = this.GetById(userId) ??
                        throw new BadRequestException("Connected user not found");

            if (type == HubType.Chat)
                userC.ChatHubId = null;
            else if (type == HubType.State)
                userC.StateHubId = null;
            else if (type == HubType.Notification)
                userC.NotificationHubId = null;

            if (userC.ChatHubId == null && userC.StateHubId == null && userC.NotificationHubId == null && userC.State == UserState.Offline)
            {
                return (userC != null)
                    ? this.ConnectedUsers.Remove(userC)
                    : false;
            }
            return false;
        }
    }
}
