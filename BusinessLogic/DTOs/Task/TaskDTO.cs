using BusinessLogic.DTOs.Tag;
using BusinessLogic.DTOs.Priority;

namespace BusinessLogic.DTOs.Task
{
    public class TaskDTO
    {
        public Guid Id { get; set; }

        public Guid TaskListId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public PriorityDTO Priority { get; set; } = null!;

        public List<TagDTO>? Tags { get; set; }
    }
}
