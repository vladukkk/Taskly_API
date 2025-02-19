using DataAccess.Contracts;

namespace DataAccess.EntityModels
{
    public class PriorityEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;

        public List<TaskEntity>? Tasks { get; set; }
    }
}
