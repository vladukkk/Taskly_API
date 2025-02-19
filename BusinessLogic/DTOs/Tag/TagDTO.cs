
namespace BusinessLogic.DTOs.Tag
{
    public class TagDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string ColorHash { get; set; } = null!;
    }
}
