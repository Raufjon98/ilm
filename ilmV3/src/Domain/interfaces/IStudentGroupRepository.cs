using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface IStudentGroupRepository
{
    Task<List<StudentGroupEntity>> GetStudentGroupsAsync();
    Task<StudentGroupEntity?> GetStudentGroupByIdAsync(int studenGroupId);
    Task<List<StudentEntity>> GetStudentGroupMembersAsync(int studentGroupId);
    Task<bool> CreateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
    Task<bool> UpdateStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
    Task<bool> DeleteStudentGroupAsync(StudentGroupEntity studentGroup, CancellationToken cancellationToken);
}
