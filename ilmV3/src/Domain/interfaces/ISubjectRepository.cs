using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface ISubjectRepository
{
    Task<List<SubjectEntity>> GetSubjectsAsync();
    Task<SubjectEntity?> GetSubjectByIdAsync(int id);
    Task<bool> CreateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<bool> UpdateSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
    Task<bool> DeleteSubjectAsync(SubjectEntity subject, CancellationToken cancellationToken);
}
