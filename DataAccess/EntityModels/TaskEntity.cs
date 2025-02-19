using DataAccess.Contracts;
using DataAccess.EntityModels.ManyToMany;

namespace DataAccess.EntityModels
{
    public class TaskEntity : IEntity
    {
        public Guid Id { get; set; }
        
        public Guid TaskListId { get; set; }
        public TaskListEntity TaskList { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public Guid PriorityId { get; set; }
        public PriorityEntity Priority { get; set; } = null!;

        public List<TaskTagEntity>? TaskTags { get; set; }
    }
}
