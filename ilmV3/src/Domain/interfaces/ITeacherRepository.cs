namespace ilmV3.Domain.interfaces;
public interface ITeacherRepository
{
    Task<TeacherEntity?> GetTeacherByIdAsync(int teacherId);
    Task<TeacherEntity> CreateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken);
    Task<TeacherEntity> UpdateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken);
    Task<bool> DeleteTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken);
}
