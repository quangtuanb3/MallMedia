﻿
using System.ComponentModel.DataAnnotations;

namespace MallMedia.Domain.Entities;

public class Location

{

    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Device> Devices { get; set; } = new List<Device>();
}
