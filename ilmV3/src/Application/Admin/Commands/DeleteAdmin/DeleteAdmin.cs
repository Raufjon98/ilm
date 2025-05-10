﻿using ilmV3.Application.Admin.Queries;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Commands.DeleteAdmin;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record DeleteAdminCommand(string adminId) : IRequest<bool>;

public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, bool>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IIdentityService _identityService;

    public DeleteAdminCommandHandler(IIdentityService identityService, IAdminRepository adminRepository)
    {
        _identityService = identityService;
        _adminRepository = adminRepository;
    }
    public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        var admin = await _adminRepository.GetAdminByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);

        await _identityService.DeleteUserAsync(user.Id);
        return await _adminRepository.DeleteAdminAsync(admin, cancellationToken);
    }
}
