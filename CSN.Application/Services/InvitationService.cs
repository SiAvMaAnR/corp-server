using System.Text.Json;
using CSN.Application.Interfaces.Services;
using CSN.Application.Models.Common;
using CSN.Application.Models.InvitationDto;
using CSN.Application.Services.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Domain.Shared.Extensions.Exceptions;
using CSN.Email;
using CSN.Email.Handlers;
using CSN.Email.Models;
using CSN.Infrastructure.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CSN.Application.Services;

public class InvitationService : BaseService<Company>, IInvitationService
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
            Email = this.configuration["Smtp:Email"],
            Password = this.configuration["Smtp:Password"],
            Host = this.configuration["Smtp:Host"],
            Port = int.Parse(this.configuration["Smtp:Port"]),
        }));
    }

    public async Task<InvitationSendInviteResponse> SendInviteAsync(InvitationSendInviteRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        Employee? employee = await this.unitOfWork.Employee.GetAsync(employee => employee.Email == request.EmployeeEmail);

        if (employee != null)
        {
            throw new BadRequestException("The user is already in the company");
        }

        IDataProtector protector = this.protection.CreateProtector(this.configuration["Invite:SecretKey"]);

        string baseUrl = this.configuration["Client:BaseUrl"];
        string path = this.configuration["Invite:Path"];

        var invitation = new Invitation()
        {
            Email = request.EmployeeEmail,
            CompanyId = company.Id,
            Role = request.EmployeeRole
        };

        await this.unitOfWork.Invitation.AddAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        string inviteJson = JsonSerializer.Serialize(new Invite()
        {
            Id = invitation.Id,
            Email = invitation.Email,
            Role = invitation.Role,
            CompanyId = company.Id
        });

        string invite = protector.Protect(inviteJson);

        string message = $"{baseUrl}/{path}?invite={invite}";

        await this.emailClient.SendAsync(new MessageModel()
        {
            From = new AddressModel(company.Login, this.configuration["Smtp:Email"]),
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
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var invitationsAll = await this.unitOfWork.Invitation.GetAllAsync(invitation => invitation.CompanyId == company.Id);

        var invitationsCount = invitationsAll?.ToList().Count ?? 0;

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
