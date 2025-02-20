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

        public Guid PriorityId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
