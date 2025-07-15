﻿using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetTeacherBySubjectQuery(int subjectId) : IRequest<TeacherVM>;

public class GetTecherBySubjectQueryHandler : IRequestHandler<GetTeacherBySubjectQuery, TeacherVM>
{
    private readonly IAplicationDbContext _context;
    public GetTecherBySubjectQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TeacherVM> Handle(GetTeacherBySubjectQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.Include(t => t.Subject)
            .FirstOrDefaultAsync(s => s.Subject != null && s.Subject.Id == request.subjectId);
        ArgumentNullException.ThrowIfNull(teacher);

        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };
        return teacherVM;
    }
}
