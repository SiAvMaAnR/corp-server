using CSN.Application.AppContext.Models;
using CSN.Application.Services.Helpers.Enums;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;

namespace CSN.Application.AppData.Interfaces;

public interface IAppData
{
    List<UserC> ConnectedUsers { get; }
    UserC? GetById(int id);
    void AddUserConnected(UserC userC);
    bool RemoveUserConnected(int id);
    IReadOnlyList<string> GetConnectionIds(ICollection<User>? users, HubType type);
}
