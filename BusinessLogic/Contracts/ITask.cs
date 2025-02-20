
namespace BusinessLogic.Contracts
{
    public interface ITask
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
