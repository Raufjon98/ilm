using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Queries.GetAbsent;

[Authorize]
public record GetAbsentsQuery : IRequest<IEnumerable<AbsentVM>>;

public class GetAbsentsQueryHandler : IRequestHandler<GetAbsentsQuery, IEnumerable<AbsentVM>>
{
    private readonly IAplicationDbContext _context;
    public GetAbsentsQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<AbsentVM>> Handle(GetAbsentsQuery request, CancellationToken cancellationToken)
    {
        var absents = await _context.Absents.ToListAsync();
        List<AbsentVM> result = new List<AbsentVM>();
        foreach (var absent in absents)
        {
            var absentVM = new AbsentVM()
            {
                Id = absent.Id,
                StudentId = absent.StudentId,
                TeacherId = absent.TeacherId,
                SubjectId = absent.SubjectId,
                Date = absent.Date,
                ClassDay = absent.ClassDay,
                Absent = absent.Absent
            };
            result.Add(absentVM);
        }
        return result;
    }
}
