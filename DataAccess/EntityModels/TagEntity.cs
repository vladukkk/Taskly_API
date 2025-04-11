using DataAccess.Contracts;

namespace DataAccess.EntityModels
{
    public class TagEntity : IEntity
    {
        public Guid Id { get; set ; }
               
        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
               
        public string? UserId { get; set; }
        public UserEntity? User { get; set; }
        public bool isGlobal { get; set; }
               
        public List<TaskEntity>? Tasks { get; set; }
    }
}