using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Priority
{
    public class PriorityUpdateDTO : IPriority
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
    }
}
