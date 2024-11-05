using MallMedia.Application.Contents.Dtos;

namespace MallMedia.Application.Schedules.Dto
{
    public class SchedulesDto 
    {
        public int ContentId { get; set; }
        public ContentDto Contentdto { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
