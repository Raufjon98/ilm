using System.Reflection;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AbsentEntity> Absents => Set<AbsentEntity>();
    public DbSet<GradeEntity> Grades => Set<GradeEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<StudentGroupEntity> StudentGroups => Set<StudentGroupEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<TeacherEntity> Teachers => Set<TeacherEntity>();
    public DbSet<TimeTableEntity> TimeTables => Set<TimeTableEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
