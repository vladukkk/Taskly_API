using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Task
{
    public class TaskUpdateDTO : ITask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid PriorityId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
