using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class StudentGroupRepository : IStudentGroupRepository
{
    private readonly IApplicationDbContext _context;
    public StudentGroupRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<StudentGroupEntity> CreateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken)
    {
        await _context.StudentGroups.AddAsync(studentGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return studentGroup;
    }

    public async Task<bool> DeleteStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken)
    {
        _context.StudentGroups.Remove(studentGroup);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<StudentGroupEntity?> GetStudentGroupByIdAsync(int id)
    {
        return await _context.StudentGroups.FirstOrDefaultAsync(sg => sg.Id == id);
    }

    public async Task<StudentGroupEntity> UpdateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken)
    {
        _context.StudentGroups.Update(studentGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return studentGroup;
    }
}
