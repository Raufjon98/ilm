using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.CreateGrade;
public record  CreateGradeCommand(GradeDto grade) :IRequest<bool>;

public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, bool>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMapper _mapper;
    public CreateGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = _mapper.Map<GradeEntity>(request.grade);
        return await _gradeRepository.CreateGradeAsync(grade, cancellationToken);
    }
}
