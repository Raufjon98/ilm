using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.CreateStudent;
public record CreateStudentCommand(StudentDto student, string email, string password) : IRequest<bool>;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    public CreateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = _mapper.Map<StudentEntity>(request.student);
        return await _studentRepository.CreateStudentAsync(student, request.email, request.password, cancellationToken);
    }
}
