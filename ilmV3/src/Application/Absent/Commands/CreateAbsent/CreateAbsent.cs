using ilmV3.Domain.interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Absent.Commands.CreateAbsent;

[Authorize(Policy = Policies.CanAdd)]
public record CreateAbsentCommand(AbsentDto absent) : IRequest<AbsentVM>;
public class CreateAbsentCommandHandle : IRequestHandler<CreateAbsentCommand, AbsentVM>
{
    private readonly IAbsentRepository _absentRepository;
    public CreateAbsentCommandHandle(IMapper mapper, IAbsentRepository absentRepository)
    {
        _absentRepository = absentRepository;
    }
    public async Task<AbsentVM> Handle(CreateAbsentCommand request, CancellationToken cancellationToken)
    {

        var absentToCreate = new AbsentEntity
        {
            SubjectId = request.absent.StudentId,
            StudentId = request.absent.StudentId,
            TeacherId = request.absent.TeacherId,
            ClassDay = request.absent.ClassDay,
            Date = request.absent.Date,
            Absent = request.absent.Absent
        };
        var absent = await _absentRepository.CreateAbsentAsync(absentToCreate, cancellationToken);
        AbsentVM absentVM = new AbsentVM
        {
            Id = absent.Id,
            SubjectId = absent.SubjectId,
            StudentId = absent.StudentId,
            TeacherId = absent.TeacherId,
            ClassDay = absent.ClassDay,
            Date = absent.Date,
            Absent = absent.Absent
        };
        return absentVM;
    }
}



