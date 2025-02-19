using DataAccess.Contracts;
using DataAccess.EntityModels.ManyToMany;

namespace DataAccess.EntityModels
{
    public class TagEntity : IEntity
    {
        public Guid Id { get; set ; }

        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;

        public List<TaskTagEntity>? TaskTags { get; set; }

    }
}
