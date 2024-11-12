
using System.ComponentModel.DataAnnotations;

namespace MallMedia.Domain.Entities;

public class Location

{

    public int Id { get; set; }

    public string Department { get; set; }

    public int Floor { get; set; }

}
