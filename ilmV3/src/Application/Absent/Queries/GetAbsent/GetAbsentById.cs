using ilmV3.Application.Common.Security;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Queries.GetAbsent;

[Authorize]
public record GetAbsentByIdQuery(int absentId) : IRequest<AbsentVM>;

public class GetAbsentByIdQueryHandler : IRequestHandler<GetAbsentByIdQuery, AbsentVM>
{
    private readonly IAbsentRepository _absentRepository;
    public GetAbsentByIdQueryHandler(IAbsentRepository absentRepository, IMapper mapper)
    {
        _absentRepository = absentRepository;
    }
    public async Task<AbsentVM> Handle(GetAbsentByIdQuery request, CancellationToken cancellationToken)
    {
        var absent = await _absentRepository.GetAbsentByIdAsync(request.absentId);
        if (absent == null)
        {
            throw new KeyNotFoundException($"Absent record with ID {request.absentId} not found.");
        }
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
