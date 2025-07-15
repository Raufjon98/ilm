using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Commands.UpdateAbsent;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateAbsentCommand(int absentId, AbsentDto Absent) : IRequest<AbsentVM>;

public class UpdateAbsentCommandHandler : IRequestHandler<UpdateAbsentCommand, AbsentVM>
{
    private readonly IAbsentRepository _absentRepository;

    public UpdateAbsentCommandHandler(IAbsentRepository absentRepository, IAplicationDbContext context)
    {
        _absentRepository = absentRepository;
    }
    public async Task<AbsentVM> Handle(UpdateAbsentCommand request, CancellationToken cancellationToken)
    {
        var absent = await _absentRepository.GetAbsentByIdAsync(request.absentId);
        ArgumentNullException.ThrowIfNull(absent);

        absent.ClassDay = request.Absent.ClassDay;
        absent.StudentId = request.Absent.StudentId;
        absent.TeacherId = request.Absent.TeacherId;
        absent.SubjectId = request.Absent.SubjectId;
        absent.Absent = request.Absent.Absent;
        absent.Date = DateOnly.FromDateTime(DateTime.UtcNow);


        var result = await _absentRepository.UpdateAbsentAsync(absent, cancellationToken);

        AbsentVM absentVM = new AbsentVM
        {
            Id = absent.Id,
            ClassDay = absent.ClassDay,
            StudentId = absent.StudentId,
            TeacherId = absent.TeacherId,
            SubjectId = absent.SubjectId,
            Date = absent.Date,
            Absent = absent.Absent,
        };
        return absentVM;

    }
}
