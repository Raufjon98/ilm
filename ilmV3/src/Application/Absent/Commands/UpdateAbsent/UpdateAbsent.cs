using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Commands.UpdateAbsent;

[Authorize(Policy = Policies.CanUpdateAndDelete)] 
public record UpdateAbsentCommand(int absentId, AbsentDto absent) : IRequest<AbsentVM>;

public class UpdateAbsentCommandHandler : IRequestHandler<UpdateAbsentCommand, AbsentVM>
{
    private readonly IAbsentRepository _absentRepository;
    public UpdateAbsentCommandHandler(IAbsentRepository absentRepository)
    {
        _absentRepository = absentRepository;
    }
    public async Task<AbsentVM> Handle(UpdateAbsentCommand request, CancellationToken cancellationToken)
    {
        var absent = await _absentRepository.GetAbsentByIdAsync(request.absentId);
        if (absent == null)
        {
            throw new KeyNotFoundException($"Absent record with ID {request.absentId} not found.");
        }

        absent.ClassDay = request.absent.ClassDay;
        absent.StudentId = request.absent.StudentId;
        absent.TeacherId = request.absent.TeacherId;
        absent.SubjectId = request.absent.SubjectId;
        absent.Absent = request.absent.Absent;
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
