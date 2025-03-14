namespace ilmV3.Domain.interfaces;
public interface ITimeTableRepository
{
    Task<List<TimeTableEntity>> GetTimeTablesAsync();
    Task<TimeTableEntity?> GetTimeTableByIdAsync(int timeTableId);
    Task<TimeTableEntity> CreateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken); 
    Task<TimeTableEntity> UpdateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken); 
    Task<bool> DeleteTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken);
}
