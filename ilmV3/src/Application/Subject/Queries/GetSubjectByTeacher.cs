using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetSubjectByTeacherQuery(int teacherId) : IRequest<SubjectVM>;

public class GetSubjectByTeacherQueryHandler : IRequestHandler<GetSubjectByTeacherQuery, SubjectVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IApplicationDbContext _context;
    public GetSubjectByTeacherQueryHandler(ISubjectRepository subjectRepository,
        IMapper mapper, IApplicationDbContext context)
    {
        _subjectRepository = subjectRepository;
        _context = context;
    }
    public async Task<SubjectVM> Handle(GetSubjectByTeacherQuery request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.TeacherId == request.teacherId);

        if (subject == null)
            throw new Exception("Subject does not exists!");

        SubjectVM subjectVM = new SubjectVM()
        {
            Id = subject.Id,
            Name = subject.Name,
            TeacherId = subject.TeacherId,
        };
        return subjectVM;
    }
}
