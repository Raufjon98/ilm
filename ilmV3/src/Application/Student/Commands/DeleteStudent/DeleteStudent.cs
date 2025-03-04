using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.DeleteStudent;
public record DeleteStudentCommand(int studentId) : IRequest<bool>;
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;
    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.studentId);
        if (student == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentId} not found.");
        }
        return await _studentRepository.DeleteStudentAsync(student, cancellationToken);
    }
}
