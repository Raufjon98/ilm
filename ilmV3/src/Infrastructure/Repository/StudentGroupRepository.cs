using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ilmV3.Infrastructure.Repository;
public class StudentGroupRepository : IStudentGroupRepository
{
    private readonly IApplicationDbContext _context;
    public StudentGroupRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken)
    {
        await _context.StudentGroups.AddAsync(studentGroup);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
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

    public async Task<List<StudentGroupEntity>> GetStudentGroupByStudentAsync(int studentId)
    {
        return await _context.StudentGroups
             .Include(s => s.Students)
             .Where(s => s.Students!.Any(s => s.Id == studentId))
             .ToListAsync();
    }

    public async Task<List<StudentEntity>> GetStudentGroupMembersAsync(int studentGroupId)
    {
        return await _context.Students
              .Include(s => s.Groups)
              .Where(s => s.Groups!.Any(sm => sm.Id == studentGroupId))
              .ToListAsync();
    }

    public async Task<List<StudentGroupEntity>> GetStudentGroupsAsync()
    {
        return await _context.StudentGroups.ToListAsync();
    }

    public async Task<bool> UpdateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken)
    {
        _context.StudentGroups.Update(studentGroup);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public async Task<TeacherEntity?> GetTeacherByStudentGroupAsync(int studentGroupId)
    {
      var group = await _context.StudentGroups
            .Include(s=>s.Subject)
            .FirstOrDefaultAsync(sg=>sg.Id == studentGroupId);
        if (group == null)
        {
            throw new Exception("The group does not found!");
        }

        var teacher = _context.Teachers.
            Include(t => t.Subject)
            .FirstOrDefault(t=> t.Subject !=null && t.Subject.Id == group.SubjectId);
        if ( teacher == null)
        {
            throw new Exception("Teacher not found!");
        }
        var result = new TeacherEntity
        {
            Id = teacher.Id,
            Subject = teacher.Subject,
            Name = teacher.Name,
        };
        return result;
    }
}
