using CSN.Domain.Shared.Enums;

namespace CSN.Application.Services.Models.ChannelDto
{
    public class MessageResponseForOne
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string? Login { get; set; }
        public string? Text { get; set; }
        public string? Html { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
    }

    public class UserResponseForOne
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public string? Post { get; set; }
        public UserState State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ChannelResponseForOne
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
        public IList<UserResponseForOne> Users { get; set; } = new List<UserResponseForOne>();
        public IList<MessageResponseForOne> Messages { get; set; } = new List<MessageResponseForOne>();
    }


    public class ChannelGetResponse
    {
        public ChannelResponseForOne? Channel { get; set; }

        public ChannelGetResponse(ChannelResponseForOne? channel)
        {
            this.Channel = channel;
        }

        public ChannelGetResponse() { }
    }
}