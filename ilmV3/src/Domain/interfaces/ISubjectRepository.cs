namespace ilmV3.Domain.interfaces;
public interface ISubjectRepository
{
    Task<SubjectEntity?> GetSubjectByIdAsync(int subjectId);
    Task<SubjectEntity> CreateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<SubjectEntity> UpdateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<bool> DeleteSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);

}
