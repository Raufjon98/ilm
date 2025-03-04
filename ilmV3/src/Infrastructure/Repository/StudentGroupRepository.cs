﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        return  await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
