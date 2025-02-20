using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Task
{
    public class TaskAddDTO : ITask
    {
        //public Guid UserId { get; set; }
        //public User User { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid PriorityId { get; set; }
        public List<Guid>? TagIds { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
