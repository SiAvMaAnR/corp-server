using CSN.Application.AppContext.Models;
using CSN.Application.Services.Helpers.Enums;
using CSN.Application.Services.Models.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.AppData.Interfaces;

public interface IAppData
{
    List<UserC> ConnectedUsers { get; }
    UserC? GetById(int id);
    UserC? GetByChatCId(string chatCId);
    void SetState(int userId, UserState state);
    void AddUserConnected(User user, HubType type, string? connectionId);
    bool RemoveUserConnected(int userId, HubType type);
    IReadOnlyList<string> GetConnectionIds(IEnumerable<User>? users, HubType type);
}
