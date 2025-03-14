﻿using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupMembersQuery(int studentGroupId) : IRequest<IEnumerable<StudentVM>>;

public class GetStudentGroupMembersQueryHandler : IRequestHandler<GetStudentGroupMembersQuery, IEnumerable<StudentVM>>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IApplicationDbContext _context;
    public GetStudentGroupMembersQueryHandler(IApplicationDbContext context, IMapper mapper, IStudentGroupRepository studentGroupRepository)
    {
        _context = context;
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentGroupMembersQuery request, CancellationToken cancellationToken)
    {
        var studentGroupMembers = await _context.Students
            .Include(s => s.Groups)
            .Where(s => s.Groups!.Any(sm => sm.Id == request.studentGroupId))
            .ToListAsync();
        List<StudentVM> result = new List<StudentVM>();
        foreach (var student in studentGroupMembers)
        {
            StudentVM studentVM = new StudentVM()
            {
                Id = student.Id,
                Name = student.Name,
            };
            result.Add(studentVM);
        }
        return result;
    }
}
