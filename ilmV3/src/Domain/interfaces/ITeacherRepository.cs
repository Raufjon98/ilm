using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface ITeacherRepository
{
    Task<List<TeacherEntity>> GetTeachersAsync();
    Task<TeacherEntity?> GetTeacherByIdAsync(int teacherId);
    Task<bool> CreateTeacherAsync(TeacherEntity teacher, string email, string password, CancellationToken cancellationToken);
    Task<bool> UpdateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken);
    Task<bool> DeleteTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken);
}
