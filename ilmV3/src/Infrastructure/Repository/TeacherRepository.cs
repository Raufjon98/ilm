using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class TeacherRepository : ITeacherRepository
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public TeacherRepository(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> CreateTeacherAsync(TeacherEntity teacher, string email, string password, CancellationToken cancellationToken)
    {
        var teacherNew = new TeacherEntity { Name = teacher.Name };
        await _context.Teachers.AddAsync(teacherNew);
        await _context.SaveChangesAsync(cancellationToken);
        var user = new ApplicationUser
        {
            ExternalUserId = teacherNew.Id,
            Email = email,
            UserName = teacherNew.Name

        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
        await _userManager.AddToRoleAsync(user, "Teacher");
        return result.Succeeded;
    }

    public async Task<bool> DeleteTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken)
    {
        _context.Teachers.Remove(teacher);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TeacherEntity?> GetTeacherByIdAsync(int id)
    {
        return await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TeacherEntity>> GetTeachersAsync()
    {
        return await _context.Teachers.ToListAsync();
    }
    public async Task<bool> UpdateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken)
    {
        _context.Teachers.Update(teacher);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
