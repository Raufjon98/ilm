using System.Text.RegularExpressions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Register;
public record RegisterCommand(RegisterDto register) : IRequest<NewUserDto?>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, NewUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITokenService _tokenService;
    public RegisterCommandHandler(IIdentityService identityService,
        IStudentRepository studentRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<NewUserDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        StudentEntity student = new StudentEntity
        {
            Name = request.register.UserName
        };

        StudentEntity createdStudent = await _studentRepository.CreateStudentAsync(student, cancellationToken);
        if (createdStudent == null)
            return null;

        var createdUser = await _identityService.CreateUserAsync(createdStudent.Id, request.register);
        if (createdUser == null) return null;

        NewUserDto newUser = new NewUserDto();
        return newUser;
    }
}
