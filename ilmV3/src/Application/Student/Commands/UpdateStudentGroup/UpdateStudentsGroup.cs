using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Student.Commands.UpdateStudentGroup;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateStudentsGroupCommand(int studentId, int studentGroupId) : IRequest<StudentGroupVM>;

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentsGroupCommand, StudentGroupVM>
{
    private readonly IAplicationDbContext _context;
    public UpdateStudentGroupCommandHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentGroupVM> Handle(UpdateStudentsGroupCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
         .Include(s => s.Groups)
         .Where(s => s.Id == request.studentId)
         .FirstOrDefaultAsync();
        ArgumentNullException.ThrowIfNull(student);
        student.Groups?.Clear();
        var group = await _context.StudentGroups.FindAsync(request.studentGroupId);
        ArgumentNullException.ThrowIfNull(group);

        student.Groups?.Add(group);
        var result = await _context.SaveChangesAsync(cancellationToken);
        StudentGroupVM studentGroupVM = new StudentGroupVM
        {
            Id = group.Id,
            Name = group.Name,
            CodeName = group.CodeName,
            SubjectId = group.SubjectId,
        };
        return studentGroupVM;
    }
}
