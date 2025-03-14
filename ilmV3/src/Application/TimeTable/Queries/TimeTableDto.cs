﻿namespace ilmV3.Application.TimeTable.Queries;
public class TimeTableDto
{
    public int StudentGroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public DayOfWeek WeekDay { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
