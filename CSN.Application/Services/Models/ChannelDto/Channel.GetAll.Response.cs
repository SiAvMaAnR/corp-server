using CSN.Domain.Entities.Channels;
using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.ChannelDto
{

    public class MessageResponseForAll
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string? Login { get; set; }
        public string? Text { get; set; }
    }

    public class UserResponseForAll
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public UserState State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ChannelResponseForAll
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDialog { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPrivate { get; set; }
        public int UnreadMessagesCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastActivity { get; set; }
        public IList<UserResponseForAll> Users { get; set; } = new List<UserResponseForAll>();
        public IList<MessageResponseForAll> Messages { get; set; } = new List<MessageResponseForAll>();
    }

    public class ChannelGetAllResponse
    {
        public IList<ChannelResponseForAll>? Channels { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int UnreadChannelsCount { get; set; }
        public int ChannelsCount { get; set; }
        public int PagesCount { get; set; }
    }
}
