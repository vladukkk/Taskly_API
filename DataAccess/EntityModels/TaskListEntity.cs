using DataAccess.Contracts;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace DataAccess.EntityModels
{
    public class TaskListEntity : IEntity
    {
        public Guid Id { get; set; }

        //public Guid UserId { get; set; }
        //public User User { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public List<TaskEntity>? Tasks { get; set; }

    }
}
