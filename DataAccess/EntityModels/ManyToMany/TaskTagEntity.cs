namespace DataAccess.EntityModels.ManyToMany
{
    public class TaskTagEntity
    {
        public Guid TaskId { get; set; }
        public TaskEntity Task { get; set; } = null!;

        public Guid TagId { get; set; }
        public TagEntity Tag { get; set; } = null!;
    }
}
