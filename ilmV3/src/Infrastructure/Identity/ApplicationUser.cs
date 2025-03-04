using Microsoft.AspNetCore.Identity;

namespace ilmV3.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public int  ExternalUserId { get; set; }
}
