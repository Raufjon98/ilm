﻿namespace ilmV3.Application.Common.Models;
public class ApplicationUserDto
{
    public string Email {  get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role {  get; set; } = string.Empty;
}
