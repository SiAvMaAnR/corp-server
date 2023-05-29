using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Application.Services.Adapters
{
    public static class AttachmentsExtension
    {
        public async static IEnumerable<Attachment> ToAttachmentsAsync(this IEnumerable<AttachmentResponse> attachmentsResponse) {
            var attachmentsList = attachmentsResponse?.Select(async attach =>
            {
                var contentType = attach.ContentType;
                // Attachment? attachment = null;

    var content = contentType switch{
        "image" => (attach.Content ?? "").Replace("data:image/png;base64,", ""),
        "file" => (attach.Content ?? "").Replace("data:image/png;base64,", ""),
        _ => throw new BadRequestException("Unknown format")
    }

var imageBytes = Convert.FromBase64String(content);
                        var imagePath = await imageBytes.WriteToFileAsync(contentType);

                        Attachment attachment = new Attachment()
                        {
                            Content = imagePath ?? "",
                            ContentType = contentType
                        };

                // switch (contentType)
                // {
                //     case "image":
                //     {
                //         var content = (attach.Content ?? "").Replace("data:image/png;base64,", "");
                //         var imageBytes = Convert.FromBase64String(content);
                //         var imagePath = await imageBytes.WriteToFileAsync(contentType);

                //         attachment = new Attachment()
                //         {
                //             Content = imagePath ?? "",
                //             ContentType = contentType
                //         };
                //         break;
                //     }
                //     case "file":
                //     {
                //         var content = (attach.Content ?? "").Replace("data:image/png;base64,", "");
                //         var imageBytes = Convert.FromBase64String(content);
                //         var imagePath = await imageBytes.WriteToFileAsync(contentType);

                //         attachment = new Attachment()
                //         {
                //             Content = imagePath ?? "",
                //             ContentType = contentType
                //         };
                //         break;
                //     }
                //     default:
                //         throw new BadRequestException("Unknown format");
                // }

                return attachment;
            });

            var attachments = await Task.WhenAll(attachmentsList ?? new List<Task<Attachment>>());

            var filteredAttachments = attachments.Where(attach => !string.IsNullOrEmpty(attach.Content));

            return filteredAttachments;
        }
    }
}