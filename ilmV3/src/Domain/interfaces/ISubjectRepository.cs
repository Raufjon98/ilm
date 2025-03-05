using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface ISubjectRepository
{
    Task<List<SubjectEntity>> GetSubjectsAsync();
    Task<SubjectEntity?> GetSubjectByIdAsync(int subjectId);
    Task<List<StudentEntity>> GetStudentsBySubjectIdAsync(int subjectId);
    Task<StudentGroupEntity?> GetGroupBySubjectAsync(int subjectId);
    Task<TeacherEntity?> GetTeacherBySubject(int subjectId);
    Task<SubjectEntity?> GetSubjectByTeacherAsync(int teacherId);
    Task<bool> CreateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<bool> UpdateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<bool> DeleteSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);

}
