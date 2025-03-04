using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.UpdateStudentGroup;
public record UpdateStudentGroupCommand(int studentGroupId, StudentGroupDto studentGroup) : IRequest<bool>;

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentGroupCommand, bool>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    public UpdateStudentGroupCommandHandler(IStudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<bool> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(request.studentGroupId);
        if (studentGroup == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentGroupId} not found.");
        }
        studentGroup.SubjectId = request.studentGroup.SubjectId;
        studentGroup.Name = request.studentGroup.Name;
        studentGroup.CodeName = request.studentGroup.CodeName;
        
        return await _studentGroupRepository.UpdateStudentGroupAsync(studentGroup, cancellationToken);
    }
}
