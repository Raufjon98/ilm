using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface ITimeTableRepository
{
    Task<List<TimeTableEntity>> GetTimeTablesAsync();
    Task<TimeTableEntity?> GetTimeTableByIdAsync(int timeTableId);
    Task<TimeTableEntity?> GetTimeTableByDateAsync(DateOnly date);
    Task<bool> CreateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken); 
    Task<bool> UpdateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken); 
    Task<bool> DeleteTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken);
}
