using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.UpdateStudentGroup;
public record UpdateStudentGroupCommand(int studentId, int StudentGroupId) : IRequest<bool>;

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentGroupCommand, bool>
{
    private readonly IStudentRepository _studentRepository;
    public UpdateStudentGroupCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<bool> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
      return await _studentRepository.UpdateStudentGroupAsync(request.studentId, request.StudentGroupId, cancellationToken);
    }
}
