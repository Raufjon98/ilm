namespace ilmV3.Domain.interfaces;
public interface IStudentGroupRepository
{
    Task<StudentGroupEntity?> GetStudentGroupByIdAsync(int studenGroupId);
    Task<StudentGroupEntity> CreateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
    Task<StudentGroupEntity> UpdateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
    Task<bool> DeleteStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
}
