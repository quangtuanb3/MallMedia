﻿namespace MallMedia.Domain.Entities;

public class TimeFrame
{

    public int Id { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public static implicit operator string(TimeFrame v)
    {
        return v.ToString();
    }
}