﻿namespace ilmV3.Domain.interfaces;
public interface IApplicationUser
{
    public int ExternalUserId { get; set; }
    public string Status { get; set; }
    public string? UserName{ get; set; }
    public string? Email { get; set; }
}
