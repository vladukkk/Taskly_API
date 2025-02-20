using DataAccess.Contracts;

namespace DataAccess.EntityModels
{
    public class TaskEntity : IEntity
    {
        public Guid Id { get; set; }

        //public Guid UserId { get; set; }
        //public User User { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid PriorityId { get; set; }
        public PriorityEntity Priority { get; set; } = null!;

        public List<TagEntity>? Tags { get; set; }
    }
}
