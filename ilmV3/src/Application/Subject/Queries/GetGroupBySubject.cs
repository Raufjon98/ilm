using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanAdd)]
public record GetGroupBySubjectQuery(int subjectId) : IRequest<StudentGroupVM>;

public class GetGroupBySubjectQueryHandler : IRequestHandler<GetGroupBySubjectQuery, StudentGroupVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IApplicationDbContext _context;
    public GetGroupBySubjectQueryHandler(ISubjectRepository subjectRepository,
        IMapper mapper, IApplicationDbContext context)
    {
        _context = context;
        _subjectRepository = subjectRepository;
    }
    public async Task<StudentGroupVM> Handle(GetGroupBySubjectQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await _context.StudentGroups.FirstOrDefaultAsync(s => s.SubjectId == request.subjectId);

        if (studentGroup == null)
            throw new Exception("Group not found!");

        StudentGroupVM studentGroupVM = new StudentGroupVM()
        {
            Id = studentGroup.Id,
            Name = studentGroup.Name,
            CodeName = studentGroup.CodeName,
            SubjectId = studentGroup.SubjectId,
        };
        return studentGroupVM;
    }
}
