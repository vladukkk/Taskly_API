using BusinessLogic.Contracts;

namespace BusinessLogic.DTOs.Tag
{
    public class TagDTO : ITag
    { 
        public Guid Id { get; set; }

        public bool IsGlobal { get; set; }
        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
    }
}
