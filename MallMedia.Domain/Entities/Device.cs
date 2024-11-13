using MallMedia.Domain.Constants;
namespace MallMedia.Domain.Entities;
public class Device
{

    public int Id { get; set; }
    public string DeviceName { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public DeviceConfiguration Configuration { get; set; }
    public int ConfigurationId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
    public ICollection<DeviceSchedule> DeviceSchedules { get; set; }
    public object DeviceType { get; set; }
}

