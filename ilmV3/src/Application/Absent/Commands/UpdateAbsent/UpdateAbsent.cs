using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Commands.UpdateAbsent;
public record UpdateAbsentCommand(int absentId, AbsentDto absent) : IRequest<bool>;

public class UpdateAbsentCommandHandler : IRequestHandler<UpdateAbsentCommand, bool>
{
    private readonly IAbsentRepository _absentRepository;
    public UpdateAbsentCommandHandler(IAbsentRepository absentRepository)
    {
        _absentRepository = absentRepository;
    }
    public async Task<bool> Handle(UpdateAbsentCommand request, CancellationToken cancellationToken)
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
       

        return await _absentRepository.UpdateAbsentAsync(absent, cancellationToken);
        
    }
}
