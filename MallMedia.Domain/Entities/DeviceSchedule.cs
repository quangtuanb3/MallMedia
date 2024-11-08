using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Entities
{
    public class DeviceSchedule
    {
        public int Id { get; set; }

        // Foreign Key to Device
        public int DeviceId { get; set; }
        public Device Device { get; set; }  // Navigation property to Device

        // Foreign Key to Content
        public int ContentId { get; set; }
        public Content Content { get; set; }  // Navigation property to Content

        public DateTime StartDate { get; set; }  // Start date of the schedule
        public DateTime EndDate { get; set; }    // End date of the schedule

        public string Status { get; set; } = "Active"; // Status of the schedule (Active, Expired, etc.)

        // Additional fields if needed, e.g., CreatedAt, UpdatedAt
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

