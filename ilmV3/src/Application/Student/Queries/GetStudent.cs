using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;
public record GetStudentQuery(int studentId) : IRequest<StudentVM>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentVM>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    public GetStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    public async Task<StudentVM> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var result = await _studentRepository.GetStudentByIdAsync(request.studentId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentId} not found.");
        }
        return _mapper.Map<StudentVM>(result);
    }
}
