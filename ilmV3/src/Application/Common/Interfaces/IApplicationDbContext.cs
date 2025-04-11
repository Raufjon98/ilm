using ilmV3.Domain.Entities;

namespace ilmV3.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AbsentEntity> Absents { get; }
    DbSet<GradeEntity> Grades { get; }
    DbSet<StudentEntity> Students { get; }
    DbSet<StudentGroupEntity> StudentGroups { get; }
    DbSet<SubjectEntity> Subjects { get; }
    DbSet<TeacherEntity> Teachers { get; }
    DbSet<TimeTableEntity> TimeTables { get; }
    DbSet<AdminEntity> Admins { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
