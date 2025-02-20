using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Task
{
    public class TaskUpdateDTO : ITask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid PriorityId { get; set; }
        public List<Guid>? TagIds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    
    }
}
