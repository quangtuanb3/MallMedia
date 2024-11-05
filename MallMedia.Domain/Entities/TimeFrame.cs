using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Entities;

public class TimeFrame
{

    public int Id { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int CountContent { get; set; } = 0;

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public static implicit operator TimeFrame(TimeFrame v)
    {
        throw new NotImplementedException();
    }
}