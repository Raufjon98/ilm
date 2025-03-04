using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.UpdateStudent;
public record UpdateStudentCommand(int studentId, StudentDto student) : IRequest<bool>;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;
    public UpdateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.studentId);
        if (student == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentId} not found.");
        }

        student.Name = request.student.Name;
        return await _studentRepository.UpdateStudentAsync(student, cancellationToken);
    }
}
