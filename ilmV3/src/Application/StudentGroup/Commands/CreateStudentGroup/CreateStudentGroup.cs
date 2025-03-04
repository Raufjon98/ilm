using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.CreateStudentGroup;
public record CreateStudentGroupCommand(StudentGroupDto studentGroup) : IRequest<bool>;

public class CreateStudentGroupCommandHandler : IRequestHandler<CreateStudentGroupCommand, bool>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public CreateStudentGroupCommandHandler(IMapper mapper, IStudentGroupRepository studentGroupRepository)
    {
         _studentGroupRepository = studentGroupRepository;
         _mapper = mapper;
    }
    public async Task<bool> Handle(CreateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = _mapper.Map<StudentGroupEntity>(request.studentGroup);
        return await _studentGroupRepository.CreateStudentGroupAsync(studentGroup, cancellationToken);
    }
}
