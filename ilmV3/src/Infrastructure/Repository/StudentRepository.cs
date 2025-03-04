using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class StudentRepository : IStudentRepository
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public StudentRepository(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> CreateStudentAsync(StudentEntity student ,string email,string password, CancellationToken cancellationToken)
    {
        var studentNew = new StudentEntity { Name = student.Name };
        await _context.Students.AddAsync(studentNew);
         await _context.SaveChangesAsync(cancellationToken);
        var user = new ApplicationUser
        {
            ExternalUserId = studentNew.Id,
            Email = email,
            UserName = student.Name
           
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
        await _userManager.AddToRoleAsync(user, "Student");
        return result.Succeeded;
    }

    public async Task<bool> DeleteStudentAsync(StudentEntity student, CancellationToken cancellationToken)
    {
        _context.Students.Remove(student);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<StudentEntity?> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<StudentEntity>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<bool> UpdateStudentAsync(StudentEntity student, CancellationToken cancellationToken)
    {
        _context.Students.Update(student);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
