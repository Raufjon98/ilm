using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class SubjectRepository : ISubjectRepository
{
    private readonly IApplicationDbContext _context;
    public SubjectRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken)
    {
        await _context.Subjects.AddAsync(subject);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken)
    {
        _context.Subjects.Remove(subject);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<StudentGroupEntity?> GetGroupBySubjectAsync(int subjectId)
    {
        return await _context.StudentGroups.FirstOrDefaultAsync(s=>s.SubjectId == subjectId);
    }

    public async Task<List<StudentEntity>> GetStudentsBySubjectIdAsync(int subjectId)
    {
       
       return  await _context.Students
        .Include(s => s.Groups)
              .Where(s => s.Groups!.Any(sm => sm.SubjectId == subjectId))
              .ToListAsync();
    }

    public async Task<SubjectEntity?> GetSubjectByIdAsync(int id)
    {
        return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<SubjectEntity>> GetSubjectsAsync()
    {
        return await _context.Subjects.ToListAsync();
    }

    public async Task<TeacherEntity?> GetTeacherBySubject(int subjectId)
    {
        return await _context.Teachers.Include(t => t.Subject)
            .FirstOrDefaultAsync(s => s.Subject != null && s.Subject.Id == subjectId);
    }

    public async Task<bool> UpdateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken)
    {
        _context.Subjects.Update(subject);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public async Task<SubjectEntity?> GetSubjectByTeacherAsync(int teacherId)
    {
        return await _context.Subjects.FirstOrDefaultAsync(s => s.TeacherId == teacherId);
    }
}
