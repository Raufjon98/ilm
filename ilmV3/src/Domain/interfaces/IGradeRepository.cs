using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface IGradeRepository
{
    Task<List<GradeEntity>> GetGradesAsync();
    Task<GradeEntity?> GetGradeByIdAsync(int id);
    Task<bool> CreateGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
    Task<bool> UpdateGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
    Task<bool> DeleteGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
}
