using BusinessLogic.DTOs.Priority;
using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.DTOs
{
    public class UserStatsDTO
    {
        public int TasksCount { get; set; }
        public int DoneTaskCount { get; set; }
        public List<PriorityStatsDTO>? PriorityStats { get; set; }
        public List<TagStatsDTO>? TagStats { get; set; }
    }
}
