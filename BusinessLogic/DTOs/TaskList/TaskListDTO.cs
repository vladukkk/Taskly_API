using BusinessLogic.DTOs.Task;

namespace BusinessLogic.DTOs.TaskList
{
    public class TaskListDTO
    {
        public Guid Id { get; set; }

        //public Guid UserId { get; set; }
        //public User User { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TaskDTO>? Tasks { get; set; }
    }
}
