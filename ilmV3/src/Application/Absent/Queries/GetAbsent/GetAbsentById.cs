using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Queries.GetAbsent;

[Authorize]
public record GetAbsentByIdQuery(int absentId) : IRequest<AbsentVM>;

public class GetAbsentByIdQueryHandler : IRequestHandler<GetAbsentByIdQuery, AbsentVM>
{
    private readonly IApplicationDbContext _context;

    public GetAbsentByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AbsentVM> Handle(GetAbsentByIdQuery request, CancellationToken cancellationToken)
    {
        var absent = await _context.Absents.FirstOrDefaultAsync(x => x.Id == request.absentId);
        ArgumentNullException.ThrowIfNull(absent);
        var result = new AbsentVM
        {
            Id = absent.Id,
            StudentId = absent.StudentId,
            TeacherId = absent.TeacherId,
            SubjectId = absent.SubjectId,
            ClassDay = absent.ClassDay,
            Date = absent.Date,
            Absent = absent.Absent,
        };

        return result;
    }
}
