using System.Security.Principal;
using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.CreateStudent;
public record CreateStudentCommand(RegisterDto register) : IRequest<StudentVM?>;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentVM?>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IIdentityService _identityService;
    public CreateStudentCommandHandler(IIdentityService identityService, IStudentRepository studentRepository)
    {
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<StudentVM?> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new StudentEntity()
        {
            Name = request.register.UserName,
        };

        StudentEntity studentNew = await _studentRepository.CreateStudentAsync(student, cancellationToken);

        var result = await _identityService.CreateUserAsync(studentNew.Id, request.register);
        if (result == null)
            return null;


        StudentVM studentVM = new StudentVM
        {
            Id = studentNew.Id,
            Name = studentNew.Name
        };
        return studentVM;
    }
}
