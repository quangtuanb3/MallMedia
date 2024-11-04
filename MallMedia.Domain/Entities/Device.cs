
using MallMedia.Domain.Constants;
using System;
using System.ComponentModel.DataAnnotations;


namespace MallMedia.Domain.Entities;

public class Device
{

    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public DeviceConfiguration Configuration { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}

