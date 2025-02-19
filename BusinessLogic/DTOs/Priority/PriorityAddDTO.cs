using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Priority
{
    public class PriorityAddDTO : IPriority
    {
        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
    }
}
