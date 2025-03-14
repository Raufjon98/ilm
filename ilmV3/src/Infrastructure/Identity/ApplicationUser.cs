﻿using ilmV3.Domain.interfaces;
using Microsoft.AspNetCore.Identity;

namespace ilmV3.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public int  ExternalUserId { get; set; }
    public bool Status { get; set; }
}
