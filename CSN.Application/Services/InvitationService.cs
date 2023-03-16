using System.Text.Json;
using CSN.Application.Services.Common;
using CSN.Application.Services.Interfaces;
using CSN.Application.Services.Models.InvitationDto;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Entities.Users;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Exceptions;

using CSN.Email;
using CSN.Email.Handlers;
using CSN.Email.Models;
using CSN.Infrastructure.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class InvitationService : BaseService, IInvitationService
{
    private readonly EmailClient emailClient;
    private readonly IConfiguration configuration;
    private readonly IDataProtectionProvider protection;

    public InvitationService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration,
        IDataProtectionProvider protection) : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.protection = protection;

        this.emailClient = new EmailClient(new MessageHandler(new SmtpModel()
        {
            Email = this.configuration["Smtp:Email"] ?? throw new BadRequestException("Missing smtp email"),
            Password = this.configuration["Smtp:Password"] ?? throw new BadRequestException("Missing smtp password"),
            Host = this.configuration["Smtp:Host"] ?? throw new BadRequestException("Missing smtp host"),
            Port = int.Parse(this.configuration["Smtp:Port"] ?? throw new BadRequestException("Missing smtp port"))
        }));
    }

    public async Task<InvitationSendInviteResponse> SendInviteAsync(InvitationSendInviteRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        User? user = await this.unitOfWork.User.GetAsync(user => user.Email == request.EmployeeEmail);

        if (user != null)
        {
            throw new BadRequestException("The user is already exists");
        }

        string secretKey = this.configuration["Invite:SecretKey"] ?? throw new BadRequestException("Missing invite secretKey");
        string baseUrl = this.configuration["Client:BaseUrl"] ?? throw new BadRequestException("Missing client baseUrl");
        string path = this.configuration["Invite:Path"] ?? throw new BadRequestException("Missing invite path");

        IDataProtector protector = this.protection.CreateProtector(secretKey);

        var invitation = new Invitation()
        {
            Email = request.EmployeeEmail,
            CompanyId = company.Id,
            Post = request.EmployeePost
        };

        await this.unitOfWork.Invitation.AddAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        string inviteJson = JsonSerializer.Serialize(new Invite()
        {
            Id = invitation.Id,
            Email = invitation.Email,
            Post = invitation.Post,
            CompanyId = company.Id
        });

        string invite = protector.Protect(inviteJson);

        string message = $"{baseUrl}/{path}?invite={invite}";

        string companyEmail = this.configuration["Smtp:Email"] ?? throw new BadRequestException("Missing smtp email");

        await this.emailClient.SendAsync(new MessageModel()
        {
            From = new AddressModel(company.Login, companyEmail),
            To = new AddressModel("Employee", request.EmployeeEmail),
            Subject = $"Welcome to {company.Login}",
            Message = message
        });

        return new InvitationSendInviteResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<InvitationGetAllResponse> GetInvitesAsync(InvitationGetAllRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(
            this.unitOfWork,
            company => company.Invitations);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var invitationsAll = company.Invitations;

        var invitationsCount = invitationsAll?.ToList().Count ?? 0;

        var invitationsActiveCount = invitationsAll?.Count(invitation => invitation.IsActive) ?? 0;
        var invitationsAcceptedCount = invitationsAll?.Count(invitation => invitation.IsAccepted) ?? 0;

        var invitations = invitationsAll?
            .Skip(request.PageNumber * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var pagesCount = (int)Math.Ceiling(((decimal)invitationsCount / request.PageSize));

        return new InvitationGetAllResponse()
        {
            Invitations = invitations,
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            InvitationsCount = invitationsCount,
            ActiveCount = invitationsActiveCount,
            AcceptedCount = invitationsAcceptedCount,
            PagesCount = pagesCount,
        };
    }

    public async Task<InvitationSetStateResponse> SetStateAsync(InvitationSetStateRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var invitation = await this.unitOfWork.Invitation.GetAsync(invitation =>
            invitation.Id == request.Id && company.Id == invitation.CompanyId);

        if (invitation == null)
        {
            throw new NotFoundException("Invite is not found");
        }

        invitation.IsActive = request.IsActive;

        await this.unitOfWork.Invitation.UpdateAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        return new InvitationSetStateResponse()
        {
            Id = invitation.Id,
            IsActive = invitation.IsActive
        };
    }

    public async Task<InvitationRemoveResponse> RemoveInviteAsync(InvitationRemoveRequest request)
    {
        var invitation = await this.unitOfWork.Invitation.GetAsync(invitation => invitation.Id == request.Id);

        if (invitation == null)
        {
            throw new NotFoundException("Invite is not found");
        }

        await this.unitOfWork.Invitation.DeleteAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        return new InvitationRemoveResponse()
        {
            IsSuccess = true
        };
    }
}
