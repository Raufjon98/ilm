using ilmV3.Application.Common.Models;

namespace ilmV3.Application.Common.Interfaces;
public interface ITokenService
{
    string CreateToken(ApplicationUserDto user);
}
