// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using CSN.Application.Services.Helpers.Enums;
// using CSN.Domain.Entities.Users;
// using CSN.Domain.Exceptions;

// namespace CSN.Application.Services.Helpers;


// public static class ConnectionHelper
// {
//     public static IReadOnlyList<string> GetConnectionIds(this ICollection<User>? users, HubType type)
//     {
//         List<string>? hubIds = type switch
//         {
//             HubType.Chat => users?.Where(user => user.ChatHubId != null)
//                 .Select(user => user.ChatHubId!)?
//                 .ToList(),
//             HubType.State => users?.Where(user => user.StateHubId != null)
//                 .Select(user => user.StateHubId!)?
//                 .ToList(),
//             HubType.Notification => users?.Where(user => user.NotificationHubId != null)
//                 .Select(user => user.NotificationHubId!)
//                 .ToList(),
//             _ => throw new BadRequestException("Unknown filter")
//         };

//         return hubIds ?? new List<string>();
//     }
// }