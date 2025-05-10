namespace ilmV3.Domain.interfaces;
public interface IStudentRepository
{
    Task<StudentEntity?> GetStudentByIdAsync(int id);
    Task<StudentEntity> CreateStudentAsync(StudentEntity student, CancellationToken cancellationToken);
    Task<StudentEntity> UpdateStudentAsync(StudentEntity student, CancellationToken cancellationToken);
    Task<bool> DeleteStudentAsync(StudentEntity student, CancellationToken cancellationToken);
}
