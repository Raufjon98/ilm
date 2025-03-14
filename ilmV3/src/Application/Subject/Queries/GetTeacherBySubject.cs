using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetTeacherBySubjectQuery(int subjectId) : IRequest<TeacherVM>;

public class GetTecherBySubjectQueryHandler : IRequestHandler<GetTeacherBySubjectQuery, TeacherVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IApplicationDbContext _context;
    public GetTecherBySubjectQueryHandler(IMapper mapper,
        ISubjectRepository subjectRepository, IApplicationDbContext context)
    {
        _subjectRepository = subjectRepository;
        _context = context;
    }
    public async Task<TeacherVM> Handle(GetTeacherBySubjectQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.Include(t => t.Subject)
            .FirstOrDefaultAsync(s => s.Subject != null && s.Subject.Id == request.subjectId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.subjectId} not found");
        }

        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };
        return teacherVM;
    }
}
