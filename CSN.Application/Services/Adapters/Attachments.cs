using CSN.Application.Services.Models.MessageDto;
using CSN.Domain.Entities.Attachments;
using CSN.Domain.Exceptions;
using CSN.Persistence.Extensions;

namespace CSN.Application.Services.Adapters
{
    public static class AttachmentsExtension
    {
        public async static Task<IEnumerable<Attachment>> ToAttachmentsAsync(this IEnumerable<AttachmentRequest>? attachmentsResponse)
        {
            var attachmentsList = attachmentsResponse?.Select(async attach =>
            {
                var contentType = attach.ContentType;

                var content = contentType switch
                {
                    "image/png" => (attach.Content ?? "").Replace("data:image/png;base64,", ""),
                    "image/jpeg" => (attach.Content ?? "").Replace("data:image/jpeg;base64,", ""),
                    "image/jpg" => (attach.Content ?? "").Replace("data:image/jpg;base64,", ""),
                    "image/gif" => (attach.Content ?? "").Replace("data:image/gif;base64,", ""),
                    "file" => (attach.Content ?? "").Replace("data", ""),
                    _ => throw new BadRequestException("Unknown format")
                };

                var imageBytes = Convert.FromBase64String(content);
                var imagePath = await imageBytes.WriteToFileAsync(contentType.Replace("/", "."));

                Attachment attachment = new Attachment()
                {
                    Content = imagePath ?? "",
                    ContentType = contentType
                };

                return attachment;
            });

            var attachments = await Task.WhenAll(attachmentsList ?? new List<Task<Attachment>>());

            var filteredAttachments = attachments.Where(attach => !string.IsNullOrEmpty(attach.Content));

            return filteredAttachments;
        }
    }
}