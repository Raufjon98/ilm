using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface IStudentRepository
{
    Task<List<StudentEntity>> GetStudentsAsync();
    Task<StudentEntity?> GetStudentByIdAsync(int id);
    Task<List<StudentEntity>> GetExcellentStudentsAsync();
    Task<bool> CreateStudentAsync(StudentEntity student, string email, string password, CancellationToken cancellationToken);
    Task<bool> UpdateStudentAsync(StudentEntity student, CancellationToken cancellationToken);
    Task<bool> UpdateStudentGroupAsync(int studentId, int studentGroupId, CancellationToken cancellationToken);
    Task<bool> DeleteStudentAsync(StudentEntity student, CancellationToken cancellationToken);
}
