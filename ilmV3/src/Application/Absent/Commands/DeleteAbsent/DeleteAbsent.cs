using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Commands.DeleteAbsent;
public record DeleteAbsentCommand(int absentId) :IRequest<bool>;

public class DeleteAbsentCommandHandler : IRequestHandler<DeleteAbsentCommand, bool>
{
    private readonly IAbsentRepository _absentRepository;
    public DeleteAbsentCommandHandler(IAbsentRepository absentRepository)
    {
        _absentRepository = absentRepository;
    }
    public async Task<bool> Handle(DeleteAbsentCommand request, CancellationToken cancellationToken)
    {
        var absent = await _absentRepository.GetAbsentByIdAsync(request.absentId);
        if (absent == null)
        {
            throw new KeyNotFoundException($"Absent record with ID {request.absentId} not found.");
}
        return await _absentRepository.DeleteAbsentAsync(absent, cancellationToken);
    }

 
}
